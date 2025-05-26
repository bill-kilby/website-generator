using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets.Mocks
{
    internal class SinglePropertyWidgetMetadataMock : WidgetMetadata
    {
        public string PropertyOne;

        public SinglePropertyWidgetMetadataMock(string name, string propertyOne) : base(name)
        {
            PropertyOne = propertyOne;
        }
    }
}
