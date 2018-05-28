using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class UserLanguage
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int LanguageId { get; set; }
        public virtual LanguageLevel Level { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Language Language { get; set; }
    }
}
