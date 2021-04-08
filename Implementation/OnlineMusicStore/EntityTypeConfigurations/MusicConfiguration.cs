using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineMusicStore.Models;

namespace OnlineMusicStore.EntityTypeConfigurations
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> modelBuilder)
        {
            modelBuilder
            .HasOne(m => m.Genre)
            .WithMany(g => g.Musics);

            modelBuilder
            .HasOne(m => m.Album)
            .WithMany(a => a.Musics);

            modelBuilder
            .HasMany(m => m.Artists)
            .WithMany(a => a.Musics)
            .UsingEntity(ma => ma.ToTable("MusicArtists"));
        }
    }
}