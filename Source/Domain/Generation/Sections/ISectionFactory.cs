using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Section
{
    internal interface ISectionFactory
    {
        public string Name { get; }

        public Section CreateSection();
    }
}
