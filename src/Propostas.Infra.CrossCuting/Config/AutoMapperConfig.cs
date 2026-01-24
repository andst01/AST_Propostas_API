using Microsoft.Extensions.DependencyInjection;
using Propostas.Infra.CrossCuting.AutoMapper;

namespace Propostas.Infra.CrossCuting.Config
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMappingConfig(this IServiceCollection services)
        {
            if(services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile),
                                   typeof(ViewModelToDomainMappingProfile));
        }
    }
}
