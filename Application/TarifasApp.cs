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
    public class TarifasApp : App<TarifasViewModel, Tarifas>, ITarifasApp
    {
        public TarifasApp(ILogger<TarifasApp> logger, IMapper mapper, ITarifasRepository tarifasRepository) : base(logger, mapper, tarifasRepository)
        {

        }
    }
}
