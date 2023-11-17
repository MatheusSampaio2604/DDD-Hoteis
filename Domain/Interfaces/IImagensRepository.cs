using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
namespace Domain.Interfaces
{
    public interface IImagensRepository : IRepository<Imagens>
    {
        Task<IEnumerable<Imagens>> FindImageFromAcomodationID(int id);
        //Task<Imagens> FindImageFromAcomodationID(int id);
    }
}
