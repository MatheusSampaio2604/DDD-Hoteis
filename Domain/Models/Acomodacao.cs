using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TBL_Acomodacao")]
    public class Acomodacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

#nullable enable
        public int? IdValor { get; set; }
        public int? IdHome { get; set; }
        public virtual Home? Home { get; set; }
        public virtual Tarifas? Tarifas { get; set; }
        public virtual IEnumerable<Imagens>? Imagens { get; set; }

#nullable disable
    }
}
