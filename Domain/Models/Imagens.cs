using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TBL_Imagens")]
    public class Imagens
    {
        public int Id { get; set; }

#nullable enable
        public int? Id_Acomodacao { get; set; }
        public string? Nome { get; set; }
        public string? RotaImagem { get; set; }
        public virtual Acomodacao? Acomodacao { get; set; }
#nullable disable


    }
}