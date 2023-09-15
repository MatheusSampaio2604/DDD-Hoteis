using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Response
{
    public class UsuarioLoginResponse
    {
        public bool Sucesso { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; private set; }

        public List<string> Erros { get; private set; }

        public UsuarioLoginResponse() => Erros = new List<string>();

        public UsuarioLoginResponse(bool sucesso = true) : this() => Sucesso = sucesso;

        public UsuarioLoginResponse(bool sucesso, string token, DateTime dataExpiracao) : this(sucesso)
        {
            Token = token;
            DataExpiracao = dataExpiracao;
        }

        public void AdicionarErro(string erro) => Erros.Add(erro);

        public void AdicionarErros(IEnumerable<string> erros) => Erros.AddRange(erros);
    }
}
