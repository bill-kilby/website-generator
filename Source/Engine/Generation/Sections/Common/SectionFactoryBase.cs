using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Section;
using website_generator.Domain.Generation.Widgets;

namespace website_generator.Engine.Generation.Sections.Common
{
    internal abstract class SectionFactoryBase
    {
        public string Name { get; }

        private readonly IWidgetFactoryCache _widgetFactoryCache;

        public SectionFactoryBase(
            string name,
            IWidgetFactoryCache widgetFactoryCache
            )
        {
            Name = name;
            _widgetFactoryCache = widgetFactoryCache;
        }

        public abstract Section CreateSection();
    }
}
