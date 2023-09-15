using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Response
{
    public class UsuarioCadastroResponse
    {
        public bool Sucesso { get; private set; }
        
        public List<string> Erros { get; private set; }
        
        public UsuarioCadastroResponse() => Erros = new List<string>();
        
        public UsuarioCadastroResponse(bool sucesso = true) : this() => Sucesso = sucesso;

        public void AdicionarErros(IEnumerable<string> erros) => Erros.AddRange(erros);
    }
}
