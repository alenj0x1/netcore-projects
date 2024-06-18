using cache.Dependencies;
using cache.Dependencies.Contract;
using cache.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace cache.Controllers
{
  [ApiController]
  public class UserController : Controller
  {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet("user/{id}")]
    public User GetUser(int id, [FromQuery(Name = "name")] string? name)
    {
      return _userService.GetUser(id, name);
    }
  }
}
