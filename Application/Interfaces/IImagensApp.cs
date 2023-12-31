﻿using Application.ViewModel;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImagensApp : IApp<ImagensViewModel, Imagens>
    {
        Task<List<ImagensViewModel>> FindOneAsyncFindImageFromAcomodationID(int id);
        Task<int> CreateManyAsync(IEnumerable<ImagensViewModel> imagensViewModel);
        Task<int> RemoveManyAsync(IEnumerable<ImagensViewModel> imagensViewModel);
    }


}
