using Microsoft.Extensions.DependencyInjection;

namespace Propostas.Infra.CrossCuting.Config
{
    public static class InjecaoDependeciaConfig
    {
        public static void AddInjecaoDependeciaConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);


        }
    }
}
