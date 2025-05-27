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

        public PageWriter(
            IHtmlVerifier htmlVerifier
            )
        {
            _htmlVerifier = htmlVerifier;
        }

        public void Write(string html)
        {
            throw new NotImplementedException();
        }
    }
}
