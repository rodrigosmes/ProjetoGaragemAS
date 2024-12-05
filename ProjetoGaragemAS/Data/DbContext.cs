using Microsoft.EntityFrameworkCore;
using ProjetoGaragemAS.Models;
namespace ProjetoGaragemAS.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaCarroFavorito> Favoritos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PessoaCarroFavorito>()
                .HasKey(pf => new { pf.PessoaId, pf.CarroId });

            modelBuilder.Entity<PessoaCarroFavorito>()
                .HasOne(pf => pf.Pessoa)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(pf => pf.PessoaId);

            modelBuilder.Entity<PessoaCarroFavorito>()
                .HasOne(pf => pf.Carro)
                .WithMany(c => c.Favoritos)
                .HasForeignKey(pf => pf.CarroId);
        }
    }

}
