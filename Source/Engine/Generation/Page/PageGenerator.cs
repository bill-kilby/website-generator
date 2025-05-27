using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Page;
using website_generator.Domain.Generation.Sections;

namespace website_generator.Engine.Generation.Page
{
    internal class PageGenerator : IPageGenerator
    {
        private readonly List<ISectionFactory> _sectionFactories;

        public PageGenerator(
            List<ISectionFactory> sectionFactories
            )
        {
            _sectionFactories = sectionFactories;
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            foreach(var factory in _sectionFactories )
            {
                var section = factory.CreateSection();

                sb.Append( section );
            }

            return sb.ToString();
        }
    }
}
