using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal abstract class WidgetMetadata
    {
        internal string Name;

        public WidgetMetadata(string name)
        {
            Name = name;
        }
    }
}
