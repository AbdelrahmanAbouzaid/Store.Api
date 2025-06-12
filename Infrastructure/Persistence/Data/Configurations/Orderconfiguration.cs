using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class Orderconfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, address => address.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod).WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(s => s.PaymentStatus)
                .HasConversion(s => s.ToString(), s => Enum.Parse<OrderPaymentStatus>(s));

            builder.Property(s => s.SubTotal).HasColumnType("decimal(18,4)");
        }
    }
}
