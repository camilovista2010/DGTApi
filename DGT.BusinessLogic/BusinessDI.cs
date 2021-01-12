using DGT.BusinessLogic.Conductor;
using DGT.BusinessLogic.Vehiculo;
using DGT.Data;
using Microsoft.Extensions.DependencyInjection;


namespace DGT.BusinessLogic
{
    public static class BusinessDI
    {
        public static IServiceCollection AddBusinessComponents(this IServiceCollection services)
        {
            services.AddDataComponents();
            services.AddScoped<IVehiculoBL, VehiculoBL>();
            services.AddScoped<IConductorBL, ConductorBL>(); 
            return services;
        }
    }
}
