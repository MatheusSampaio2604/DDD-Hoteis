﻿using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class ImagensRepository : Repository<Imagens>, IImagensRepository
    {
        public ImagensRepository(DataContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Imagens>> FindImageFromAcomodationID(int id)
        {
            try
            {
                List<Imagens> items = await DbSet.AsNoTracking().Where(x => x.Id_Acomodacao == id).ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao encontrar a entidade.", ex);
            }
        }

        public async Task<int> CreateManyAsync(IEnumerable<Imagens> imagens)
        {
            try
            {
                DbSet.AddRange(imagens);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                List<string> errorMessages = ex.Entries
                    .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    .Select(entry => $"{entry.Entity.GetType().Name}: {string.Join(", ", entry.CurrentValues.Properties.Select(prop => $"{prop.Name}='{entry.CurrentValues[prop]}''"))}")
                    .ToList();

                string fullErrorMessage = string.Join("; ", errorMessages);
                string exceptionMessage = $"Ocorreu um erro ao adicionar objetos. Detalhes: {fullErrorMessage}";

                throw new Exception(exceptionMessage, ex);
            }
        }

        public async Task<int> RemoveManyAsync(IEnumerable<Imagens> imagens)
        {
            try
            {
                DbSet.RemoveRange(imagens);
                return await SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                List<string> errorMessages = ex.Entries
                    .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
                    .Select(entry => $"{entry.Entity.GetType().Name}: {string.Join(", ", entry.CurrentValues.Properties.Select(prop => $"{prop.Name}='{entry.CurrentValues[prop]}''"))}")
                    .ToList();

                string fullErrorMessage = string.Join("; ", errorMessages);
                string exceptionMessage = $"Ocorreu um erro ao Remover objetos. Detalhes: {fullErrorMessage}";

                throw new Exception(exceptionMessage, ex);
            }
        }


    }
}
