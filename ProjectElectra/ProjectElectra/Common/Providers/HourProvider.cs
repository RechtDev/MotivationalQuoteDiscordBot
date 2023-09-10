using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using EnumsNET;

namespace ProjectElectra.Common.Providers
{
    public class HourProvider : IAutocomplete​Provider
    {
        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            IEnumerable<DiscordAutoCompleteChoice> Hours = default;
            await Task.Run(() =>
            {
                Hours = new List<DiscordAutoCompleteChoice>()
                {
                    //am Hours
                    new DiscordAutoCompleteChoice("12am",Enums.Hours.TwelveAM.AsString()),
                    new DiscordAutoCompleteChoice("1am",Enums.Hours.OneAM.AsString()),
                    new DiscordAutoCompleteChoice("2am",Enums.Hours.TwoAM.AsString()),
                    new DiscordAutoCompleteChoice("3am",Enums.Hours.ThreeAM.AsString()),
                    new DiscordAutoCompleteChoice("4am",Enums.Hours.FourAM.AsString()),
                    new DiscordAutoCompleteChoice("5am",Enums.Hours.FiveAM.AsString()),
                    new DiscordAutoCompleteChoice("6am",Enums.Hours.SixAM.AsString()),
                    new DiscordAutoCompleteChoice("7am",Enums.Hours.SevenAM.AsString()),
                    new DiscordAutoCompleteChoice("8am",Enums.Hours.EightAM.AsString()),
                    new DiscordAutoCompleteChoice("9am",Enums.Hours.NineAM.AsString()),
                    new DiscordAutoCompleteChoice("10am",Enums.Hours.TenAM.AsString()),
                    new DiscordAutoCompleteChoice("11am",Enums.Hours.ElevenAM.AsString()),
                    //pm Hours
                    new DiscordAutoCompleteChoice("12pm",Enums.Hours.TwelvePM.AsString()),
                    new DiscordAutoCompleteChoice("1pm",Enums.Hours.OnePM.AsString()),
                    new DiscordAutoCompleteChoice("2pm",Enums.Hours.TwoPM.AsString()),
                    new DiscordAutoCompleteChoice("3pm",Enums.Hours.ThreePM.AsString()),
                    new DiscordAutoCompleteChoice("4pm",Enums.Hours.FourPM.AsString()),
                    new DiscordAutoCompleteChoice("5pm",Enums.Hours.FivePM.AsString()),
                    new DiscordAutoCompleteChoice("6pm",Enums.Hours.SixPM.AsString()),
                    new DiscordAutoCompleteChoice("7pm",Enums.Hours.SevenPM.AsString()),
                    new DiscordAutoCompleteChoice("8pm",Enums.Hours.EightPM.AsString()),
                    new DiscordAutoCompleteChoice("9pm",Enums.Hours.NinePM.AsString()),
                    new DiscordAutoCompleteChoice("10pm",Enums.Hours.TenPM.AsString()),
                    new DiscordAutoCompleteChoice("11pm",Enums.Hours.ElevenPM.AsString()),
                }.AsEnumerable();
            });
            return Hours;
        }
    }
}