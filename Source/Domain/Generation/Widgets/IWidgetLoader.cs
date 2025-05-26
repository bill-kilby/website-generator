namespace website_generator.Domain.Generation.Widgets
{
    internal interface IWidgetLoader
    {
        public string LoadTemplateFromDisk(string widgetName);
    }
}
