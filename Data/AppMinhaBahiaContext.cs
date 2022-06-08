using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMinhaBahia.Data
{
    public class AppMinhaBahiaContext : DbContext
    {
        public AppMinhaBahiaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Prefeitura> Prefeituras { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Requisicao>()
                .HasKey(key => new { key.SetorId, key.PrefeituraId });
        }
    }
}
