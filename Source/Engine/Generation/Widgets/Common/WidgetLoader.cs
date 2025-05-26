using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal class WidgetLoader : IWidgetLoader
    {
        public string LoadTemplateFromDisk(string widgetName)
        {
            var path = GetWidgetPath(widgetName);

            return File.ReadAllText(path);
        }

        private string GetWidgetPath(string widgetName) => $"./Html/{widgetName}.html";
    }
}