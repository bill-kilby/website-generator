using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website_generator.Domain.Generation;
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
