using System.Collections.Concurrent;
using MiniWeb.Models;

namespace MiniWeb
{
    public interface IUserCache
    {
        ConcurrentDictionary<string, ApplicationUser> GetCache();
    }
}