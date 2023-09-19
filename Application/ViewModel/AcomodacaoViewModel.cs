
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ViewModel
{
    public class AcomodacaoViewModel
    {
        
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Nome da Acomodação")]
        [Required(ErrorMessage = "Necessário!")]
        public string Nome { get; set; }

        [MaxLength(20000)]
        [DisplayName("Descrição da Acomodação")]
        [Required(ErrorMessage = "Necessário!")]
        public string Descricao { get; set; }

        [DisplayName("Ativo")]
        public bool Ativo { get; set; }

#nullable enable
        [DisplayName("Valor")]
        public int? IdValor { get; set; }


        [DisplayName("Pertence a")]
        [ForeignKey("Home")]
        public int? IdHome { get; set; }

        [DisplayName("Destino Imagem")]
        public string? RotaImagem { get; set; }

        public IFormFile? Fotos { get; set; }
#nullable disable

        public virtual HomeViewModel Home { get; set; }
        public virtual TarifasViewModel Tarifas { get; set; }

    }
}
