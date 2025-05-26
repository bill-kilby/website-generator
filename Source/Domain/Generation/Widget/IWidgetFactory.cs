using website_generator.Engine.Generation.Widgets.Common;

namespace website_generator.Domain.Generation.Widget
{
    internal interface IWidgetFactory
    {
        public string Name { get; }

        public Widget CreateWidget(WidgetMetadata widgetType);
    }
}
