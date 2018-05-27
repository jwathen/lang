using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class UserLanguage
    {
        public virtual int Id { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual LanguageLevel Level { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Language Language { get; set; }
    }
}
