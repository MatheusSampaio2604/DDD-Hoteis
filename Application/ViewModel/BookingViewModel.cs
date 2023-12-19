using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            Entrada = DateTime.Now;
            Saida = DateTime.Now.AddDays(+1);
        }

        [Required(ErrorMessage = "Necessário!")]
        public DateTime Entrada { get; set; }

        [Required(ErrorMessage = "Necessário!")]
        public DateTime Saida { get; set; }

        [Required(ErrorMessage = "Necessário!")]
        public string Acomodacao { get; set; }

        [Required(ErrorMessage = "Necessário!")]
        public string Pet { get; set; }
        

        [Required(ErrorMessage = "Necessário!")]
        [EmailAddress(ErrorMessage = "E-mail Inacessivel.")]
        public string Email { get; set; }
    }
}