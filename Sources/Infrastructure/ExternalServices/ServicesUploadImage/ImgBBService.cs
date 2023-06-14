using ExternalServices.ServicesUploadImage.Model;
using RestSharp;
using System.Text.Json;
using Utils.Converters;
using Utils.Exception;
using Utils.Singletons;

namespace ExternalServices.ServicesUploadImage
{
    /// <summary>
    /// Documentation of API Service : https://api.imgbb.com/
    /// </summary>
    public class ImgBbService : IImgBbService
    {
        private readonly IRestClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        public ImgBbService()
        {
            _client = new RestClient(EnvironementSingleton.GetEnvironmentVariable("IMGBB_URL"));
        }

        /// <summary>
        /// Constructor used for UnitTest
        /// </summary>
        public ImgBbService(IRestClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Upload image on imgBB services
        /// </summary>
        /// <param name="filePath">Path of original image</param>
        /// <returns>Return image object with all URL</returns>
        /// <exception cref="UploadFileException">Exception when not able to convert image to base64 string</exception>
        /// <exception cref="ApiCallErrorException">Exception after calling API, depending status</exception>
        public async Task<(string?, string?, string?)> UploadFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");

            string b64String = await Base64Converter.ConvertFileToBase64Func(filePath);

            if (string.IsNullOrEmpty(b64String)) throw new UploadFileException("Error generate base 64 image");

            var request = new RestRequest();
            request.AlwaysMultipartFormData = true;
            request.Method = Method.Post;
            request.AddParameter("image", b64String, ParameterType.RequestBody);
            request.AddParameter("expiration", "30", ParameterType.RequestBody);
            RestResponse response = await _client.ExecuteAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ApiCallErrorException("Error500: Erreur à l'appel de l'API");
            if (string.IsNullOrEmpty(response.Content)) throw new ApiCallErrorException("No data return");

            var apiResponse = JsonSerializer.Deserialize<ImgBBResponse>(response.Content);

            if (apiResponse == null) throw new ClientException("Unable to deserialize result");
            if (!apiResponse.success) throw new ApiCallErrorException($"Erreur au résultat de l'API : {apiResponse.status}");

            return (apiResponse.data.image?.url, apiResponse.data.thumb?.url, apiResponse.data.medium?.url);
        }

        public async Task<string> DownloadFile(string prefix, string url, string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException($"{nameof(path)} Is Null Or Empty : {path}");

            byte[] dataMedium = await new RestClient().DownloadDataAsync(new RestRequest(url, Method.Get));
            if (dataMedium == null)
                throw new InvalidOperationException($"Cannot download file: {url}");

            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
                throw new InvalidOperationException($"Directory does not exist : {path}");

            string pathMedium = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(path)}_{prefix}{Path.GetExtension(path)}");
            if (string.IsNullOrEmpty(pathMedium))
                throw new ArgumentException($"{nameof(path)} is probably wrong : {path}");

            File.WriteAllBytes(pathMedium, dataMedium);

            return pathMedium;
        }
    }
}