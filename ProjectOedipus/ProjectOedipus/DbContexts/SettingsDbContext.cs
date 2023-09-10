using Microsoft.EntityFrameworkCore;
using ProjectOedipus.Models;

namespace ProjectOedipus.DbContexts
{
    public class SettingsDbContext : DbContext
    {
        public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=RECHT-PC\SQLEXPRESS;Initial Catalog=DiscordBotSettings;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //}
        public DbSet<GuildServerSettingsModel> GetGuildServerSettings { get; set; }
    }
}