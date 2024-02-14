using fx_quote_service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx_quote_service.interfaces
{
    public interface Iprovider
    {
        public string SendProviderRequest(ClientRequestDTO requestDTO);
        public Quote ProviderResponseToQuoteParser(string providerResponse, ClientRequestDTO clientRequest);
    }

    internal enum providerName{
        Alpha,
        SecondProvider,
    }
}
