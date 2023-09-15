using Domain.Interfaces;
using Domain.Models;
using Infra.Context;

namespace Infra.Repository
{
    public class AcomodacaoRepository : Repository<Acomodacao>, IAcomodacaoRepository
    {
        public AcomodacaoRepository(DataContext context) : base(context)
        {

        }
    }
}
