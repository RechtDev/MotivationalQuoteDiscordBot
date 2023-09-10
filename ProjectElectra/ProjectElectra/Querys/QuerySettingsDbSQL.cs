using Microsoft.EntityFrameworkCore;
using ProjectElectra.Models;

namespace ProjectElectra.Querys
{
    internal class QuerySettingsDbSQL : IQuerySettingsDb
    {
        private SettingsDbContext _settingsContext;

        public QuerySettingsDbSQL(SettingsDbContext settingsContext)
        {
            _settingsContext = settingsContext;
        }

        public async Task<GuildServerSettingsModel> GetBotSettings(ulong guildId)
        {
            try
            {
                var botSettings = await _settingsContext.GetGuildServerSettings.FirstOrDefaultAsync(x => x.GuildId == guildId);
                return botSettings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SaveBotSettings(GuildServerSettingsModel botSettings)
        {
            try
            {
                botSettings.IsNew = true;
                await _settingsContext.GetGuildServerSettings.AddAsync(botSettings);
                await _settingsContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateBotSettings(GuildServerSettingsModel updatedBotSettings)
        {
            try
            {
                var currentSettings = await _settingsContext.GetGuildServerSettings.FirstOrDefaultAsync(x => x.GuildId == updatedBotSettings.GuildId);

                if (currentSettings == null)
                {
                    throw new NullReferenceException(message: $"No server settings were found for server {updatedBotSettings.GuildId} ");
                }

                currentSettings.OrignalStartTime = updatedBotSettings.StartTime;
                currentSettings.StartTime = updatedBotSettings.StartTime;
                currentSettings.NumberOfTimesToPost = updatedBotSettings.NumberOfTimesToPost;
                currentSettings.AssignedChannelId = updatedBotSettings.AssignedChannelId;
                currentSettings.PostingInterval = updatedBotSettings.PostingInterval;
                currentSettings.TimesPostedAlready = 0;
                await _settingsContext.SaveChangesAsync();
            }
            catch (NullReferenceException ex)
            {
                throw;
            }
        }

        public async Task<List<GuildServerSettingsModel>> GetUpdatedOrNewSettings() => await _settingsContext.GetGuildServerSettings.Where(x => x.IsNew || x.HasUpdated).ToListAsync();
    }
}