using System.Threading.Tasks;
using BancoApi.Api.Models;
using BancoApi.Api.Services;

namespace BancoApi.Api.GraphQL
{
    public class Mutation
    {
        public async Task<Conta> Sacar([Service] ContaService service, int conta, decimal valor)
        {
            return await service.Sacar(conta, valor);
        }

        public async Task<Conta> Depositar([Service] ContaService service, int conta, decimal valor)
        {
            return await service.Depositar(conta, valor);
        }
    }
}
