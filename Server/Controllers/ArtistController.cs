using Microsoft.AspNetCore.Mvc;

namespace MixSubmit.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistController : ControllerBase
{
    [HttpPost]
    public IActionResult AddArtist(string name)
    {
        return Ok(name);
    }

    [HttpGet("{id}")]
    public IActionResult GetArtist(string id)
    {
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteArtist(string id)
    {
        return Ok(id);
    }

    [HttpGet]
    public IActionResult GetAllArtists()
    {
        return Ok();
    }
}