// See https://aka.ms/new-console-template for more information
using fx_quote_service;
using fx_quote_service.DTO;

Console.WriteLine("Hello, World!");
FxQuoteService service = new FxQuoteService();
ClientRequestDTO clientRequestDTO = new ClientRequestDTO()
{
    currencySource = "dollar",
    currencyTaget = "shekel",
    amount = 2345.67,
    provider = "Alpha",
    date = DateTime.Now,
};

ClientResposeDTO responce = service.RateController(clientRequestDTO);
if  (responce != null)
{
    Console.WriteLine($"currency: {responce.currency } amount: { responce.amount}");
}