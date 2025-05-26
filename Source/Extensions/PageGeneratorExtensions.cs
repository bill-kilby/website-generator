using Microsoft.Extensions.DependencyInjection;
using website_generator.Domain.Generation.Page;
using website_generator.Engine.Generation.Page;

namespace website_generator.Extensions
{
    internal static class PageGeneratorExtensions
    {
        public static IServiceCollection AddPageGeneratorExtensions(this IServiceCollection services)
        {
            services.AddSingleton<IPageGenerator, PageGenerator>();

            return services;
        }

    }
}
