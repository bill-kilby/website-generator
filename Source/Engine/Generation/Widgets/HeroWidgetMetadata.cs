using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Engine.Generation.Widgets.Common;

namespace website_generator.Engine.Generation.Widgets
{
    internal class HeroWidgetMetadata : WidgetMetadata
    {
        public HeroWidgetMetadata(string name = "HeroWidget") : base(name)
        {
            Values = new()
            {
                { "PropertyOne", "TempTest" }
            };
        }
    }
}
