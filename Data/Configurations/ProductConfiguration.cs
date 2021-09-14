using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
  public  class ProductConfiguration : IEntityTypeConfiguration<ProductAndService>
    {
        public void Configure(EntityTypeBuilder<ProductAndService> builder)
        {
            builder.Property(x => x.Price).HasPrecision(18, 2);
           
        }

    }
}
