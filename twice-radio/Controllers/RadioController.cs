using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace twice_radio.Controllers
{
  [Route("r")]
  [ApiController]
  public class RadioController : Controller
  {
    private readonly string[] _musics = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Music"));

    [HttpGet("{filename}")]
    public IActionResult GetSong(string filename, [FromQuery] bool exactName)
    {
      string? findSong;

      if (exactName)
      {
        findSong = _musics.Where(file => Path.GetFileName(file).Contains(filename)).FirstOrDefault();
      }
      else
      {
        findSong = _musics.Where(file => Path.GetFileName(file).Contains(filename)).FirstOrDefault();
      }

      if (findSong == null) return NotFound("file not exist.");

      FileStream fileStream = new(findSong, FileMode.Open, FileAccess.Read);

      return File(fileStream, "audio/mpeg", Path.GetFileName(findSong));
    }

    [HttpGet("getSongs")]
    public IActionResult GetSongs()
    {
      return Ok(JsonSerializer.Serialize(_musics.Select(file => Path.GetFileName(file))));
    }
  }
}
