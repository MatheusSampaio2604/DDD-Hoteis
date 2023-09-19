using Application.ViewModel;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Acomodacao, AcomodacaoViewModel>().ReverseMap();

            CreateMap<Home, HomeViewModel>().ReverseMap();

            CreateMap<Tarifas, TarifasViewModel>().ReverseMap();





        }
    }
}
