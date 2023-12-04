using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class ImagensMapping : IEntityTypeConfiguration<Imagens>
    {
        public void Configure(EntityTypeBuilder<Imagens> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Acomodacao).WithMany(x => x.Imagens).HasForeignKey(x => x.Id_Acomodacao);

            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Nome).HasColumnName("Nome");
            builder.Property(x => x.Id_Acomodacao).HasColumnName("IdAcomodacao");
            builder.Property(x => x.RotaImagem).HasColumnName("CaminhoImagem");

        }
    }
}
