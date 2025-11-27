using System;
using BancoApi.Api.Data;
using BancoApi.Api.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BancoApi.Tests
{
    public class BankDbContextTests
    {
        [Fact]
        public void DbContext_DeveConfigurarCorretamentePrimaryKey()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var db = new BankDbContext(options);
            db.Database.EnsureCreated();

            var entityType = db.Model.FindEntityType(typeof(Conta));
            var primaryKey = entityType.FindPrimaryKey();

            Assert.NotNull(primaryKey);
            Assert.Single(primaryKey.Properties);
            Assert.Equal(nameof(Conta.ContaNumero), primaryKey.Properties[0].Name);
        }

        [Fact]
        public void DbContext_DeveCriarBancoComSeed()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var db = new BankDbContext(options);
            db.Database.EnsureCreated();

            var conta = db.Contas.Find(54321);

            Assert.NotNull(conta);
            Assert.Equal(54321, conta.ContaNumero);
            Assert.Equal(160m, conta.Saldo);
        }

        [Fact]
        public void DbContext_DeveAdicionarERecuperarConta()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 11111, Saldo = 999.99m });
                db.SaveChanges();
            }

            using (var db = new BankDbContext(options))
            {
                var conta = db.Contas.Find(11111);
                Assert.NotNull(conta);
                Assert.Equal(999.99m, conta.Saldo);
            }
        }
    }
}
