using System;
using System.Collections.Generic;
using System.Linq;
using AppMinhaBahia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AppMinhaBahia.Data
{
    public class AppMinhaBahiaContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppMinhaBahiaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prefeitura> Prefeituras { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Requisicao>()
                .HasKey(key => new { key.SetorId, key.PrefeituraId });

            /* Abaixo segue o semeador de cidades do estado */
            var arrayCidades = _configuration.GetSection("ListaDeCidadesBA").Get<string[]>();
            builder.Entity<Cidade>().HasData(
                arrayCidades.Select((nomeCidade, index) => new Cidade() {
                    Id = index + 1,
                    Nome = nomeCidade
                })
            );
        }
    }
}
