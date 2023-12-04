using System;
using System.Data.Common;

namespace Application.ViewModel
{
    public class BookingViewModel
    {
        public BookingViewModel()
        {
            Entrada = DateTime.Now;
            Saida = DateTime.Now;
        }

        public DateTime Entrada { get; set; }
        public DateTime Saida { get; set; }
        public string Email { get; set; }
        public string Pet { get; set; }
        public string Acomodacao { get; set; }

    }
}