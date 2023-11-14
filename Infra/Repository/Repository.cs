using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext context;
        protected readonly DbSet<T> DbSet;

        public Repository(DataContext dataContext)
        {
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
            await DbSet.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> EditAsync(T entity)
        {
            DbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<T> FindOneAsync(int id)
        {
            var obj = await DbSet.FindAsync(id);
            if (context.Entry(obj).State == EntityState.Unchanged)
            {
                context.Entry(obj).State = EntityState.Detached;
                obj = await DbSet.FindAsync(id);
            }
            return obj;
        }

        public async Task<T> FindNoTrackinOneAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }


        public void Remove(T entity)
        {
            try
            {
                DbSet.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir entidade {typeof(T).Name}: {ex.Message}");
                // Você pode lançar a exceção novamente ou tomar alguma outra ação adequada.
            }
        }

    }
}
