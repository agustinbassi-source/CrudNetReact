using Bassi.CrudBassi.Manager;
using Bassi.CrudBassi.Manager.Interface;
using Microsoft.Extensions.DependencyInjection;
using Bassi.ADO.Utilities;
 

namespace Bassi.CrudBassi.API.Dependencies
{
    public static class ManagerDependencyResolver
    {
        public static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IReceiptManager, ReceiptManager>();
            services.AddScoped<IClientManager, ClientManager>();
            services.AddScoped<IProductManager, ProductManager>();
        }
    }
}

