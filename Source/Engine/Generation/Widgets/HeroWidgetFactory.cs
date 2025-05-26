using website_generator.Domain.Generation.Widgets;
using website_generator.Engine.Generation.Widgets.Common;

namespace website_generator.Engine.Generation.Widgets
{
    internal class HeroWidgetFactory : WidgetFactoryBase<HeroWidgetMetadata>
    {
        public HeroWidgetFactory(IWidgetLoader widgetLoader, IWidgetVerifier widgetVerifier, string name = "HeroWidget")
            : base(name, widgetLoader, widgetVerifier)
        {
        }
    }
}
