using Domain.Models;
using ExternalServices.ServicesUploadImage.Model;
using RestSharp;
using System.Text.Json;
using Utils.Exception;

namespace ExternalServices.ServicesUploadImage
{
    public class ImgBBService
    {
        private RestClient _client;

        public ImgBBService(  )
        {
            _client = new RestClient("https://api.imgbb.com/1/upload?key=76ae7a6cb9d11a5c4a253cfe66942a61");
        }

        public async Task<ImageInstruction> UploadFile(string b64Image)
        {
            try
            {
                var request = new RestRequest();
                request.AlwaysMultipartFormData = true;
                request.Method = Method.Post;
                request.AddParameter(Parameter.CreateParameter("image", b64Image, ParameterType.RequestBody));
                RestResponse response = await _client.ExecuteAsync(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new ApiCallErrorEsception("Errr500: Erreur à l'appel de l'API");

                var apiResponse = JsonSerializer.Deserialize<ImgBBResponse>(response.Content);

                if (!apiResponse.success) throw new ApiCallErrorEsception($"Erreur au résultat de l'API : {apiResponse.status}");
                
                return new ImageInstruction(apiResponse.data.image.url, apiResponse.data.thumb.url, apiResponse.data.medium.url);
            }
            catch (Exception ex)
            {
                throw new ApiCallErrorEsception("ApiError", ex);
            }
        }
    }
}