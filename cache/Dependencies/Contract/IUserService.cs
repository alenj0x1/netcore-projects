using cache.Models;

namespace cache.Dependencies.Contract
{
  public interface IUserService
  {
    User GetUser(int id, string? name);
  }
}
