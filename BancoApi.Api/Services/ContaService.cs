using System.Threading.Tasks;
using BancoApi.Api.Data;
using BancoApi.Api.Models;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;

namespace BancoApi.Api.Services
{
    public class ContaService
    {
        private readonly IDbContextFactory<BankDbContext> _factory;

        public ContaService(IDbContextFactory<BankDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<Conta> GetConta(int contaNumero)
        {
            using var db = _factory.CreateDbContext();
            var conta = await db.Contas.FindAsync(contaNumero);
            if (conta == null)
            {
                throw new GraphQLException(ErrorBuilder.New().SetMessage("Conta não encontrada.").Build());
            }

            return conta;
        }

        public async Task<decimal> GetSaldo(int contaNumero)
        {
            var conta = await GetConta(contaNumero);
            return conta.Saldo;
        }

        public async Task<Conta> Depositar(int contaNumero, decimal valor)
        {
            using var db = _factory.CreateDbContext();
            var conta = await db.Contas.FindAsync(contaNumero);
            if (conta == null)
                throw new GraphQLException(ErrorBuilder.New().SetMessage("Conta não encontrada.").Build());

            conta.Saldo += valor;
            db.Contas.Update(conta);
            await db.SaveChangesAsync();
            return conta;
        }

        public async Task<Conta> Sacar(int contaNumero, decimal valor)
        {
            using var db = _factory.CreateDbContext();
            var conta = await db.Contas.FindAsync(contaNumero);
            if (conta == null)
                throw new GraphQLException(ErrorBuilder.New().SetMessage("Conta não encontrada.").Build());

            if (conta.Saldo < valor)
                throw new GraphQLException(ErrorBuilder.New().SetMessage("Saldo insuficiente.").Build());

            conta.Saldo -= valor;
            db.Contas.Update(conta);
            await db.SaveChangesAsync();
            return conta;
        }
    }
}
