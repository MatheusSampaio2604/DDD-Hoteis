using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public virtual async Task<List<ImagensViewModel>> FindOneAsyncFindImageFromAcomodationID(int id)
        {
            IEnumerable<Imagens> images = await _IImagensRepository.FindImageFromAcomodationID(id);

            List<ImagensViewModel> imagesViewModels = images.Select(img => new ImagensViewModel
            {
                Id = img.Id,
                Id_Acomodacao = img.Id_Acomodacao,
                Nome = img.Nome,
                RotaImagem = img.RotaImagem,
            }).ToList();

            return imagesViewModels;
        }

        public virtual async Task<int> CreateManyAsync(IEnumerable<ImagensViewModel> imagensViewModel)
        {
            try
            {
                IEnumerable<Imagens> imagens = _mapper.Map<IEnumerable<ImagensViewModel>, IEnumerable<Imagens>>(imagensViewModel);
                return await _IImagensRepository.CreateManyAsync(imagens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

        public virtual async Task<int> RemoveManyAsync(IEnumerable<ImagensViewModel> imagensViewModel)
        {
            try
            {
                IEnumerable<Imagens> imagens = _mapper.Map<IEnumerable<ImagensViewModel>, IEnumerable<Imagens>>(imagensViewModel);
                return await _IImagensRepository.RemoveManyAsync(imagens);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0; 
            }
        }

    }
}
