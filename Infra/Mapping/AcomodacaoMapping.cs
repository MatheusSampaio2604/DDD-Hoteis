using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
