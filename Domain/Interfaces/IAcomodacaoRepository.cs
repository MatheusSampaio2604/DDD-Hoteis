using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAcomodacaoRepository : IRepository<Acomodacao>
    {
       Task<IEnumerable<Acomodacao>> FindAcomodacoesWithPhrase(string phrase);
    }
}
