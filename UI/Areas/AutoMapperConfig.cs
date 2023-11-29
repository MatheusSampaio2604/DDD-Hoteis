using Application.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace UI.Areas
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            MapperConfiguration mappingConfig = new (mc => { mc.AddProfile(new AutoMapperProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
