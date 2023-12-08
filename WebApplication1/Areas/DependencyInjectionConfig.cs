using Application;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Interfaces;
using Identity.Data;
using Identity.Services;
using Infra.Context;
using Infra.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services;

namespace WebApi.Areas
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();
            services.AddScoped<ImageService>();
            services.AddScoped<IAcomodacaoRepository, AcomodacaoRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<IImagensRepository, ImagensRepository>();
            services.AddScoped<ITarifasRepository, TarifasRepository>();

            services.AddScoped<IAcomodacaoApp, AcomodacaoApp>();
            services.AddScoped<IHomeApp, HomeApp>();
            services.AddScoped<IImagensApp, ImagensApp>();
            services.AddScoped<ITarifasApp, TarifasApp>();


            //******************************************************************* Asp.Net Identity*******************************************************************
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();


            //*******************************************************************Asp.Net Identity*******************************************************************

            return services;
        }
    }
}
