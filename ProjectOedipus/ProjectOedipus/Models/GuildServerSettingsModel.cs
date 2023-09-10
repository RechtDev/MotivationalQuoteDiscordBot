using System.ComponentModel.DataAnnotations;

namespace ProjectOedipus.Models
{
    public class GuildServerSettingsModel
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }

        public ulong AssignedChannelId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime OrignalStartTime { get; set; }

        public int PostingInterval { get; set; }

        public int NumberOfTimesToPost { get; set; }

        public bool HasUpdated { get; set; }
        public bool IsNew { get; set; }
        public int TimesPostedAlready { get; set; }
    }
}