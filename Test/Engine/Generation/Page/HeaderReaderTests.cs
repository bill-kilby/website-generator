using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Page;
using website_generator.Engine.Generation.Page;

namespace Test.Engine.Generation.Page
{
    public class HeaderReaderTests : IDisposable
    {
        private IHeaderReader _headerReader;
        private IHtmlVerifier _htmlVerifier;

        public HeaderReaderTests()
        {
            _htmlVerifier = Substitute.For<IHtmlVerifier>();
            _headerReader = new HeaderReader(_htmlVerifier);
        }

        public void Dispose()
        {
            File.WriteAllText("Html/Header.html", "<head>\r\n    <meta test=\"TestHeader\">\r\n</head>");
        }

        [Fact]
        public void WhenReadingHeader_GivenHeaderExists_ThenReturnsHeader()
        {
            // Assemble

            // Act
            var result = _headerReader.ReadHeader();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenReadingHeader_GivenNoHeaderExists_ThenThrowsError()
        {
            // Assemble
            File.Delete("Html/Header.html");

            // Act
            var exception = Record.Exception(
                () => _headerReader.ReadHeader()
                );

            // Assert
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact]
        public void WhenReadingHeader_GivenInvalidHtml_ThenThrowsError()
        {
            // Assemble
            File.WriteAllText("Html/Header.html", "<head<head>");

            // Act
            var exception = Record.Exception(
                () => _headerReader.ReadHeader()
                );

            // Assert
            Assert.IsType<InvalidHTMLException>(exception);
        }
    }
}
