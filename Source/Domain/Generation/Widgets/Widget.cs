﻿namespace website_generator.Domain.Generation.Widgets
{
    internal class Widget
    {
        public readonly string Name;
        public readonly string Content;

        public Widget(string name, string content)
        {
            Name = name;
            Content = content;
        }
    }
}
