using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class ChatSession
    {
        public ChatSession()
        {
            Participants = new List<ChatParticipant>();
        }

        public virtual int Id { get; set; }
        public virtual ChatSessionStatus Status { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual Guid Guid { get; set; }

        public virtual List<ChatParticipant> Participants { get; set; }
    }
}
