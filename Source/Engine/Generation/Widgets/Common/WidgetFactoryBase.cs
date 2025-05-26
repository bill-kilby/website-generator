using System.Text.RegularExpressions;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal abstract class WidgetFactoryBase<TMetadata> where TMetadata : WidgetMetadata
    {
        protected readonly IWidgetLoader _widgetLoader;
        protected readonly IWidgetVerifier _widgetVerifier;
        private readonly Regex _fieldRegex = new(@"\{([A-Za-z]+)\}", RegexOptions.Compiled);

        public WidgetFactoryBase(
            IWidgetLoader widgetLoader,
            IWidgetVerifier widgetVerifier
            )
        {
            _widgetLoader = widgetLoader;
            _widgetVerifier = widgetVerifier;
        }

        public virtual Widget CreateWidget(TMetadata metadata)
        {
            _widgetVerifier.Verify(metadata);

            var html = _widgetLoader.LoadTemplateFromDisk(metadata.Name);
            html = InsertProperties(metadata, html);

            if (HasRemainingFields(html)) throw new InvalidHTMLException($"Properties remain in: {html}.");

            return new Widget(
                metadata.Name,
                html
                );
        }

        protected string InsertProperties(TMetadata metadata, string html)
        {
            var fields = metadata.GetFields();

            foreach (var field in fields)
            {
                var regex = GetTargetRegex(field);

                html = Regex.Replace(
                    html,
                    regex,
                    metadata.GetValue(field)
                    );
            }

            return html;
        }

        private bool HasRemainingFields(string html) => _fieldRegex.IsMatch(html);

        private string GetTargetRegex(string target) => $"\\{{{Regex.Escape(target)}\\}}";
    }
}