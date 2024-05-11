using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using Serilog;

namespace PhoneBook.WebApi.Controllers;

[ApiController]
[Route("api/phonebook")]
public class PhonebookController : ControllerBase
{
    public PhonebookController()
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePhonebookRecord([FromBody] PhonebookDto dto, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        //return CreatedAtAction();
        throw new NotImplementedException();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePhonebookRecord(int id,
        [FromBody] PhonebookUpdateDto updateDto, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        throw new NotImplementedException();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRecord(int id, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        throw new NotImplementedException();
    }
}