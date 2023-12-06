
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public int? IdHome { get; set; }

        // [DisplayName("Destino Imagem")]
        // public string? RotaImagem { get; set; }

        [DisplayName("Imagens")]
        public IEnumerable<string>? ImagensID { get; set; }

        [DisplayName("Tipo de Acomodação")]
        [Required(ErrorMessage = "Necessário!")]
        public string? TipoAcomodacao { get; set; }

        public List<IFormFile>? Fotos { get; set; }

        public List<int>? ImagensExcluir { get; set; }

        public virtual HomeViewModel? Home { get; set; }
        public virtual TarifasViewModel? Tarifas { get; set; }
        public virtual IEnumerable<ImagensViewModel>? Imagens { get; set; }
#nullable disable

    }
}
