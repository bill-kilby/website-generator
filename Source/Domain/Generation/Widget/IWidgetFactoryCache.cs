namespace website_generator.Domain.Generation.Widget
{
    internal interface IWidgetFactoryCache
    {
        public IWidgetFactory GetFactory(string name);
    }
}
