using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace ProjectElectra.Common.Providers
{
    internal class TextChannelProvider : IAutocomplete​Provider
    {
        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            var allChannels = await ctx.Guild.GetChannelsAsync();
            var textChannelChoices = allChannels.Where(x => x.Type == DSharpPlus.ChannelType.Text)
                                                .Select(x => new DiscordAutoCompleteChoice(x.Name, x.Name));
            return textChannelChoices;
        }
    }
}