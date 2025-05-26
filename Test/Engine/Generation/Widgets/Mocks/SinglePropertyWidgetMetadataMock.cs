using website_generator.Engine.Generation.Widgets.Common;

namespace Test.Engine.Generation.Widgets.Mocks
{
    internal class SinglePropertyWidgetMetadataMock : WidgetMetadata
    {
        public SinglePropertyWidgetMetadataMock(string name = "SinglePropertyWidgetMetadataMock") : base(name)
        {
            Values = new()
            {
                { "PropertyOne", "PropertyOneValue" }
            };
        }
    }
}
