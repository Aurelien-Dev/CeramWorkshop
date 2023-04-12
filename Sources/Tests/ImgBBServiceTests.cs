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
    public class ImgBbServiceTests
    {
        private const string Base64String = "base64string";
        private const string FilePath = "nonexistentfile.png";

        private readonly ImgBbService _imgBbService;
        private readonly Mock<IRestClient> _mockClient;

        public ImgBbServiceTests()
        {
            _mockClient = new Mock<IRestClient>();

            Base64Converter.ConvertFileToBase64Func = (path) => Task.FromResult(Base64String);

            var mockResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "{\"success\":true,\"data\":{\"image\":{\"url\":\"imageurl\"},\"thumb\":{\"url\":\"thumburl\"},\"medium\":{\"url\":\"mediumurl\"}}}"
            };

            _mockClient.Setup(client => client.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            _imgBbService = new ImgBbService(_mockClient.Object);
        }


        [Fact]
        public async Task UploadFile_ThrowsArgumentNullException_WhenFilePathIsNull()
        {
            string? nullFilePath = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _imgBbService.UploadFile(nullFilePath));
        }

        [Fact]
        public async Task UploadFile_ThrowsArgumentNullException_WhenFilePathIsEmpty()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _imgBbService.UploadFile(string.Empty));
        }

        [Fact]
        public async Task UploadFile_ThrowsUploadFileException_WhenBase64ConversionFails()
        {
            // Arrange
            Base64Converter.ConvertFileToBase64Func = (path) => Task.FromResult(string.Empty);

            // Act & Assert
            await Assert.ThrowsAsync<UploadFileException>(() => _imgBbService.UploadFile(FilePath));
        }

        [Fact]
        public async Task UploadFile_ReturnsImageInstruction_WhenApiCallSucceeds()
        {
            // Act
            (string url, string thumb, string medium) = await _imgBbService.UploadFile(FilePath);

            // Assert
            Assert.Equal("imageurl", url);
            Assert.Equal("thumburl", thumb);
            Assert.Equal("mediumurl", medium);
        }


    }
}