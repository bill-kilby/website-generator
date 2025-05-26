using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Common
{
    internal interface IHtmlVerifier
    {
        public void Verify(string html);
    }
}
