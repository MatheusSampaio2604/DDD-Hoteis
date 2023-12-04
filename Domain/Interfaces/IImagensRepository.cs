using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Domain.Interfaces
{
    public interface IImagensRepository : IRepository<Imagens>
    {
        Task<IEnumerable<Imagens>> FindImageFromAcomodationID(int id);
        Task<int> CreateManyAsync(IEnumerable<Imagens> imagens);

        Task<int> RemoveManyAsync(IEnumerable<Imagens> imagens);
    }
}
