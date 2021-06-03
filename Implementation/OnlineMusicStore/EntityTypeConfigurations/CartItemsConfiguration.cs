using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.EntityTypeConfigurations
{
    public class CartItemsConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder
                 .HasOne(ci => ci.Music)
                 .WithOne()
                 .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(ci => ci.MusicId)
                .IsUnique(false);
        }
    }
}
