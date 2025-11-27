using System.Threading.Tasks;
using BancoApi.Api.Services;

namespace BancoApi.Api.GraphQL
{
    public class Query
    {
        public async Task<decimal> Saldo([Service] ContaService service, int conta)
        {
            return await service.GetSaldo(conta);
        }
    }
}
