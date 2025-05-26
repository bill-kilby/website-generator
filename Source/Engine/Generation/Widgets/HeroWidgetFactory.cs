using website_generator.Domain.Generation.Widget;
using website_generator.Engine.Generation.Widgets.Common;
using System.Text.RegularExpressions;

namespace website_generator.Engine.Generation.Widgets
{
    internal class HeroWidgetFactory : WidgetFactoryBase<HeroWidgetMetadata>
    {
        public HeroWidgetFactory(IWidgetLoader widgetLoader, IWidgetVerifier widgetVerifier) : base(widgetLoader, widgetVerifier)
        {
        }
    }
}
