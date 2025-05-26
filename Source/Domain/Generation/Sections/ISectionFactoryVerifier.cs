using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website_generator.Domain.Generation.Sections
{
    internal interface ISectionFactoryVerifier
    {
        public void Verify(Section section);
    }
}
