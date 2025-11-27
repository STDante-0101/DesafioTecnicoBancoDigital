using System;
using System.Threading.Tasks;
using BancoApi.Api.Data;
using BancoApi.Api.Models;
using BancoApi.Api.Services;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BancoApi.Tests
{
    public class ContaServiceTests
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
        public async Task Sacar_DeveDiminuirSaldo_QuandoSaldoSuficiente()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 1000, Saldo = 200m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            var conta = await service.Sacar(1000, 140m);

            Assert.Equal(60m, conta.Saldo);
        }

        [Fact]
        public async Task Sacar_DeveLancarErro_QuandoSaldoInsuficiente()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 2000, Saldo = 50m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await Assert.ThrowsAsync<GraphQLException>(async () => await service.Sacar(2000, 100m));
        }

        [Fact]
        public async Task Depositar_DeveAumentarSaldo()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 3000, Saldo = 20m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            var conta = await service.Depositar(3000, 200m);

            Assert.Equal(220m, conta.Saldo);
        }

        [Fact]
        public async Task GetSaldo_DeveRetornarSaldoAtual()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 4000, Saldo = 75m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            var saldo = await service.GetSaldo(4000);

            Assert.Equal(75m, saldo);
        }

        [Fact]
        public async Task GetConta_ContaInexistente_DeveLancarErro()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await Assert.ThrowsAsync<GraphQLException>(async () => await service.GetConta(99999));
        }

        [Fact]
        public async Task GetSaldo_ContaInexistente_DeveLancarErro()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await Assert.ThrowsAsync<GraphQLException>(async () => await service.GetSaldo(99999));
        }

        [Fact]
        public async Task Depositar_ContaInexistente_DeveLancarErro()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await Assert.ThrowsAsync<GraphQLException>(async () => await service.Depositar(99999, 100m));
        }

        [Fact]
        public async Task Depositar_ValoresVariados_DeveAtualizarCorreto()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 5000, Saldo = 100m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await service.Depositar(5000, 0.50m);
            await service.Depositar(5000, 199.50m);

            var saldo = await service.GetSaldo(5000);
            Assert.Equal(300m, saldo);
        }

        [Fact]
        public async Task Sacar_ValoresVariados_DeveAtualizarCorreto()
        {
            var options = new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var db = new BankDbContext(options))
            {
                db.Contas.Add(new Conta { ContaNumero = 6000, Saldo = 500m });
                db.SaveChanges();
            }

            var factory = new TestDbContextFactory(options);
            var service = new ContaService(factory);

            await service.Sacar(6000, 100m);
            await service.Sacar(6000, 50m);

            var saldo = await service.GetSaldo(6000);
            Assert.Equal(350m, saldo);
        }
    }
}
