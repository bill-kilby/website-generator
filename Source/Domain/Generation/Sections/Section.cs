using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Widgets;

namespace website_generator.Domain.Generation.Sections
{
    internal class Section
    {
        public List<string> Widgets { get; set; }

        public Section(List<string> widgets)
        {
            this.Widgets = widgets;
        }
    }
}
