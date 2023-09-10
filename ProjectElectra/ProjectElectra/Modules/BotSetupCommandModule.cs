using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using ProjectElectra.Common;
using ProjectElectra.Common.Providers;
using ProjectElectra.Models;
using ProjectElectra.Querys;

namespace ProjectElectra.Modules
{
    public class BotSetupCommandModule : ApplicationCommandModule
    {
        private IQuerySettingsDb _query;

        public BotSetupCommandModule(IQuerySettingsDb query)
        {
            this._query = query;
        }

        [SlashRequirePermissions(DSharpPlus.Permissions.ManageChannels)]
        [SlashCommand("setup", "Used to setup bot, should be used once to initialize the bot")]
        public async Task Setup(InteractionContext ctx, [Option("Channel", "Text channel the bot will post in", true)][Autocomplete(typeof(TextChannelProvider))] string channel,
                                                        [Option("Start-Time", "Time posts will begin", true)][Autocomplete(typeof(HourProvider))] string time,
                                                        [Option("Interval", "How long to wait inbetween posts")] double interval,
                                                        [Option("Frequency", "How many times to post overall")] string freq)
        {
            try
            {
                bool channelExists = await Utility.CheckIfChannelExists(channel, ctx);
                DiscordChannel Channel;
                if (!channelExists)
                {
                    //DiscordMember PosiVybes = ctx.Guild.GetMemberAsync();
                    //var Permissions = new DiscordOverwriteBuilder()
                    //create channel
                    Channel = await ctx.Guild.CreateTextChannelAsync(name: channel);
                    channel = Channel.Name;
                }

                var channelId = await Utility.GetChannelId(channel, ctx);

                // check to see if server settings exist
                var settings = await _query.GetBotSettings(ctx.Guild.Id);

                if (settings != null)
                {
                    throw new InvalidOperationException();
                }

                // save settings to server
                var startTime = Utility.ConvertDateEnumToDateTime((Enums.Hours)Enum.Parse(typeof(Enums.Hours), time));
                await _query.SaveBotSettings(new GuildServerSettingsModel
                {
                    AssignedChannelId = channelId,
                    GuildId = ctx.Guild.Id,
                    NumberOfTimesToPost = Convert.ToInt32(freq),
                    PostingInterval = Convert.ToInt32(interval),
                    OrignalStartTime = startTime,
                    StartTime = startTime
                });
                //send confirmation to bot
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Green_Check_Mark)} Posi Vybes Setup Succesfully",
                    Color = new DiscordColor(Variables.Galaxy_Purple_Color_Hex),
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"\n \n {Variables.Setup} By: {ctx.Member.DisplayName} - {DateTime.Now.ToShortDateString()}"
                    }
                };
                embedBuilder.AddField("Details", $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Alarm_Clock)} Start Time: {startTime} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Tv)} Channel: {channel} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Hourglass_Flowing_Sand)} Interval (every x-hour(s)): {interval} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Timer_Clock)} Times per Day: {freq}");
                await ctx.CreateResponseAsync(embedBuilder.Build());
            }
            catch (InvalidOperationException)
            {
                //send confirmation to bot
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Red_X)} Posi Vybes Setup Failed",
                    Description = "The bot has been setup already. Use '/update'",
                    Color = new DiscordColor(Variables.Galaxy_Purple_Color_Hex),
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"\n \n {Variables.Setup} By: {ctx.Member.DisplayName} - {DateTime.Now.ToShortDateString()}"
                    }
                };

                await ctx.CreateResponseAsync(embedBuilder.Build());
            }
        }

        [SlashRequirePermissions(DSharpPlus.Permissions.ManageChannels)]
        [SlashCommand("update", "Used to update bot settings")]
        public async Task Update(InteractionContext ctx, [Option("Update-Channel", "Text channel the bot will post in", true)][Autocomplete(typeof(TextChannelProvider))] string channel,
                                                        [Option("Update-Start-Time", "Time posts will begin", true)][Autocomplete(typeof(HourProvider))] string time,
                                                        [Option("Update-Interval", "How long to wait inbetween posts")] double interval,
                                                        [Option("Update-Frequency", "How many times to post overall")] string freq)
        {
            bool channelExists = await Utility.CheckIfChannelExists(channel, ctx);
            DiscordChannel Channel;
            if (!channelExists)
            {
                //create channel
                Channel = await ctx.Guild.CreateTextChannelAsync(name: channel);
                channel = Channel.Name;
            }

            var channelId = await Utility.GetChannelId(channel, ctx);

            // update servers settings
            var startTime = Utility.ConvertDateEnumToDateTime((Enums.Hours)Enum.Parse(typeof(Enums.Hours), time));
            try
            {
                await _query.UpdateBotSettings(new GuildServerSettingsModel
                {
                    AssignedChannelId = channelId,
                    GuildId = ctx.Guild.Id,
                    NumberOfTimesToPost = Convert.ToInt32(freq),
                    PostingInterval = Convert.ToInt32(interval),
                    StartTime = startTime,
                    OrignalStartTime = startTime
                });

                //send confirmation to bot
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Green_Check_Mark)} Posi Vybes Updated Succesfully",
                    Color = new DiscordColor(Variables.Galaxy_Purple_Color_Hex),
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"\n \n {Variables.Updated} By: {ctx.Member.DisplayName} - {DateTime.Now.ToShortDateString()}"
                    }
                };
                embedBuilder.AddField("Details", $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Alarm_Clock)} Updated Start Time: {startTime} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Tv)} Updated Channel: {channel} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Hourglass_Flowing_Sand)} Updated Interval (every x-hour(s)): {interval} \n \n" +
                    $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Timer_Clock)} Updated Times per Day: {freq}");
                await ctx.CreateResponseAsync(embedBuilder.Build());
            }
            catch (NullReferenceException)
            {
                //send confirmation to bot
                DiscordEmbedBuilder embedBuilder = new DiscordEmbedBuilder
                {
                    Title = $"{DiscordEmoji.FromName(ctx.Client, Variables.Emoji_Red_X)} Posi Vybes Update Failed",
                    Description = "The bot was not initally setup. Use '/setup'",
                    Color = new DiscordColor(Variables.Galaxy_Purple_Color_Hex),
                    Footer = new DiscordEmbedBuilder.EmbedFooter
                    {
                        Text = $"\n \n {Variables.Updated} By: {ctx.Member.DisplayName} - {DateTime.Now.ToShortDateString()}"
                    }
                };

                await ctx.CreateResponseAsync(embedBuilder.Build());
            }
        }
    }
}