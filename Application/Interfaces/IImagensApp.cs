using Application.ViewModel;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImagensApp : IApp<ImagensViewModel, Imagens> 
    {
        Task<ImagensViewModel> FindOneAsyncFindImageFromAcomodationID(int id);
    }


}
