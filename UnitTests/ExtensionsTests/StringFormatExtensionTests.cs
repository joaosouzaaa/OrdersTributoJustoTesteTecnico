using OrdersTributoJustoTesteTecnico.Business.Extensions;

namespace UnitTests.ExtensionsTests
{
    public sealed class StringFormatExtensionTests
    {
        [Fact]
        public void FormatTo_ReturnsFormatedString()
        {
            var formatedString = "{0} meu nome é {1}".FormatTo("oi", "joao");

            Assert.Equal("oi meu nome é joao", formatedString);
        }

        [Fact]
        public void CleanCaracters_ReturnsCleanedString()
        {
            var cleanedString = "akakkakakk81818".CleanCaracters();

            Assert.Equal("81818", cleanedString);
        }
    }
}
