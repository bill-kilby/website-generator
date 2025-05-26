using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Widget
{
    internal interface IWidget
    {
        public string Name { get; set; }
        public string Content { get; set; } 
    }
}
