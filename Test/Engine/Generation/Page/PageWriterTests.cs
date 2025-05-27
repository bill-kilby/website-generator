using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Page;
using website_generator.Engine.Generation.Page;

namespace Test.Engine.Generation.Page
{
    public class PageWriterTests
    {
        private readonly IPageWriter _pageWriter;

        public PageWriterTests()
        {
            _pageWriter = new PageWriter();
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
        public void WhenWritingPage_GivenEmptyInput_ThenJustWritesHeader()
        {
            // Assemble
            var input = "";
            var expected = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n<head>\r\n    <meta test=\"TestHeader\">\r\n</head>\r\n\r\n<body>\r\n</body>\r\n\r\n</html>";

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
    }
}
