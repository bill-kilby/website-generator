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
        private IWidgetLoader _widgetLoader;
        private IHtmlVerifier _htmlVerifier;

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
            
            var captures = Regex.Matches(html, @"\{([a-zA-Z]+)\}");
            var distinctCaptures = captures.Distinct();

            var fields = GetFields();

            VerifyFields(fields, distinctCaptures);
        }

        private void VerifyFields(HashSet<string> fields, IEnumerable<Match> captures)
        {
            VerifyFieldCount(fields, captures);

            VerifyFieldNames(fields, captures);
        }

        private void VerifyFieldNames(HashSet<string> fields, IEnumerable<Match> captures)
        {
            foreach(var capture in captures)
            {
                var inner = capture.Groups[1].Value;

                if (!fields.Contains(inner))
                {
                    throw new InvalidMetadataException($"Name does not exist in both metadata and HTML: {capture.Value}.");
                }
            }
        }
        
        private void VerifyFieldCount(HashSet<string> fields, IEnumerable<Match> captures)
        {
            if (fields.Count() - 1 == captures.Count()) return;

            var missing = captures
                .Where(c => !fields.Contains(c.Value))
                .Select(c => c.Value)
                .ToList();

            throw new InvalidMetadataException($"Metadata is missing: {missing}");
        }

        private HashSet<string> GetFields()
        {
            var type = _widgetMetadata.GetType();

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            return new HashSet<string>(fields.Select(f => f.Name));
        }
    }
}
