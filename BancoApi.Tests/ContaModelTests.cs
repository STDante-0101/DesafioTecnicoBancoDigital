using BancoApi.Api.Models;
using Xunit;

namespace BancoApi.Tests
{
    public class ContaModelTests
    {
        [Fact]
        public void Conta_DevePermitirAtribuirPropriedades()
        {
            var conta = new Conta
            {
                ContaNumero = 12345,
                Saldo = 100.50m
            };

            Assert.Equal(12345, conta.ContaNumero);
            Assert.Equal(100.50m, conta.Saldo);
        }

        [Fact]
        public void Conta_SaldoPodeSerZero()
        {
            var conta = new Conta
            {
                ContaNumero = 99999,
                Saldo = 0m
            };

            Assert.Equal(0m, conta.Saldo);
        }

        [Fact]
        public void Conta_SaldoPodeSerNegativo()
        {
            var conta = new Conta
            {
                ContaNumero = 88888,
                Saldo = -50m
            };

            Assert.Equal(-50m, conta.Saldo);
        }

        [Fact]
        public void Conta_ContaNumeroDeveSerInteiro()
        {
            var conta = new Conta
            {
                ContaNumero = int.MaxValue,
                Saldo = 1000m
            };

            Assert.Equal(int.MaxValue, conta.ContaNumero);
        }
    }
}
