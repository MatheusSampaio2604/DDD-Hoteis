using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
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
            //List<ImagensViewModel> viewModels = _mapper.Map<List<Imagens>, List<ImagensViewModel>>(images);
            IEnumerable<Imagens> images = await _IImagensRepository.FindImageFromAcomodationID(id);

            List<ImagensViewModel> imagesViewModels = images.Select(img => new ImagensViewModel
            {
                Id = img.Id,
                Id_Acomodacao = img.Id_Acomodacao,
                Nome = img.Nome,
                RotaImagem = img.RotaImagem,
            }).ToList(); // Convertendo para List<ImagensViewModel>

            return imagesViewModels;
        }

    }
}
