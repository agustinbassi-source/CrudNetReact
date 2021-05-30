using Bassi.ADO.Utilities;
using Bassi.API.Configuration;
using Bassi.CrudBassi.Repository;
using Bassi.CrudBassi.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bassi.CrudBassi.API.Dependencies
{
    public static class RepositoryDependencyResolver
    {
        public static void AddRepositories(this IServiceCollection services, IAppSettings configuration)
        {
            services.AddScoped<IDBContext>(s => new DBContext(configuration, "CrudBassi"));
            services.AddScoped<ISqlStoreExcuter, SqlStoreExcuter>();

            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IReceiptDetailRepository, ReceiptDetailRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

        }
    }
}

