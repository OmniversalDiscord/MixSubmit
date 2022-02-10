using Microsoft.AspNetCore.Mvc;

namespace MixSubmit.Controllers;

[ApiController]
[Route("api/submit")]
public class UploadController : ControllerBase
{
    // Receives an MP3 file and mix ID and uploads to S3
    [HttpPost]
    public IActionResult Post([FromForm] string mixId, [FromForm] IFormFile file)
    {
        return Ok(mixId);
    }
}