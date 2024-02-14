using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx_quote_service.DTO
{
    public class ClientResposeDTO
    {
        public string currency;
        public double rate;
        public double amount;

        public ClientResposeDTO(string currencyTaget, double calculatedTargetCurrency, double rate)
        {
            currency = currencyTaget;
            this.rate = rate;
            amount = calculatedTargetCurrency;
        }
    }
}
