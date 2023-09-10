using ProjectOedipus.Models;

namespace ProjectOedipus.Querys
{
    public interface IQuerySettingsDb
    {
        public Task SaveBotSettings(GuildServerSettingsModel botSettings);

        public Task UpdateStartTime(DateTime newStartTime, GuildServerSettingsModel serverToUpdate);

        public Task<GuildServerSettingsModel> GetBotSettings(ulong guildId);

        Task<List<GuildServerSettingsModel>> GetAllBotSettings();

        Task ResetServerSettings(List<GuildServerSettingsModel> serversToReset);
    }
}