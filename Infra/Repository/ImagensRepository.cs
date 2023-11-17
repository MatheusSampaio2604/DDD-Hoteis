using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

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
                var items = await DbSet.AsNoTracking().ToListAsync();
                if (items.Any())
                {
                    var item = items.Where(x => ((dynamic)x).Id_Acomodacao == id);
                    return item;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao encontrar a entidade.", ex);
            }
        }

    }
}
