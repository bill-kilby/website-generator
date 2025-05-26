namespace website_generator.Domain.Generation.Sections
{
    internal interface ISectionFactory
    {
        public string Name { get; }

        public Section CreateSection();
    }
}
