using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class HomeMapping : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Nome).HasColumnName("Nome");
            builder.Property(x => x.Descricao_0).HasColumnName("Descricao 0");
            builder.Property(x => x.Descricao_1).HasColumnName("Descricao 1");
            builder.Property(x => x.Descricao_2).HasColumnName("Descricao 2");
            builder.Property(x => x.Descricao_3).HasColumnName("Descricao 3");
            builder.Property(x => x.Descricao_4).HasColumnName("Descricao 4");
        }
    }
}
