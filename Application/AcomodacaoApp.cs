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
    public class AcomodacaoApp : App<AcomodacaoViewModel, Acomodacao>, IAcomodacaoApp
    {
        public AcomodacaoApp(ILogger<AcomodacaoApp> logger, IMapper mapper, IAcomodacaoRepository acomodacaoRepository) : base(logger, mapper, acomodacaoRepository)
        {

        }
    }
}