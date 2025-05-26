using website_generator.Domain.Generation.Widget;
using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets.Mocks
{
    internal class WidgetFactoryMock : WidgetFactoryBase<SinglePropertyWidgetMetadataMock>
    {
        public WidgetFactoryMock(IWidgetLoader widgetLoader, IWidgetVerifier widgetVerifier, string name = "WidgetFactoryMock")
            : base(name, widgetLoader, widgetVerifier)
        {
        }
    }
}
