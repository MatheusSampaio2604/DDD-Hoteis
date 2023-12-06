using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class HomeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nome do Estabelecimento")]
        public string Nome { get; set; }



#nullable enable
        [MaxLength(20000)]
        [DisplayName("Descrição Geral")]
        public string? Descricao_0 { get; set; }

        [MaxLength(20000)]
        [DisplayName("Descrição 1")]
        public string? Descricao_1 { get; set; }

        [MaxLength(20000)]
        [DisplayName("Descrição 2")]
        public string? Descricao_2 { get; set; }

        [MaxLength(20000)]
        [DisplayName("Descrição 3")]
        public string? Descricao_3 { get; set; }

        [MaxLength(20000)]
        [DisplayName("Descrição 4")]
        public string? Descricao_4 { get; set; }
        public virtual ICollection<AcomodacaoViewModel>? Acomodacao { get; set; }
#nullable disable











        //         #region formulario de contato
        // #nullable enable
        //         [Required(ErrorMessage = "O nome é obrigatório.")]
        //         [DisplayName("Nome")]
        //         public string? Name { get; set; }

        //         [Required(ErrorMessage = "O e-mail é obrigatório.")]
        //         [EmailAddress(ErrorMessage = "Digite um endereço de e-mail válido.")]
        //         [DisplayName("E-mail")]
        //         public string? Email { get; set; }

        //         [Required(ErrorMessage = "O assunto é obrigatório.")]
        //         [DisplayName("Assunto")]
        //         public string? Subject { get; set; }

        //         [Required(ErrorMessage = "A mensagem é obrigatória.")]
        //         [DisplayName("Mensagem")]
        //         public string? Message { get; set; }
        // #nullable disable
        //         #endregion

    }

}
