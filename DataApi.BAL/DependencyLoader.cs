using DataApi.BAL.Services;
using DataApi.BAL.Services.Defaults;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL
{
    public static class DependencyLoader
    {
        public static void Load(this IServiceCollection services)
        {

            DataApi.DAL.DependencyLoader.Load(services);
            services.AddScoped<IPersonService, PersonService>();

        }
    }
}
