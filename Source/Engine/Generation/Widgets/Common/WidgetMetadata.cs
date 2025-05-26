namespace website_generator.Engine.Generation.Widgets.Common
{
    internal abstract class WidgetMetadata
    {
        internal string Name;
        protected Dictionary<string, string> Values;

        public WidgetMetadata(string name)
        {
            Name = name;
            Values = new();
        }

        public HashSet<string> GetFields()
        {
            return Values.Keys.ToHashSet();
        }

        public string GetValue(string name)
        {
            if (!Values.ContainsKey(name)) throw new NullReferenceException(
                $"Missing value on the metadata - are you targetting the correct one? : {name}");
            return Values[name];
        }
    }
}
