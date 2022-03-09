using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using GroceryListSystem.DAL;
using GroceryListSystem.BLL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
#endregion
namespace GroceryListSystem
{
    public static class GroceryBackEndExtensions
    {
        public static void BackendDependencies(this IServiceCollection services,
    Action<DbContextOptionsBuilder> options)
        {
            //register the DBContext class with the service collection
            services.AddDbContext<GroceryListContext>(options);

            //add any services that you create in the class library using .AddTransient<T>(....)
            //services.AddTransient<AboutService>((serviceProvider) =>
            //{
            //    //retrieve the registered DbContext done in AddDbContext<>()
            //    var context = serviceProvider.GetRequiredService<ChinookContext>();
            //    return new AboutService(context);
            //});
                    }

    }
}
