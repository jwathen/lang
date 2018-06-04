using Lang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class UserLanguageViewModel
    {
        public LanguageLevel Level { get; set; }
        public LanguageViewModel Language { get; set; }
    }
}
