using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Data
{
    public class ChatParticipant
    {
        public int ChatSessionId { get; set; }
        public int UserId { get; set; }
        public bool? HasAccepted { get; set; }

        public ChatSession ChatSession { get; set; }
        public User User { get; set; }
    }
}
