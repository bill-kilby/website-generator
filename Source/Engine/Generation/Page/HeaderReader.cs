using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Page;

namespace website_generator.Engine.Generation.Page
{
    internal class HeaderReader : IHeaderReader
    {
        private readonly IHtmlVerifier _htmlVerifier;

        public HeaderReader(IHtmlVerifier htmlVerifier)
        {
            _htmlVerifier = htmlVerifier;
        }

        public string ReadHeader()
        {
            var html = File.ReadAllText("Html/Header.html");

            _htmlVerifier.Verify(html);

            return html;
        }
    }
}
