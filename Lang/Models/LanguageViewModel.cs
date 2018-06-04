using Lang.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class LanguageViewModel
    {
        private static readonly string ALL_LANGUAGES_CACHE_KEY = "LanguageViewModel.All";

        public LanguageViewModel(Language language)
        {
            Id = language.Id;
            Name = language.Name;
            Icon = language.Icon;
            IsCommon = language.IsCommon;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsCommon { get; set; }

        public static async Task<Dictionary<string, LanguageViewModel>> All(IMemoryCache cache, ApplicationDbContext db)
        {
            var result = cache.Get<Dictionary<string, LanguageViewModel>>(ALL_LANGUAGES_CACHE_KEY);
            if (result == null)
            {
                var languages = await db.Languages.ToListAsync();
                result = languages.Select(x => new LanguageViewModel(x)).ToDictionary(x => x.Id);
                cache.Set(ALL_LANGUAGES_CACHE_KEY, result, DateTimeOffset.UtcNow.AddMinutes(5));
            }

            return result;
        }
    }
}
