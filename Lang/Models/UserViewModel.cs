using Lang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Models
{
    public class UserViewModel
    {
        public UserViewModel(User user, List<UserLanguage> userLanguages, Dictionary<string, LanguageViewModel> languages)
        {
            Id = user.Id;
            Name = user.Name;
            Country = user.Country;
            Gender = user.Gender;
            if (user.BirthYear.HasValue)
            {
                Age = DateTime.UtcNow.Year - user.BirthYear.Value;
            }
            Bio = user.Bio;
            ActivityStatus = user.ActivityStatus ?? UserActivityStatus.Offline;
            foreach (var userLanguage in userLanguages)
            {
                Languages.Add(new UserLanguageViewModel
                {
                    Language = languages[userLanguage.LanguageId],
                    Level = userLanguage.Level ?? LanguageLevel.Beginner
                });
            }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public string Bio { get; set; }
        public UserActivityStatus ActivityStatus { get; set; }

        public List<UserLanguageViewModel> Languages { get; set; } = new List<UserLanguageViewModel>();
    }
}
