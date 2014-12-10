using System.Collections.Concurrent;
using MiniWeb.Models;

namespace MiniWeb
{
    public class UserCache : IUserCache
    {
        private ConcurrentDictionary<string, ApplicationUser> cache = new ConcurrentDictionary<string, ApplicationUser>();

        public ConcurrentDictionary<string, ApplicationUser> GetCache()
        {
            return cache;
        }
    }
}