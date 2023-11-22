using System.IO;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContextOptions<DataContext> _OptionsBuilder;
        protected readonly DataContext context;
        protected readonly DbSet<T> DbSet;

        public Repository(DataContext dataContext)
        {
            _OptionsBuilder = new DbContextOptions<DataContext>();
            context = dataContext;
            DbSet = context.Set<T>();
        }

        /// 

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose

        bool disposed = false;

        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        ///

        public async Task<int> CreateAsync(T entity)
        {
            try
            {
                await DbSet.AddAsync(entity);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar o objeto.", ex);
            }
        }

        public async Task<int> EditAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao editar o objeto.", ex);
            }
        }

        public async Task<T> FindOneAsync(int id)
        {
            try
            {
                var obj = await DbSet.FindAsync(id);
                if (context.Entry(obj).State == EntityState.Unchanged)
                {
                    context.Entry(obj).State = EntityState.Detached;
                    obj = await DbSet.FindAsync(id);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao Encontrar o objeto.", ex);
            }
        }

        // public async Task<T> FindNoTrackinOneAsync(int id)
        // {
        //     try
        //     {
        //         var items = await DbSet.AsNoTracking().FirtOrDefaultAsync(x => x.Id == id);

        //         var item = items.Find(x => ((dynamic)x).Id == id);

        //         return item;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception("Ocorreu um erro ao encontrar a entidade.", ex);
        //     }
        // }

        // public async Task<T> FindNoTrackinOneAsync(int id)
        // {
        //     try
        //     {
        //         // Obtém o tipo da entidade
        //         Type entityType = typeof(T);

        //         // Cria o parâmetro para a expressão (x => x.Id == id)
        //         ParameterExpression parameter = Expression.Parameter(entityType, "x");

        //         // Cria a expressão (x => x.Id)
        //         Expression property = Expression.Property(parameter, "Id");

        //         // Cria a expressão (x => x.Id == id)
        //         BinaryExpression equality = Expression.Equal(property, Expression.Constant(id));

        //         // Combina as expressões para criar a expressão final (x => x.Id == id)
        //         Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

        //         // Executa a consulta no DbSet utilizando a expressão criada
        //         var item = await DbSet.AsNoTracking().FirstOrDefaultAsync(lambda);

        //         return item;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception("Ocorreu um erro ao encontrar a entidade.", ex);
        //     }
        // }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            try
            {
                return await DbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao Carregar a lista.", ex);
            }
        }


        public async Task<int> Remove(T entity)
        {
            try
            {
                using var data = new DataContext(_OptionsBuilder);
                data.Set<T>().Remove(entity);
                return await data.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao remover a entidade.", ex);
            }

        }

    }
}
