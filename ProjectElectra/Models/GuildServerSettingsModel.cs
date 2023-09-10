using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProjectElectra.Models
{
    public class GuildServerSettingsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ulong GuildId { get; set; }

        [Required]
        public ulong AssignedChannelId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime OrignalStartTime { get; set; }

        [Required]
        public int PostingInterval { get; set; }

        [Required]
        public int NumberOfTimesToPost { get; set; }

        public bool HasUpdated { get; set; }
        public bool IsNew { get; set; }

        [Required, NotNull, DefaultValue(0)]
        public int TimesPostedAlready { get; set; }
    }
}