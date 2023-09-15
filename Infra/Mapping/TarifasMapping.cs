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
    public class TarifasMapping : IEntityTypeConfiguration<Tarifas>
    {
        public void Configure(EntityTypeBuilder<Tarifas> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Ativo).HasDefaultValue(false);
            
        }
    }
}
