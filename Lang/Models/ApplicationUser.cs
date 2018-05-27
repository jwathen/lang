using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lang.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual string FirstName { get; set; }
        public virtual string Country { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual string Profile { get; set; }

        public virtual List<UserLanguage> UserLanguages { get; set; }
    }
}
