using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("TBL_Acomodacao")]
    public class Acomodacao
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Descricao")]
        public string Descricao { get; set; }

        [Column("Ativo")]
        public bool Ativo { get; set; }

        [Column("IdTarifas")]
        public int? IdValor { get; set; }
        public virtual Tarifas Tarifas { get; set; }

        [Column("IdHome")]
        public int? IdHome { get; set; }
        public virtual Home? Home { get; set; }

        [Column("Fotos")]
        public string? RotaImagem { get; set; }

        [NotMapped]
        public ICollection<IFormFile>? Fotos { get; set; }
    }
}
