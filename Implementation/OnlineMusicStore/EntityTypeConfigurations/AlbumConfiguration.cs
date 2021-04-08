using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.EntityTypeConfigurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> modelBuilder)
        {

            modelBuilder
            .HasMany(al => al.Musics)
            .WithOne(m => m.Album);

        }
    }
}