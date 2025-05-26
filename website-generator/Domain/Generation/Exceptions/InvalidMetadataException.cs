using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Exceptions
{
    internal class InvalidMetadataException : Exception
    {
        internal InvalidMetadataException(string message) : base(message)
        {
        }
    }
}
