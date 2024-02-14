using fx_quote_service.DTO;
using fx_quote_service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static fx_quote_service.providers.AlphaProvider;

namespace fx_quote_service.providers
{
    public class AlphaProvider : Iprovider
    {
        private string BASE_URL = "http://www.alpha.com/api/getquote";
        public AlphaProvider() { }
        public class RequestBody
        {
            public string currencySource { get; set; }
            public string currencyTarget { get; set; }
            public string date {  get; set; }
        }

        public class ResponseBody
        {
            public string rate { get; set; }

            public string currencySource { get; set; }
            public string currencyTarget { get; set; }
            public string date { get; set; }
        }
        private RequestBody CreateProviderRequest(ClientRequestDTO requestDTO)
        {
            return new RequestBody()
            {
                currencySource = requestDTO.currencySource.ToString(),
                currencyTarget = requestDTO.currencyTaget.ToString(),
                date = requestDTO.date.ToString(),
            };
        }

        public string SendProviderRequest(ClientRequestDTO requestDTO)
        {
            RequestBody requestBody = CreateProviderRequest(requestDTO);

            // type of connection requesrt for Sending request to provider ( REST, WebRTC, etc.)
            // http post example:
            // var jsonString = await client.PostAsync($`{BASE_URL}?source={requestDTO.currencySource}`, requestBody);
            ResponseBody providerResponse =  new ResponseBody()
            {
                rate = "3.14",
                currencySource = requestDTO.currencySource,
                currencyTarget = requestDTO.currencyTaget,
                date = requestDTO.date.ToString(),
            };
            // mocking provider response as json string
            return JsonSerializer.Serialize(providerResponse);
        }
        public Quote ProviderResponseToQuoteParser(string response, ClientRequestDTO clientRequest)
        {
            var parsedResponse = JsonSerializer.Deserialize<ResponseBody>(response);
            return new Quote()
            {
                providerName = clientRequest.provider,
                currencyTaget = clientRequest.currencyTaget.ToString(),
                currencySource = clientRequest.currencySource.ToString(),
                validFrom = clientRequest.date.AddHours(-1),
                validTo = clientRequest.date.AddHours(1),
                rate = Double.Parse(parsedResponse.rate),
            };
        }
    }
}
