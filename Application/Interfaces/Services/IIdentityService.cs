using Application.Interfaces.Services.Request;
using Application.Interfaces.Services.Response;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
        Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);
    }
}
