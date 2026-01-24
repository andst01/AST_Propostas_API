using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Propostas.Infra.Data.Contexto;

namespace Propostas.Infra.CrossCuting.Config
{
    public static class DataBaseConfig
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<PropostaDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnectionString");
                options.UseSqlServer(connectionString);
            });
            
        }
    }
}
