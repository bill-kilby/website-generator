using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Page
{
    internal interface IPageWriter
    {
        public void Write(string html);
    }
}
