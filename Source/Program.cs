using Microsoft.Extensions.DependencyInjection;
using website_generator.Domain.Generation.Page;
using website_generator.Extensions;

namespace website_generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddPageGeneratorExtensions();
            // TODO: Add more services here.

            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IPageGenerator>()
                .Generate();
        }
    }
}
