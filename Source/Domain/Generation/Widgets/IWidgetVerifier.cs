using website_generator.Engine.Generation.Widgets.Common;

namespace website_generator.Domain.Generation.Widgets
{
    internal interface IWidgetVerifier
    {
        public void Verify(WidgetMetadata metadata);
    }
}
