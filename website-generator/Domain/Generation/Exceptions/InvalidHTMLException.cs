using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Exceptions
{
    internal class InvalidHTMLException : Exception
    {
        public string ParseErrors { get; }

        internal InvalidHTMLException(string parseErrors, string message = "HTML had parse errors!") : base(message)
        {
            ParseErrors = parseErrors;
        }
    }
}
