using Domain.Models;
using ExternalServices.ServicesUploadImage.Model;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Text.Json;
using Utils.Converters;
using Utils.Exception;

namespace ExternalServices.ServicesUploadImage
{
    /// <summary>
    /// Documentation of API Service : https://api.imgbb.com/
    /// </summary>
    public class ImgBBService
    {
        private RestClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        public ImgBBService(IConfiguration configuration)
        {
            _client = new RestClient(configuration.GetConnectionString("imgbbURL"));
        }

        /// <summary>
        /// Upload image on imgBB services
        /// </summary>
        /// <param name="filePath">Path of original image</param>
        /// <returns>Return image object with all URL</returns>
        /// <exception cref="UploadFileException">Exception when not able to convert image to base64 string</exception>
        /// <exception cref="ApiCallErrorException">Exception after calling API, depending status</exception>
        public async Task<ImageInstruction> UploadFile(string filePath)
        {
            try
            {
                string b64String = await Base64Converter.ConvertFileToBase64(filePath);

                if (string.IsNullOrEmpty(b64String)) throw new UploadFileException("Error generate base 64 image");

                var request = new RestRequest();
                request.AlwaysMultipartFormData = true;
                request.Method = Method.Post;
                request.AddParameter(Parameter.CreateParameter("image", b64String, ParameterType.RequestBody));
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ApiCallErrorException("Errr500: Erreur à l'appel de l'API");

                var apiResponse = JsonSerializer.Deserialize<ImgBBResponse>(response.Content);

                if (!apiResponse.success) throw new ApiCallErrorException($"Erreur au résultat de l'API : {apiResponse.status}");

                return new ImageInstruction(apiResponse.data.image.url, apiResponse.data.thumb.url, apiResponse.data.medium.url);
            }
            catch (Exception ex)
            {
                throw new ApiCallErrorException("ApiError", ex);
            }
        }
    }
}