using Domain.Interfaces;
using Domain.Models;
using Infra.Context;

namespace Infra.Repository
{
    public class TarifasRepository : Repository<Tarifas> , ITarifasRepository
    {
        public TarifasRepository(DataContext context) : base(context)
        {

        }
    }
}
