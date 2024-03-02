using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using GroupLucky.Application.Contracts.Persistences;
using GroupLucky.Infrastructure.Repositories;

namespace GroupLucky.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration ) {
            services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbTransaction>(sp =>
            {
                var connection = sp.GetRequiredService<IDbConnection>();
                connection.Open();
                return connection.BeginTransaction();
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
