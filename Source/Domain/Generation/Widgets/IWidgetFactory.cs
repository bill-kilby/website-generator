using website_generator.Engine.Generation.Widgets.Common;

namespace website_generator.Domain.Generation.Widgets
{
    internal interface IWidgetFactory
    {
        public string Name { get; }

        public Widget CreateWidget(WidgetMetadata widgetType);
    }
}
