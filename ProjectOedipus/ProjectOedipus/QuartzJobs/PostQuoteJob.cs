using Microsoft.Extensions.Logging;
using ProjectOedipus.Common;
using ProjectOedipus.Models;
using ProjectOedipus.Querys;
using ProjectOedipus.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOedipus.QuartzJobs
{
    public class PostQuoteJob : IJob
    {
        private readonly IQuerySettingsDb _context;
        private readonly IMessageServerChannel _channel;
        private readonly ILogger<PostQuoteJob> _logger;

        public PostQuoteJob(ILogger<PostQuoteJob> logger, IQuerySettingsDb context, IMessageServerChannel channel)
        {
            _logger = logger;
            _context = context;
            _channel = channel;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Log(LogLevel.Information, "Posting Job: Gathering all Servers that need to be Posted to");

            List<GuildServerSettingsModel> ServersNeedingReset = new();
            var scheduledTime = context.ScheduledFireTimeUtc;

            var servers = await _context.GetAllBotSettings();
            var serversToPostIn = servers.Where(x => x.StartTime.ToUniversalTime().Hour == context.ScheduledFireTimeUtc.Value.Hour);

            _logger.LogInformation("Posting Job: Found {ServersCount} servers", serversToPostIn.Count());
            _logger.Log(LogLevel.Information, "Posting Job: Preparing to post quotes in designated chats");

            foreach (var server in serversToPostIn)
            {
                if (server.TimesPostedAlready >= server.NumberOfTimesToPost)
                {
                    ServersNeedingReset.Add(server);
                }
                else
                {
                    await _channel.SendQuote(server);
                    _logger.Log(LogLevel.Information, "Posting Job: Posted in server");
                    await _context.UpdateStartTime(Utility.AddTime(server.StartTime, server.PostingInterval), server);
                }
            }

            if (ServersNeedingReset.Count > 0)
            {
                await _context.ResetServerSettings(ServersNeedingReset);
            }

        }
    }
}
