using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Helpers.Interfaces;
using Serilog;

namespace PhoneBook.WebApi.Controllers;

[ApiController]
[Route("api/phonebook")]
public class PhonebookController : ControllerBase
{
    private readonly IPhonebookRepository _repository;

    public PhonebookController(IPhonebookRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var result = await _repository.GetAllAsync(token);

        Log.Information("Find {count} records in DB", result.Count);
        return Ok(result);
    }

    [HttpGet("{id:maxlength(30)}")]
    public async Task<IActionResult> GetById(string id, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var result = await _repository.GetByIdAsync(id, token);

        if (result is null)
        {
            Log.Information("Record with ID={id}: not found", id);
            NotFound();
        }

        Log.Information("Record with ID={id}: {@result}", id, result);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePhonebookRecord([FromBody] PhonebookDto dto, CancellationToken token)
    {
        Log.Information("Input: {@dto}", dto);
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var result = await _repository.CreateAsync(dto, token);

        Log.Information("Record created");
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [Authorize]
    [HttpPut("{id:maxlength(30)}")]
    public async Task<IActionResult> UpdatePhonebookRecord(string id,
        [FromBody] PhonebookUpdateDto updateDto, CancellationToken token)
    {
        Log.Information("Input: ID={id}, DTO={@updateDto}", id, updateDto);
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var result = await _repository.UpdateAsync(id, updateDto, token);

        if (result is null)
        {
            Log.Information("Record with ID={id} - doesn't found", id);
            return NotFound("Phonebook doesn't contain that record.");
        }

        Log.Information("Record with ID={id} - was updated", id);
        return Ok(result);
    }

    [Authorize]
    [HttpDelete("{id:maxlength(30)}")]
    public async Task<IActionResult> DeleteRecord(string id, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Error("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var deletedRecord = await _repository.DeleteAsync(id, token);

        if (deletedRecord is null)
        {
            Log.Information("Record with ID={id} - doesn't found", id);
            return NotFound("Phonebook doesn't contain that record with this id.");
        }

        Log.Information("Record with ID={id} - was deleted", id);
        return NoContent();
    }
}