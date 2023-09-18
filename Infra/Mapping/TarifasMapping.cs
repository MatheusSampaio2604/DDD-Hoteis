using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class TarifasMapping : IEntityTypeConfiguration<Tarifas>
    {
        public void Configure(EntityTypeBuilder<Tarifas> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Ativo).HasDefaultValue(false);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Nome).HasColumnName("Nome");
            builder.Property(x => x.Ativo).HasColumnName("Ativo");
            builder.Property(x => x.Valor).HasColumnName("Valor").HasPrecision(18, 2);
            
        }
    }
}
