using DataApi.DAL.Repositories;
using DataApi.DAL.Repositories.Defaults;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.DAL
{
   public  class DependencyLoader
    {
        public static void Load(IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();

        }
    }
}
