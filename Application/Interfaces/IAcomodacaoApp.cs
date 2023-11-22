using Application.ViewModel;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAcomodacaoApp : IApp<AcomodacaoViewModel, Acomodacao>
    {
        Task<IEnumerable<AcomodacaoViewModel>> FindAcomodacoesWithPhrase(string phrase);

        Task<AcomodacaoViewModel> FindNoTrackinOneAsync(int id);

    }
}
