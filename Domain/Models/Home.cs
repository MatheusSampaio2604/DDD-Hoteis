using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("TBL_Estabelecimento")]
    public class Home
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nome")]
        public string Nome { get; set; }

        [Column("Descricao 0")]
        public string? Descricao_0 { get; set; }
        [Column("Descricao 1")]
        public string? Descricao_1 { get; set; }
        [Column("Descricao 2")]
        public string? Descricao_2 { get; set; }
        [Column("Descricao 3")]
        public string? Descricao_3 { get; set; }
        [Column("Descricao 4")]
        public string? Descricao_4 { get; set; }

        public virtual ICollection<Acomodacao> Acomodacao { get; set; }
    }
}
