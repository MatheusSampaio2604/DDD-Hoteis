using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class AcomodacaoMapping : IEntityTypeConfiguration<Acomodacao>
    {
        public void Configure(EntityTypeBuilder<Acomodacao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Ativo).HasDefaultValue(false);
            builder.HasOne(x => x.Tarifas).WithMany(x => x.Acomodacao).HasForeignKey(x => x.IdValor);
            builder.HasOne(x => x.Home).WithMany(x => x.Acomodacao).HasForeignKey(x => x.IdHome);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Nome).HasColumnName("Nome");
            builder.Property(x => x.Descricao).HasColumnName("Descricao");
            builder.Property(x => x.Ativo).HasColumnName("Ativo");
            builder.Property(x => x.IdValor).HasColumnName("IdTarifas");
            builder.Property(x => x.IdHome).HasColumnName("IdHome");
            builder.Property(x => x.RotaImagem).HasColumnName("Fotos");
        }
    }
}
