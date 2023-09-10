using ProjectOedipus.Models;

namespace ProjectOedipus.Services
{
    public interface IMessageServerChannel
    {
        Task SendQuote(GuildServerSettingsModel server);
    }
}