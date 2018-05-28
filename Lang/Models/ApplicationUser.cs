using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lang.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser() : base()
        {
            Languages = new List<UserLanguage>();
            ChatParticipation = new List<ChatParticipant>();
        }

        public virtual string FirstName { get; set; }
        public virtual string Country { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string Profile { get; set; }

        public virtual List<UserLanguage> Languages { get; set; }
        public virtual List<ChatParticipant> ChatParticipation { get; set; }
    }
}
