using Application.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Areas
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
<<<<<<< HEAD
            MapperConfiguration mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
=======
            MapperConfiguration mappingConfig = new (mc => { mc.AddProfile(new AutoMapperProfile()); });
>>>>>>> fc19d229c883c2ee1f8833f566af6425c7684dd7
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
