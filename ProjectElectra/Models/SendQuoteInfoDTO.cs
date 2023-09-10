using Timer = System.Timers.Timer;

namespace Models
{
    public class SendQuoteInfoDTO
    {
        public ulong ServerGuid { get; set; }
        public ulong AssignedChannelId { get; set; }
        public Timer SendQuoteTimer { get; set; }
    }
}