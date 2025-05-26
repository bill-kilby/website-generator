namespace website_generator.Domain.Generation.Sections
{
    internal class Section
    {
        public List<string> Widgets { get; set; }

        public Section(List<string> widgets)
        {
            this.Widgets = widgets;
        }
    }
}
