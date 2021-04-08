using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.EntityTypeConfigurations
{
    public class ArtistcConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> modelBuilder)
        {
            modelBuilder
            .HasMany(ar => ar.Musics)
            .WithMany(m => m.Artists);

            modelBuilder
            .HasMany(ar => ar.Albums)
            .WithMany(al => al.Artists);

        }
    }
}