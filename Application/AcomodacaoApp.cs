using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class AcomodacaoApp : App<AcomodacaoViewModel, Acomodacao>, IAcomodacaoApp
    {
        private readonly IAcomodacaoRepository _iAcomodacaoRepository;
        private readonly IMapper _map;

        public AcomodacaoApp(ILogger<AcomodacaoApp> logger, IMapper mapper, IAcomodacaoRepository acomodacaoRepository) : base(logger, mapper, acomodacaoRepository)
        {
            _iAcomodacaoRepository = acomodacaoRepository;
            _map = mapper;
        }

        public virtual async Task<IEnumerable<AcomodacaoViewModel>> FindAcomodacoesWithPhrase(string phrase)
        {

            try
            {
                IEnumerable<Acomodacao> models = await _iAcomodacaoRepository.FindAcomodacoesWithPhrase(phrase);

                IEnumerable<AcomodacaoViewModel> modelViews = _map.Map<IEnumerable<Acomodacao>, IEnumerable<AcomodacaoViewModel>>(models);

                return modelViews;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new List<AcomodacaoViewModel>();
        }

        public virtual async Task<AcomodacaoViewModel> FindNoTrackinOneAsync(int id)
        {
            try
            {
                Acomodacao models = await _iAcomodacaoRepository.FindNoTrackinOneAsync(id)/*.Result*/;
                
                AcomodacaoViewModel modelViews = _map.Map<Acomodacao, AcomodacaoViewModel>(models);

                return modelViews;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

        }

    }
}