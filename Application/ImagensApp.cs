using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application
{
    public class ImagensApp : App<ImagensViewModel, Imagens>, IImagensApp
    {
        protected readonly IImagensRepository _IImagensRepository;
        public ImagensApp(ILogger<ImagensApp> logger, IMapper mapper, IImagensRepository imagensRepository) : base(logger, mapper, imagensRepository)
        {
            _IImagensRepository = imagensRepository;
        }

        public virtual async Task<ImagensViewModel> FindOneAsyncFindImageFromAcomodationID(int id)
        {
            Imagens map = (Imagens)await _IImagensRepository.FindImageFromAcomodationID(id);

            ImagensViewModel mapper = _mapper.Map<Imagens, ImagensViewModel>(map);

            return mapper;
        }
    }
}
