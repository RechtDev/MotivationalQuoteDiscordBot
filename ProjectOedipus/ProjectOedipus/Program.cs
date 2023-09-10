using DSharpPlus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectOedipus.Commands;
using ProjectOedipus.Common;
using ProjectOedipus.DbContexts;
using ProjectOedipus.QuartzJobs;
using ProjectOedipus.Querys;
using ProjectOedipus.Services;
using Quartz;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Collections.Specialized;

namespace ProjectOedipus
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            BuildConfig(builder);

            var config = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .Filter.ByIncludingOnly(x => x.Level == Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<SettingsDbContext>(options => options.UseSqlServer(config.GetConnectionString("ServerSettingsSQL")).LogTo(Log.Logger.Debug, LogLevel.Debug, null));
                    services.AddTransient<IQuerySettingsDb, QuerySettingsDbSQL>();
                    services.AddHttpClient<IQuoteService, ZenQuoteService>().ConfigureHttpClient(client => client.BaseAddress = Utility.CreateUri(config.GetSection("APIUrls")["ZenQuoteAPI"]));
                    services.AddScoped<IQuoteCommand, QuoteCommand>();
                    services.AddScoped<IMessageServerChannel, MessageServerChannel>();
                    services.AddQuartz(x =>
                    {
                        x.UseMicrosoftDependencyInjectionJobFactory();
                    });
                    services.AddSingleton<DiscordRestClient>(x =>
                    {
                        return new DiscordRestClient(new DiscordConfiguration
                        {
                            Token = config.GetSection("AuthTokens")["DiscordBotToken"],
                            TokenType = TokenType.Bot,
                            Intents = DiscordIntents.AllUnprivileged
                        });
                    });
                })
                .UseSerilog()
                .Build();

            var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await schedulerFactory.GetScheduler();

            var job = JobBuilder.Create<PostQuoteJob>()
                .WithIdentity("SendScheduledQuote", "PostingJobs")
                .Build();

            var timerTrigger = TriggerBuilder.Create()
                .WithIdentity("1hrTimerTrigger", "PostingJobs")
                .ForJob(job)
                .StartAt(DateBuilder.EvenHourDate(null))
                //.WithCronSchedule("0 0/1 * 1/1 * ? *")
                .WithCronSchedule("0 0 0/1 1/1 * ? *")
                .Build();

            await scheduler.ScheduleJob(job, timerTrigger);

            await scheduler.Start();

            await host.RunAsync();
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}