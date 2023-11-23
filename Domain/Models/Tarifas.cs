using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TBL_Tarifas")]
    public class Tarifas
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }
#nullable enable
        public virtual ICollection<Acomodacao>? Acomodacao { get; set; }
#nullable disable
    }
}
