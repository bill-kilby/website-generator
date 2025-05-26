using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Common;
using website_generator.Domain.Generation.Exceptions;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal class WidgetFactoryVerifier : IWidgetFactoryVerifier
    {
        private readonly IWidgetLoader _widgetLoader;
        private readonly IHtmlVerifier _htmlVerifier;
        private readonly Regex _fieldRegex = new(@"\{([A-Za-z]+)\}", RegexOptions.Compiled);


        private IWidgetFactory _widgetFactory;
        private WidgetMetadata _widgetMetadata;

        #pragma warning disable CS8618 // This will never be null as public method sets them.
        public WidgetFactoryVerifier(
        #pragma warning restore CS8618
            IWidgetLoader widgetLoader,
            IHtmlVerifier htmlVerifier
            )
        {
            _widgetLoader = widgetLoader;
            _htmlVerifier = htmlVerifier;
        }

        public void Verify(IWidgetFactory factory, WidgetMetadata metadata)
        {
            _widgetFactory = factory;
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
            var metadataFields = GetMetadataFields();

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
            if (metadataFields.Count - 1 == htmlFields.Count) return;

            var missing = htmlFields
                .Where(c => !metadataFields.Contains(c.Value))
                .Select(c => c.Value)
                .ToList();

            throw new InvalidMetadataException($"Metadata is missing: {missing}");
        }

        private HashSet<string> GetMetadataFields()
        {
            var fields = _widgetMetadata.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            return fields.Select(f => f.Name).ToHashSet();
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
