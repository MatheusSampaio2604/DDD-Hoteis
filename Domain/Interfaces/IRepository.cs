using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class

    {

        Task<int> CreateAsync(T entity);

        Task<int> EditAsync(T entity);

        Task<T> FindOneAsync(int id);

        Task<T> FindNoTrackinOneAsync(int id);

        Task<IEnumerable<T>> FindAllAsync();

        Task<int> Remove(T entity);



    }






}
