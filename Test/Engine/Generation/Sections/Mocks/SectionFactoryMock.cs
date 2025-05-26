using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Section;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections.Mocks
{
    internal class SectionFactoryMock : SectionFactoryBase
    {
        public SectionFactoryMock(IWidgetFactoryCache widgetFactoryCache, string name = "SectionFactoryMock")
            : base(name, widgetFactoryCache)
        {
        }

        public override Section CreateSection()
        {
            return new Section(
                new()
                {
                    "TestWidgetOne",
                    "TestWidgetTwo"
                }
            );
        }
    }
}
