using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TBL_Estabelecimento")]
    public class Home
    {
        public int Id { get; set; }

        public string Nome { get; set; }

#nullable enable
        public string? Descricao_0 { get; set; }

        public string? Descricao_1 { get; set; }

        public string? Descricao_2 { get; set; }

        public string? Descricao_3 { get; set; }

        public string? Descricao_4 { get; set; }
#nullable disable

        public virtual ICollection<Acomodacao> Acomodacao { get; set; }
    }
}
