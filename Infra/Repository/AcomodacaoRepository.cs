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
        {  }

        public async Task<IEnumerable<Acomodacao>> FindAcomodacoesWithPhrase(string phrase)
        {
            return await DbSet.Where(a => a.Nome.Contains(phrase) && a.Ativo == true).ToListAsync();
        }

        public async Task<Acomodacao> FindNoTrackinOneAsync(int id)
        {
            Acomodacao o =  await DbSet.AsNoTracking()
                               .Include(a => a.Home)
                               .Include(a => a.Imagens)
                               .Include(a => a.Tarifas)
                              .FirstOrDefaultAsync(x => x.Id == id);
        
            return o;
        }
    }
}
