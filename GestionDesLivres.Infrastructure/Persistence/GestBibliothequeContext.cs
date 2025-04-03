using GestionDesLivres.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionDesLivres.Infrastructure.Persistence
{
    public class GestBibliothequeContext : DbContext
    {
        public GestBibliothequeContext(DbContextOptions<GestBibliothequeContext> options) : base(options)
        {
        }
 
        public DbSet<Livre> Livre { get; set; }
        public DbSet<Categorie> Categorie { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categorie>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Livre>()
                .HasOne(l => l.Categorie)
                .WithMany(c => c.Livres)
                .HasForeignKey(l => l.IDCategorie)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}


