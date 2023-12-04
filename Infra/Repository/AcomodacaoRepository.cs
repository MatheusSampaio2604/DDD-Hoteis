using Domain.Interfaces;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repository
{
    public class AcomodacaoRepository : Repository<Acomodacao>, IAcomodacaoRepository
    {
        public AcomodacaoRepository(DataContext context) : base(context)
        { }

        public async Task<IEnumerable<Acomodacao>> FindAcomodacoesWithPhrase(string phrase)
        {
            return await DbSet.Where(a => a.Nome.Contains(phrase) && a.Ativo == true).ToListAsync();
        }

        public async Task<Acomodacao> FindNoTrackinOneAsync(int id)
        {
<<<<<<< HEAD
            Acomodacao o = await DbSet.AsNoTracking()
=======
            Acomodacao o =  await DbSet.AsNoTracking()
>>>>>>> fc19d229c883c2ee1f8833f566af6425c7684dd7
                               .Include(a => a.Home)
                               .Include(a => a.Imagens)
                               .Include(a => a.Tarifas)
                              .FirstOrDefaultAsync(x => x.Id == id);

            return o;
        }
    }
}
