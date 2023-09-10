using ProjectOedipus.Models.Responses;
using static ProjectOedipus.Common.Enums;

namespace ProjectOedipus.Commands
{
    public interface IQuoteCommand
    {
        Task<QuoteResponse> Execute(QuoteType quoteType);
    }
}