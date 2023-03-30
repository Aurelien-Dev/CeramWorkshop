using ExternalServices.ServicesUploadImage;
using Microsoft.Extensions.Configuration;
using Moq;
using RestSharp;
using System.Net;
using Utils.Converters;
using Utils.Exception;
using Xunit;

namespace Tests
{
    public class ImgBBServiceTests
    {
        private const string base64String = "base64string";
        private const string filePath = "nonexistentfile.png";

        private readonly ImgBBService _imgBBService;
        private readonly Mock<IRestClient> _mockClient;

        public ImgBBServiceTests()
        {
            _mockClient = new Mock<IRestClient>();

            Base64Converter.ConvertFileToBase64Func = (path) => Task.FromResult(base64String);

            var mockResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "{\"success\":true,\"data\":{\"image\":{\"url\":\"imageurl\"},\"thumb\":{\"url\":\"thumburl\"},\"medium\":{\"url\":\"mediumurl\"}}}"
            };

            _mockClient.Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            _imgBBService = new ImgBBService(_mockClient.Object);
        }


        [Fact]
        public async Task UploadFile_ThrowsArgumentNullException_WhenFilePathIsNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _imgBBService.UploadFile(null));
        }

        [Fact]
        public async Task UploadFile_ThrowsArgumentNullException_WhenFilePathIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _imgBBService.UploadFile(string.Empty));
        }

        [Fact]
        public async Task UploadFile_ThrowsUploadFileException_WhenBase64ConversionFails()
        {
            // Arrange
            Base64Converter.ConvertFileToBase64Func = (path) => Task.FromResult(string.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<UploadFileException>(() => _imgBBService.UploadFile(filePath));
        }

        [Fact]
        public async Task UploadFile_ReturnsImageInstruction_WhenApiCallSucceeds()
        {
            // Act
            (string url, string thumb, string medium) = await _imgBBService.UploadFile(filePath);

            // Assert
            Assert.Equal("imageurl", url);
            Assert.Equal("thumburl", thumb);
            Assert.Equal("mediumurl", medium);
        }


    }
}