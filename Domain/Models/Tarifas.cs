using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("TBL_Tarifas")]
    public class Tarifas
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Valor")]
        public decimal Valor { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        public virtual ICollection<Acomodacao> Acomodacao { get; set; }

    }
}
