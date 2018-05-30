using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class Language
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Icon { get; set; }
        public virtual bool IsCommon { get; set; }

        public virtual List<UserLanguage> UserLanguages { get; set; }
    }
}
