using System.ComponentModel.DataAnnotations;

namespace BancoApi.Api.Models
{
    public class Conta
    {
        [Key]
        public int ContaNumero { get; set; }

        public decimal Saldo { get; set; }
    }
}
