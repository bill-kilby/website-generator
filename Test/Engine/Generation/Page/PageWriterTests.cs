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
    public class PageWriterTests : IDisposable
    {
        private readonly IPageWriter _pageWriter;
        private readonly IHtmlVerifier _htmlVerifier;
        private readonly IHeaderReader _headerReader;

        public PageWriterTests()
        {
            _htmlVerifier = Substitute.For<IHtmlVerifier>();
            _headerReader = Substitute.For<IHeaderReader>();

            _pageWriter = new PageWriter(_htmlVerifier, _headerReader);
        }

        public void Dispose()
        {
            if (File.Exists("Output/index.html"))
            {
                File.Delete("Output/index.html");
            }
        }

        [Fact]
        public void WhenWritingPage_GivenValidInput_ThenWritesPage()
        {
            // Assemble
            var input = "<div></div>";

            // Act
            _pageWriter.Write(input);
            string result = string.Empty;
            var exception = Record.Exception(
                () => result = File.ReadAllText("output/index.html")
                );

            // Assert
            Assert.Null(exception);
            Assert.NotNull(result);
        }

        [Fact]
        public void WhenWritingPage_GivenEmptyInput_ThenJustEmptyPageWithHeader()
        {
            // Assemble
            var input = "";
            var expected = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta test=\"TestHeader\">\r\n</head>\r\n\r\n<body>\r\n\r\n</body>\r\n\r\n</html>";
            _headerReader.ReadHeader().Returns("<head>\r\n    <meta test=\"TestHeader\">\r\n</head>");

            // Act
            _pageWriter.Write(input);
            string result = string.Empty;
            var exception = Record.Exception(
                () => result = File.ReadAllText("output/index.html")
                );

            // Assert
            Assert.Null(exception);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenWritingPage_GivenInvalidHtmlInput_ThenThrowsError()
        {
            // Assemble
            var input = "<div>div>";
            _htmlVerifier
                .WhenForAnyArgs(x => x.Verify(Arg.Any<string>()))
                .Do(_ => throw new InvalidHTMLException("Invalid HTML"));

            // Act
            var writeException = Record.Exception(
                () => _pageWriter.Write(input)
                );

            var readException = Record.Exception(
                () => File.ReadAllText("output/index.html")
                );

            // Assert
            Assert.IsType<InvalidHTMLException>(writeException);
            Assert.IsType<FileNotFoundException>(readException);
        }
    }
}
