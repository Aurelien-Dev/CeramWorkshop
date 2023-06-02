using ExternalServices.ServicesUploadImage;
using Moq;
using RestSharp;
using System.Net;
using Client.Services;
using Utils.Converters;
using Utils.Exception;
using Xunit;

namespace Tests
{
    public class ImageServiceTests
    {

        private readonly ImageService _imageService;

        public ImageServiceTests()
        {
            _imageService = new ImageService();

        }

        [Fact]
        public async Task UploadFile_ThrowsArgumentNullException_WhenFilePathIsEmpty()
        {       
            
        }


    }
}