using HtmlAgilityPack;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Engine.Generation.Common;

namespace Test.Engine.Common
{
    public class HtmlVerifierTests
    {
        private IHtmlVerifier _htmlVerifier;

        public HtmlVerifierTests()
        {
            _htmlVerifier = new HtmlVerifier();
        }

        [Theory]
        [InlineData("<div></div>")]
        [InlineData("<div>{PropertyOne}</div>")]
        public void WhenVerifyingHTML_GivenValidHTML_ThenNoErrorsThrown(string html)
        {
            // Assemble

            // Act
            var exception = Record.Exception(
                () => _htmlVerifier.Verify(html)
                );

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData("<div><div>")]
        [InlineData("<div<div>")]
        public void WhenVerifyingHTML_GivenInvalidHTML_ThenErrorsThrown(string html)
        {
            // Assemble

            // Act
            var exception = Record.Exception(
                () => _htmlVerifier.Verify(html)
                );

            // Assert
            Assert.IsType<InvalidHTMLException>(exception);
        }
    }
}
