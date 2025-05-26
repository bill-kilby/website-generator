using System.Text.RegularExpressions;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal abstract class WidgetFactoryBase<TMetadata> : IWidgetFactory
        where TMetadata : WidgetMetadata
    {
        public string Name { get; }

        protected readonly IWidgetLoader _widgetLoader;
        protected readonly IWidgetVerifier _widgetVerifier;
        protected readonly Regex _fieldRegex = new(@"\{([A-Za-z]+)\}", RegexOptions.Compiled);

        public WidgetFactoryBase(
            string name,
            IWidgetLoader widgetLoader,
            IWidgetVerifier widgetVerifier
            )
        {
            Name = name;
            _widgetLoader = widgetLoader;
            _widgetVerifier = widgetVerifier;
        }

        public Widget CreateWidget(WidgetMetadata metadata)
        {
            return GenerateTypedWidget((TMetadata)metadata);
        }

        private Widget GenerateTypedWidget(TMetadata metadata)
        {
            _widgetVerifier.Verify(metadata);

            var html = GenerateWidgetHtml(metadata);

            if (HasRemainingFields(html))
                throw new InvalidHTMLException($"Properties remain in: {html}.");

            return new Widget(metadata.Name, html);
        }

        private string GenerateWidgetHtml(TMetadata metadata)
        {
            var template = _widgetLoader.LoadTemplateFromDisk(metadata.Name);
            return ReplaceTemplateFields(metadata, template);
        }

        private string ReplaceTemplateFields(TMetadata metadata, string html)
        {
            foreach (var field in metadata.GetFields())
            {
                var regex = GetTargetRegex(field);
                html = Regex.Replace(html, regex, metadata.GetValue(field));
            }

            return html;
        }

        private bool HasRemainingFields(string html) => _fieldRegex.IsMatch(html);

        private string GetTargetRegex(string target) => $"\\{{{Regex.Escape(target)}\\}}";
    }
}