using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        void Remove(TModel model);
    }

    public interface IApp<TModel>
    where TModel : class
    { 
        Task<TModel> CreateAsync(TModel viewModel);
        Task<TModel> EditAsync(TModel viewModel);
        Task<TModel> FindOneAsync(int id);
        Task<TModel> FindNoTrackinOneAsync(int id);
        Task<IEnumerable<TModel>> FindAllAsync();
         void Remove(TModel model);
    }
}
