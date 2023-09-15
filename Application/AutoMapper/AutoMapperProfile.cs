using Application.ViewModel;
using AutoMapper;
using Domain.Models;
using System.Linq;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Acomodacao, AcomodacaoViewModel>()
                .ForMember(
                dest => dest.Ativo,
                opt => opt.MapFrom(
                src => src.Ativo ? "true" : "false"))
                
                .ReverseMap();

            CreateMap<Home, HomeViewModel>().ReverseMap();
            
            CreateMap<Tarifas, TarifasViewModel>()
                .ForMember(
                dest => dest.Ativo,
                opt => opt.MapFrom(
                src => src.Ativo ? "true" : "false"))

                .ReverseMap();





        }
    }
}
