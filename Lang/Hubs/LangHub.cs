using Lang.Data;
using Lang.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Hubs
{
    public class LangHub : Hub
    {
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;

        public LangHub(ApplicationDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public async Task HeartBeat(UserActivityStatus status)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            await HeartBeat(int.Parse(Context.User.Identity.Name), status);
        }

        public async Task HeartBeat(int userId, UserActivityStatus status)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            var user = await _db.Users.FindAsync(userId);
            var userLanguages = await _db.UserLanguages
                .Where(x => x.UserId == userId)
                .ToListAsync();
            user.HeartBeat = DateTime.UtcNow;
            if (user.ActivityStatus != status)
            {
                user.ActivityStatus = status;
                await _db.SaveChangesAsync();
                var allLanguages = await LanguageViewModel.All(_cache, _db);
                var userViewModel = new UserViewModel(user, userLanguages, allLanguages);
                await Clients.Others.SendAsync("userStatusChanged", userViewModel);
            }
        }
    }
}
