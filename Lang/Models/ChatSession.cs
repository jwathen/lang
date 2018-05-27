using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class ChatSession
    {
        public virtual int Id { get; set; }
        public virtual string User1Id { get; set; }
        public virtual string User2Id { get; set; }
        public virtual ChatSessionStatus Status { get; set; }
        public virtual DateTime DateCreated { get; set; }

        public virtual ApplicationUser User1 { get; set; }
        public virtual ApplicationUser User2 { get; set; }
    }
}
