using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx_quote_service.DTO
{
    public class Quote
    {
        public string providerName;
        public string currencySource;
        public string currencyTaget;
        public double amount;
        public double rate;
        public DateTime validFrom;
        public DateTime validTo;
    }
}
