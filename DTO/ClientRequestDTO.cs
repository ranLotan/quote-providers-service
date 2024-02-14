using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx_quote_service.DTO
{
    public class ClientRequestDTO
    {                                
        public string currencySource;
        public string currencyTaget;
        public double amount;
        public string provider;
        public DateTime date; 

    }
}
