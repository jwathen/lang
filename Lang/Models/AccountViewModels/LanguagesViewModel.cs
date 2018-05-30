using Lang.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models.AccountViewModels
{
    public class LanguagesViewModel
    {
        public Dictionary<string, LanguageLevel> GetUserLanguages()
        {
            if (string.IsNullOrWhiteSpace(UserLanguagesJson))
            {
                return new Dictionary<string, LanguageLevel>();
            }
            return JsonConvert.DeserializeObject<Dictionary<string, LanguageLevel>>(UserLanguagesJson);
        }

        public string UserLanguagesJson { get; set; }
        public Dictionary<string, Language> AllLanguages { get; set; }
        public string AllLanguagesJson { get; set; }

        public async Task BuildAsync(ApplicationDbContext db)
        {
            AllLanguages = await db.Languages.ToDictionaryAsync(x => x.Id);
            AllLanguagesJson = JsonConvert.SerializeObject(AllLanguages);
        }
    }
}
