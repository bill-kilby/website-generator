using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Page;

namespace website_generator.Engine.Generation.Page
{
    internal class PageWriter : IPageWriter
    {
        private IHtmlVerifier _htmlVerifier;
        private IHeaderReader _headerReader;

        public PageWriter(
            IHtmlVerifier htmlVerifier,
            IHeaderReader headerReader
            )
        {
            _htmlVerifier = htmlVerifier;
            _headerReader = headerReader;
        }

        public void Write(string html)
        {
            Cleanup();

            var sb = new StringBuilder();

            AddTopHtmlTag(sb);
            AddHeader(sb);
            AddBody(sb, html);
            AddBottomHtmlTag(sb);

            _htmlVerifier.Verify(sb.ToString());

            File.WriteAllText("Output/index.html", sb.ToString());
        }

        private void Cleanup()
        {
            if (Directory.Exists("Output"))
            {
                Directory.Delete("Output");
            }

            Directory.CreateDirectory("Output");
        }

        private void AddTopHtmlTag(StringBuilder sb) => sb.Append("<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n");

        private void AddHeader(StringBuilder sb)
        {
            var header = _headerReader.ReadHeader();

            sb.Append(header);
            sb.Append("\r\n");
        }

        private void AddBody(StringBuilder sb, string body)
        {
            sb.Append("\r\n<body>\r\n");

            sb.Append(body);

            sb.Append("\r\n</body>\r\n");
        }

        private void AddBottomHtmlTag(StringBuilder sb) => sb.Append("\r\n</html>");
    }
}
