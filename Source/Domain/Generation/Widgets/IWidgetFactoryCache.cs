namespace website_generator.Domain.Generation.Widgets
{
    internal interface IWidgetFactoryCache
    {
        public IWidgetFactory GetFactory(string name);

        public bool Contains(string name);
    }
}
