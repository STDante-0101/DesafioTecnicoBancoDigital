using System;
using System.Threading.Tasks;
using BancoApi.Api.Data;
using BancoApi.Api.GraphQL;
using BancoApi.Api.Models;
using BancoApi.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BancoApi.Tests
{
    public class GraphQLResolversTests
    {
        private class TestDbContextFactory : IDbContextFactory<BankDbContext>
        {
            private readonly DbContextOptions<BankDbContext> _options;

            public TestDbContextFactory(DbContextOptions<BankDbContext> options)
            {
                _options = options;
            }

            public BankDbContext CreateDbContext()
            {
                return new BankDbContext(_options);
            }
        }

        [Fact]
        public async Task Query_Saldo_DeveRetornarValorCorreto()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 12345, Saldo = 500m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);
            var query = new Query();

            var saldo = await query.Saldo(service, 12345);

            Assert.Equal(500m, saldo);
        }

        [Fact]
        public async Task Mutation_Sacar_DeveRetornarContaAtualizada()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 23456, Saldo = 300m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);
            var mutation = new Mutation();

            var conta = await mutation.Sacar(service, 23456, 100m);

            Assert.Equal(23456, conta.ContaNumero);
            Assert.Equal(200m, conta.Saldo);
        }

        [Fact]
        public async Task Mutation_Depositar_DeveRetornarContaAtualizada()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 34567, Saldo = 100m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);
            var mutation = new Mutation();

            var conta = await mutation.Depositar(service, 34567, 250m);

            Assert.Equal(34567, conta.ContaNumero);
            Assert.Equal(350m, conta.Saldo);
        }
    }
}
