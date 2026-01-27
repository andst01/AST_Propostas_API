using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Propostas.Application;
using Propostas.Application.Interfaces;
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

            #endregion

            services.AddScoped<PropostaDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        }
    }
}
