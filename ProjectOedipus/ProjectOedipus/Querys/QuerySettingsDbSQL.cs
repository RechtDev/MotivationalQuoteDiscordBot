using Microsoft.EntityFrameworkCore;
using ProjectOedipus.Common;
using ProjectOedipus.DbContexts;
using ProjectOedipus.Models;

namespace ProjectOedipus.Querys
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

        public async Task<List<GuildServerSettingsModel>> GetAllBotSettings() => await _settingsContext.GetGuildServerSettings.ToListAsync();

        public async Task UpdateStartTime(DateTime newStartTime, GuildServerSettingsModel serverToUpdate)
        {
            try
            {
                var currentSettings = await _settingsContext.GetGuildServerSettings.FirstOrDefaultAsync(x => x.GuildId == serverToUpdate.GuildId);

                if (currentSettings == null)
                {
                    throw new NullReferenceException(message: $"No server settings were found for server {serverToUpdate.GuildId} ");
                }

                currentSettings.StartTime = newStartTime;
                currentSettings.TimesPostedAlready++;
                await _settingsContext.SaveChangesAsync();
            }
            catch (NullReferenceException ex)
            {
                throw;
            }

        }

        public async Task ResetServerSettings(List<GuildServerSettingsModel> serversToReset)
        {
            try
            {
                List<GuildServerSettingsModel> ValidServers = new();
                // loop through and find server settings
                foreach (var server in serversToReset)
                {
                    var currentSettings = await _settingsContext.GetGuildServerSettings.FirstOrDefaultAsync(x => x.GuildId == server.GuildId);

                    if (currentSettings == null)
                    {
                        continue;
                    }

                    currentSettings.OrignalStartTime = currentSettings.OrignalStartTime.AddDays(1);
                    currentSettings.StartTime = currentSettings.OrignalStartTime;
                    currentSettings.TimesPostedAlready = 0;
                    ValidServers.Add(currentSettings);
                }

                // save all changes
                _settingsContext.UpdateRange(ValidServers);
                await _settingsContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}