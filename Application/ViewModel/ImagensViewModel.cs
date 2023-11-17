namespace Application.ViewModel
{
    public class ImagensViewModel
    {
        public int Id { get; set; }
        public int Id_Acomodacao { get; set; }
        public string Nome { get; set; }
        public string RotaImagem { get; set; }

        public virtual AcomodacaoViewModel Acomodacao { get; set; }



    }
}