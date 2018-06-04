using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lang.Models
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            Languages = new List<UserLanguage>();
            ChatParticipation = new List<ChatParticipant>();
        }

        public virtual string Name { get; set; }
        public virtual string Country { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual int? BirthYear { get; set; }
        public virtual string Bio { get; set; }
        public virtual DateTime? HeartBeat { get; set; }
        public virtual UserActivityStatus? ActivityStatus { get; set; }

        public virtual List<UserLanguage> Languages { get; set; }
        public virtual List<ChatParticipant> ChatParticipation { get; set; }
    }
}
