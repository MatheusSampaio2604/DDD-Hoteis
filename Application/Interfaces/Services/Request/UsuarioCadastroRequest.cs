using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces.Services.Request
{
    public class UsuarioCadastroRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Senha { get; set; }


        [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais")]
        public string ConfirmaSenha { get; set; }
    }
}
