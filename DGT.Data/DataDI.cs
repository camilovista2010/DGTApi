using DGT.Data.Conductor;
using DGT.Data.Connection;
using DGT.Data.Vehiculo;
using Microsoft.Extensions.DependencyInjection;

namespace DGT.Data
{
    public static class DataDI
    {
        public static IServiceCollection AddDataComponents(this IServiceCollection services)
        {
            IConnectionFactory connectionFactory = new ConnectionFactory();
            services.AddSingleton(connectionFactory);
            services.AddScoped<IVehiculoRepository, VehiculoRepository>();
            services.AddScoped<IConductorRepository , ConductorRepository>();
            connectionFactory.InitDatabase();

            return services;
        }
    }
}
