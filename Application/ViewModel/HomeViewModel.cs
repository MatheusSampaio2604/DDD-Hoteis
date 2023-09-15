using Microsoft.AspNetCore.Http;
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

        [DisplayName("Fotos")]
        public IFormFile Fotos { get; set; }

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
    }
}
