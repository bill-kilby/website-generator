using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal class WidgetFactoryCache : IWidgetFactoryCache
    {
        private readonly Dictionary<string, IWidgetFactory> _factoryCache;

        public WidgetFactoryCache(
            List<IWidgetFactory> widgetFactories
            )
        {
            _factoryCache = new();
            foreach (var widgetFactory in widgetFactories)
            {
                _factoryCache.Add(widgetFactory.Name, widgetFactory);
            }
        }

        public IWidgetFactory GetFactory(string name) 
        {
            if (!_factoryCache.ContainsKey(name))
                throw new Exception($"WidgetFactoryCache does not contain: {name}.");

            return _factoryCache[name];
        }
    }
}
