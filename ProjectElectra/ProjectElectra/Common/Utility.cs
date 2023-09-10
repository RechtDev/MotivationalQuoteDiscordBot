using DSharpPlus.SlashCommands;

namespace ProjectElectra.Common
{
    public static class Utility
    {
        public static async Task<bool> CheckIfChannelExists(string channelName, InteractionContext ctx)
        {
            try
            {
                var channels = await ctx.Guild.GetChannelsAsync();
                var textChannels = channels.Where(x => x.Type == DSharpPlus.ChannelType.Text).Select(x => new { Name = x.Name });

                if (textChannels != null)
                {
                    if (textChannels.Any(x => x.Name == channelName))
                    {
                        return true;
                    }
                    return false;
                }
                throw new NullReferenceException();
            }
            catch (NullReferenceException ex)
            {
                //Logging eventually
                throw;
            }
        }

        public static async Task<ulong> GetChannelId(string channelName, InteractionContext ctx)
        {
            var channels = await ctx.Guild.GetChannelsAsync();
            var textChannels = channels.Where(x => x.Type == DSharpPlus.ChannelType.Text).Select(x => new { Name = x.Name, Id = x.Id });

            if (textChannels != null)
            {
                var channel = textChannels.First(x => x.Name == channelName);
                if (channel != null)
                {
                    return channel.Id;
                }
            }
            return default;
        }

        public static DateTime ConvertDateEnumToDateTime(Enums.Hours time)
        {
            var currentDate = DateTime.Now;

            List<DateTime> TodaysTimes = new();
            for (int i = 0; i < 24; i++)
            {
                TodaysTimes.Add(DateTime.Today.AddHours(i));
            }

            var desiredTime = TodaysTimes.Find(x => x.Hour == (int)time);
            var timePassed = desiredTime.Subtract(currentDate);
            if (timePassed.Ticks < 0)
            {
                desiredTime = desiredTime.AddDays(1);
            }

            return desiredTime;
        }
    }
}