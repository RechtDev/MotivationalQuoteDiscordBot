using ProjectOedipus.Models.Responses;

namespace ProjectOedipus.Services
{
    public interface IQuoteService
    {
        public Task<QuoteResponse> GetQuoteFromProvider();
        public Task<List<QuoteResponse>> GetQuotesInSpecificCategory(string catergory);
    }
}