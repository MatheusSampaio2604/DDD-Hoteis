using Application.ViewModel;
using Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImagensApp : IApp<ImagensViewModel, Imagens> 
    {
        Task<List<ImagensViewModel>> FindOneAsyncFindImageFromAcomodationID(int id);
    }


}
