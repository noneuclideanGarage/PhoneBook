using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.WebApi.Services;

namespace PhoneBook.WebApi.Controllers;

[ApiController]
[Route("api/sync-json")]
public class SyncController : ControllerBase
{
    private readonly SyncService _sync;

    public SyncController(SyncService sync)
    {
        _sync = sync;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Upload([FromForm] IList<IFormFile> files, CancellationToken token)
    {
        if (files.Count == 0)
        {
            return BadRequest();
        }

        var result = await _sync.SyncFormFiles(files, token);

        if (result.Count == 0)
            return BadRequest();

        return Ok(result);
    }
}