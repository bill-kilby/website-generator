using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Sections;
using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Sections.Common;

namespace Test.Engine.Generation.Sections.Mocks
{
    internal class SectionFactoryMock : SectionFactoryBase
    {
        public SectionFactoryMock(ISectionFactoryVerifier sectionFactoryVerifier, string name = "SectionFactoryMock")
            : base(name, sectionFactoryVerifier)
        {
        }

        protected override Section _section => new Section(
                new()
                {
                    "TestWidgetOne",
                    "TestWidgetTwo"
                }
            );
    }
}
