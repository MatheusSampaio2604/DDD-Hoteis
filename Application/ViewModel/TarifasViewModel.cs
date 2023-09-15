using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class TarifasViewModel
    {
        public TarifasViewModel()
        {
            Valor = Convert.ToDecimal(Valor);
        }

        public int Id { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Necessário!")]
        public decimal Valor { get; set; }

        [MaxLength(100)]
        [DisplayName("Descrição da Tarifa")]
        [Required(ErrorMessage = "Necessário!")]
        public string Nome { get; set; }


        [DisplayName("Valor Ativo?")]
        public bool Ativo { get; set; }

        public virtual ICollection<AcomodacaoViewModel> AcomodacaoViewModel { get; set; }

    }
}
