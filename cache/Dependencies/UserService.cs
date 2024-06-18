using cache.Dependencies.Contract;
using cache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace cache.Dependencies
{
  public class UserService : IUserService
  {
    private readonly IMemoryCache _cache;

    public UserService(IMemoryCache cache)
    {
      _cache =  cache;
    }

    public User GetUser(int userId, string? name)
    {
      if (!_cache.TryGetValue(userId, out User user))
      {
        user = new()
        {
          Id = userId,
          Name = name,
        };

        _cache.Set(userId, user);

        return user;
      }

      return user;
    }
  }
}
