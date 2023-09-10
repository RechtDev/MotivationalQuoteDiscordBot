using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectElectra.Modules;

namespace ProjectElectra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateBot().GetAwaiter().GetResult();
        }

        private static async Task CreateBot()
        {
            IConfigurationRoot config = SetupConfig();
            //init bot client
            var discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = config.GetSection("AuthTokens")["DiscordBotToken"],
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });
            SetupSlashCommands(config, discordClient);
            //Connect to Discord
            await discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static IConfigurationRoot SetupConfig()
        {
            //config file setup
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Program>()
                .Build();
        }

        private static void SetupSlashCommands(IConfigurationRoot config, DiscordClient discordClient)
        {
            //register commands
            var commands = discordClient.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = Startup.ConfigureServices(new ServiceCollection(), config),
            });
            commands.RegisterCommands<BotSetupCommandModule>();
        }
    }
}