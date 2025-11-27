using BancoApi.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Api.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conta>().HasKey(c => c.ContaNumero);

            // Seed an example account matching the prompt examples: initial saldo = 160
            modelBuilder.Entity<Conta>().HasData(new Conta
            {
                ContaNumero = 54321,
                Saldo = 160m
            });
        }
    }
}
