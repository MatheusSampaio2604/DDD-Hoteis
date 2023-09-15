using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application
{
    public class HomeApp : App<HomeViewModel, Home>, IHomeApp
    {
        public HomeApp(ILogger<HomeApp> logger, IMapper mapper, IHomeRepository homeRepository) : base(logger, mapper, homeRepository)
        {

        }
    }
}
