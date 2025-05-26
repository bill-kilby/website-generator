using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation.Widget;

namespace website_generator.Engine.Generation.Widgets.Common
{
    internal class WidgetFactoryVerifier : IWidgetFactoryVerifier
    {
        private IWidgetLoader _widgetLoader;

        public WidgetFactoryVerifier(IWidgetLoader widgetLoader)
        {
            _widgetLoader = widgetLoader;
        }

        public void Verify(IWidgetFactory factory, WidgetMetadata metadata)
        {
            throw new NotImplementedException();
        }
    }
}
