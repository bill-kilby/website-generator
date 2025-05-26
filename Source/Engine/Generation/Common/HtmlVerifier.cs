using HtmlAgilityPack;
using System.Text;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;

namespace website_generator.Engine.Generation.Common
{
    internal class HtmlVerifier : IHtmlVerifier
    {
        public void Verify(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            if (doc.ParseErrors.Count() > 0)
            {
                var parseErrors = new StringBuilder();
                foreach ( var error in doc.ParseErrors)
                {
                    parseErrors.AppendLine(error.Reason);
                }

                throw new InvalidHTMLException(parseErrors.ToString());
            }
        }
    }
}
