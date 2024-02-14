using fx_quote_service.DTO;
using fx_quote_service.interfaces;
using fx_quote_service.providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace fx_quote_service
{
    public class FxQuoteService
    {
        public delegate Iprovider ProviderFactory();
        private Dictionary<string, ProviderFactory> providerMap;
        private Dictionary<string, List<Quote>> dbContextMock;

        public FxQuoteService() 
        {
            providerMap = new Dictionary<string, ProviderFactory>
            {
                { "Alpha", () => (Iprovider)Activator.CreateInstance(typeof(AlphaProvider))  }
            };

            dbContextMock = new Dictionary<string, List<Quote>>()
            {
                { "Alpha", new List<Quote>() }
            };
        }

        public ClientResposeDTO? RateController(ClientRequestDTO clientRequest)
        {

            Quote quote = SerachForValidExistingQuote(clientRequest, clientRequest.provider);
            if (quote == null)
            {
                if (!providerMap.TryGetValue(clientRequest.provider, out var providerConstructor))
                {
                    return null;
                }
                Iprovider provider = providerConstructor();
                var jsonStringResponse = provider.SendProviderRequest(clientRequest);
                quote = provider.ProviderResponseToQuoteParser(jsonStringResponse, clientRequest);

                SaveQuote(quote, clientRequest.provider);
            }

            double calculatedTargetCurrency = calculateNewCurrency(clientRequest.amount, quote.rate);
            return new ClientResposeDTO(clientRequest.currencyTaget, calculatedTargetCurrency, quote.rate);
        }

        private void SaveQuote(Quote quote, string provider)
        {
            dbContextMock[provider].Add(quote);
        }

        private double calculateNewCurrency(double requestAmount, double rate)
        {
            return requestAmount * rate;
        }
        private Quote? SerachForValidExistingQuote(ClientRequestDTO request, string provider)
        {
            DateTime date = request.date;
            List<Quote> quotes = dbContextMock[provider];
            return quotes.FirstOrDefault(quote => quote.currencySource == request.currencySource && 
                                  quote.currencyTaget == request.currencyTaget &&
                                  DateTime.Compare(date, quote.validFrom) >= 0 &&
                                  DateTime.Compare(quote.validTo ,date) >= 0
                                  );
        }

        public void reduceNonValidQuotes()
        {
            DateTime dateNow = DateTime.Now;

            foreach (var (key, quotes) in dbContextMock)
            {
                dbContextMock[key] = quotes.Where(quote => DateTime.Compare(dateNow, quote.validTo) > 0).ToList();
            }
        }
    }
}
