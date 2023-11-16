using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApp<TViewModel, TModel>

     where TViewModel : class

     where TModel : class

    {
        Task<TModel> CreateAsync(TViewModel viewModel);
        Task<TViewModel> EditAsync(TViewModel entity);
        Task<TViewModel> FindOneAsync(int id);
        Task<TViewModel> FindNoTrackinOneAsync(int id);
        Task<IEnumerable<TViewModel>> FindAllAsync();
        Task<int> Remove(TModel model);
    }

    public interface IApp<TModel>
    where TModel : class
    {
        Task<TModel> CreateAsync(TModel viewModel);
        Task<TModel> EditAsync(TModel viewModel);
        Task<TModel> FindOneAsync(int id);
        Task<TModel> FindNoTrackinOneAsync(int id);
        Task<IEnumerable<TModel>> FindAllAsync();
        Task<int> Remove(TModel model);
    }
}
