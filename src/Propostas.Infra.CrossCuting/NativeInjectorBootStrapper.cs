using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Propostas.Application;
using Propostas.Application.DTO;
using Propostas.Application.Interfaces;
using Propostas.Application.Interfaces.Map;
using Propostas.Application.Map;
using Propostas.Application.Request;
using Propostas.Domain.Entidade;
using Propostas.Domain.Interfaces;
using Propostas.Infra.Data.Contexto;
using Propostas.Infra.Data.Repositorio;

namespace Propostas.Infra.CrossCuting
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repositorio

            services.AddScoped(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddScoped<IPropostaRepositorio, PropostaRepositorio>();


            #endregion

            #region Aplicacao

            services.AddScoped(typeof(IAppBase<, ,>), typeof(AppBase<, ,>));
            services.AddScoped<IPropostaApp, PropostaApp>();

            services.AddScoped<IMapBase<Proposta, PropostaRequest>, PropostaRequestToEntity>();
            services.AddScoped<IMapBase<PropostaDTO, Proposta>, PropostaEntityToDto>();

            #endregion

            services.AddScoped<PropostaDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }
    }
}
