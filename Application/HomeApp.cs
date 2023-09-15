using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class HomeApp : App<HomeViewModel, Home>, IHomeApp
    {
        public HomeApp(ILogger<HomeApp> logger, IMapper mapper, IHomeRepository homeRepository) : base(logger, mapper, homeRepository)
        {

        }
    }
}
