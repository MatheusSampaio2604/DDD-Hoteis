using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class ImagensApp : App<ImagensViewModel, Imagens>, IImagensApp
    {
        public ImagensApp(ILogger<ImagensApp> logger, IMapper mapper, IImagensRepository imagensRepository) : base(logger, mapper, imagensRepository)
        {

        }
    }
}
