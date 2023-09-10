using ProjectElectra.Models;

namespace ProjectElectra.Querys
{
    public interface IQuerySettingsDb
    {
        public Task SaveBotSettings(GuildServerSettingsModel botSettings);

        public Task UpdateBotSettings(GuildServerSettingsModel updatedBotSettings);

        public Task<GuildServerSettingsModel> GetBotSettings(ulong guildId);

        Task<List<GuildServerSettingsModel>> GetUpdatedOrNewSettings();
    }
}