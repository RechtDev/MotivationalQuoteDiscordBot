using ProjectOedipus.Models.Responses;
using ProjectOedipus.Services;
using static ProjectOedipus.Common.Enums;

namespace ProjectOedipus.Commands
{
    public class QuoteCommand : IQuoteCommand
    {
        private IQuoteService _service;

        public QuoteCommand(IQuoteService service)
        {
            _service = service;
        }

        public async Task<QuoteResponse> Execute(QuoteType quoteType)
        {
            string payload = quoteType.ToString();
            var result = await _service.GetQuoteFromProvider();
            var response = new ZenQuoteResponse
            {
                Author = result.Author,
                Quote = result.Quote
            };
            return response;
        }
    }
}