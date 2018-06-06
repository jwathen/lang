using Lang.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models.AccountViewModels
{
    public class SignUpProcess
    {
        public ProfileViewModel Profile { get; set; } = new ProfileViewModel();
        public Dictionary<string, LanguageLevel> UserLanguages = new Dictionary<string, LanguageLevel>();
    }
}
