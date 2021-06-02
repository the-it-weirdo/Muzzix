using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineMusicStore.Models;
using OnlineMusicStore.EntityTypeConfigurations;

namespace OnlineMusicStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<IdentityUser> AllUsers { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        public DbSet<Address> Addresses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new MusicConfiguration());
            builder.ApplyConfiguration(new AlbumConfiguration());
            builder.ApplyConfiguration(new ArtistcConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new CartItemsConfiguration());
            builder.ApplyConfiguration(new AddressConfiguration());
        }
    }
}
