using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Sections;
using website_generator.Domain.Generation.Widgets;

namespace website_generator.Engine.Generation.Sections.Common
{
    internal class SectionFactoryVerifier : ISectionFactoryVerifier
    {
        private IWidgetFactoryCache _widgetFactoryCache;

        public SectionFactoryVerifier(IWidgetFactoryCache widgetFactoryCache)
        {
            _widgetFactoryCache = widgetFactoryCache;
        }

        public void Verify(Section section)
        {
            foreach (var widget in section.Widgets)
            {
                if (!_widgetFactoryCache.Contains(widget))
                    throw new InvalidSectionException($"A widget is not registered: {widget}");
            }
        }
    }
}
