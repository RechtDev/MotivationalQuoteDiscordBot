using DSharpPlus;
using DSharpPlus.Entities;
using ProjectOedipus.Commands;
using ProjectOedipus.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static ProjectOedipus.Common.Enums;

namespace ProjectOedipus.Services
{
    public class MessageServerChannel : IMessageServerChannel
    {
        private readonly DiscordRestClient _discordClient;
        private readonly IQuoteCommand _command;
        public MessageServerChannel(DiscordRestClient client, IQuoteCommand command)
        {
            _discordClient = client;
            _command = command;
        }
        public async Task SendQuote(GuildServerSettingsModel serverToPostIn)
        {
            var response = await _command.Execute(QuoteType.inspiration);
            if (response != null)
            {
                DiscordEmbedBuilder embedBuilder = new()
                {
                    Color = DiscordColor.Blurple,
                    Title = "Motivational Quote",
                    Url = @"https://zenquotes.io/",
                    Footer = new() { Text = $"Author - {response.Author} | Inspirational quotes provided by Zenquotes" },
                    Description = response.Quote
                };
                await _discordClient.CreateMessageAsync(serverToPostIn.AssignedChannelId, embedBuilder.Build());
            }
        }
    }
}
