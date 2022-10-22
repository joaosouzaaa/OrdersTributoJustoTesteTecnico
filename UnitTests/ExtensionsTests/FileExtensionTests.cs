using OrdersTributoJustoTesteTecnico.Business.Extensions;
using TestsBuilders.Helpers;

namespace UnitTests.ExtensionsTests
{
    public sealed class FileExtensionTests
    {
        [Fact]
        public void ImageToBytes_ReturnsSuccess()
        {
            var formFile = BuildersUtils.BuildIFormFile();

            var imageBytes = formFile.ImageToBytes();

            Assert.NotEmpty(imageBytes);
        }

        [Fact]
        public void ImageToBytes_Invalid_Format_ReturnsNull()
        {
            var formFile = BuildersUtils.BuildIFormFile(".xls");

            var imageBytes = formFile.ImageToBytes();

            Assert.Null(imageBytes);
        }
    }
}
