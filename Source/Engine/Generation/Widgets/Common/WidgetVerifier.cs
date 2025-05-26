using System.Text.RegularExpressions;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal class WidgetVerifier : IWidgetVerifier
    {
        private readonly IWidgetLoader _widgetLoader;
        private readonly IHtmlVerifier _htmlVerifier;
        private readonly Regex _fieldRegex = new(@"\{([A-Za-z]+)\}", RegexOptions.Compiled);

        private WidgetMetadata _widgetMetadata;

        #pragma warning disable CS8618 // This will never be null as public method sets them.
        public WidgetVerifier(
        #pragma warning restore CS8618
            IWidgetLoader widgetLoader,
            IHtmlVerifier htmlVerifier
            )
        {
            _widgetLoader = widgetLoader;
            _htmlVerifier = htmlVerifier;
        }

        public void Verify(WidgetMetadata metadata)
        {
            _widgetMetadata = metadata;

            VerifyHTML();
            VerifyMetadata();
        }

        private void VerifyHTML()
        {
            var html = _widgetLoader.LoadTemplateFromDisk(_widgetMetadata.Name);

            _htmlVerifier.Verify(html);
        }

        private void VerifyMetadata()
        {
            var html = _widgetLoader.LoadTemplateFromDisk(_widgetMetadata.Name);

            var htmlFields = GetHtmlFields(html);
            var metadataFields = _widgetMetadata.GetFields();

            VerifyFields(metadataFields, htmlFields);
        }

        private void VerifyFields(HashSet<string> metadataFields, HashSet<Match> htmlFields)
        {
            VerifyFieldCount(metadataFields, htmlFields);
            VerifyFieldNames(metadataFields, htmlFields);
        }

        private void VerifyFieldNames(HashSet<string> metadataFields, HashSet<Match> htmlFields)
        {
            foreach(var field in htmlFields)
            {
                var inner = field.Groups[1].Value;

                if (!metadataFields.Contains(inner))
                {
                    throw new InvalidMetadataException($"Name does not exist in both metadata and HTML: {inner}.");
                }
            }
        }
        
        private void VerifyFieldCount(HashSet<string> metadataFields, HashSet<Match> htmlFields)
        {
            if (metadataFields.Count == htmlFields.Count) return;

            var missing = htmlFields
                .Where(c => !metadataFields.Contains(c.Value))
                .Select(c => c.Value)
                .ToArray();

            throw new InvalidMetadataException($"Metadata is missing: {string.Join(", \n", missing)}");
        }

        private HashSet<Match> GetHtmlFields(string html)
        {
            return _fieldRegex
                .Matches(html)
                .Distinct()
                .ToHashSet();
        }
    }
}
