
using Domain.Interfaces;
using Domain.Models;
using Infra.Context;

namespace Infra.Repository
{
    public class HomeRepository : Repository<Home> , IHomeRepository
    {
        public HomeRepository(DataContext context) : base(context)
        {

        }
    }
}
