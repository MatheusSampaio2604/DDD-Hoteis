using System.Linq;
using Application.ViewModel;
using AutoMapper;
using Domain.Models;

namespace Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Acomodacao, AcomodacaoViewModel>()
            .ForMember(
                dest => dest.ImagensID,
                opt => opt.MapFrom(
                    src => src.Imagens.Where(x => x.Id_Acomodacao == src.Id).Select(x => x.RotaImagem).ToList()))
            .ForMember(
                dest => dest.Imagens,
                opt => opt.MapFrom(
                    src => src.Imagens))
            // .ForPath(
            //     dest => dest.Home.Acomodacao,
            //     opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Home, HomeViewModel>().ReverseMap();

            CreateMap<Tarifas, TarifasViewModel>().ReverseMap();

            CreateMap<Imagens, ImagensViewModel>().ReverseMap();

        }
    }
}
