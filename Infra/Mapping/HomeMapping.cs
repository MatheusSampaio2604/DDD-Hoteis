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
    public class HomeMapping : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
