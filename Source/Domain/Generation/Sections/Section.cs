using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Widgets;

namespace website_generator.Domain.Generation.Section
{
    internal class Section
    {
        public List<Widget> Widgets { get; set; }

        public Section(List<Widget> widgets)
        {
            this.Widgets = widgets;
        }
    }
}
