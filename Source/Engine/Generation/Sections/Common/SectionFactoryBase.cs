using website_generator.Domain.Generation.Sections;

namespace website_generator.Engine.Generation.Sections.Common
{
    internal abstract class SectionFactoryBase : ISectionFactory
    {
        public string Name { get; }

        private readonly ISectionFactoryVerifier _sectionFactoryVerifier;

        protected abstract Section _section { get; }

        public SectionFactoryBase(
            string name,
            ISectionFactoryVerifier sectionFactoryVerifier
            )
        {
            Name = name;
            _sectionFactoryVerifier = sectionFactoryVerifier;
        }

        public Section CreateSection()
        {
            _sectionFactoryVerifier.Verify(_section);

            return _section;
        }
    }
}
