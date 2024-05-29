using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Helpers.Mapping;
using Serilog;

namespace PhoneBook.WebApi.Services;

public class SyncService
{
    private readonly PhonebookDbContext _context;
    private readonly IPhonebookRepository _repository;
    private readonly ParserService _parser;

    public SyncService(
        PhonebookDbContext context, 
        IPhonebookRepository repository,
        ParserService parser)
    {
        _parser = parser;
        _context = context;
        _repository = repository;
    }

    public async Task<Dictionary<string, SyncStatus>> SyncFormFiles(IList<IFormFile> files, CancellationToken token)
    {
        Log.Information("SyncService started");
        //result-dictionary
        Dictionary<string, SyncStatus> synchronizedJsons = [];

        Log.Information("Initial sync-status: {@synchronizedJsons}", synchronizedJsons);

        //Synchronization
        try
        {
            foreach (var file in files)
            {
                using var sr = new StreamReader(file.OpenReadStream());
                string jsonText = await sr.ReadToEndAsync(token);

                var dtos = _parser.ParseJsonText(jsonText);

                if (dtos.Count == 0)
                {
                    Log.Error("An error occurred while parsing json");
                    synchronizedJsons.Add(file.FileName, SyncStatus.Unsuccessful);
                    continue;
                }

                foreach (var dto in dtos)
                {
                    var existingRecord = await _context.PhonebookRecords
                        .Include(r => r.PhoneNumbers)
                        .FirstOrDefaultAsync(pr => pr.Id == dto.Id, token);

                    if (existingRecord is null)
                    {
                        var createdRecord =
                            await _repository.CreateAsync(dto, cancellationToken: token);

                        Log.Information("{file} : Record with ID={id} - was created",
                            file.FileName, createdRecord.Id);
                        synchronizedJsons.Add($"{file.FileName} - ID: {createdRecord.Id}", SyncStatus.Success);
                    }
                    else
                    {
                        Log.Information(
                            "({filename}) : Record with ID={id} - was found",
                            file.FileName,
                            existingRecord.Id);

                        var result = await _repository.UpdateAsync(dto.Id,
                            dto.ToUpdateDto(),
                            cancellationToken: token);

                        if (result != null)
                        {
                            Log.Information(
                                "({filename}) : Record with ID={id} - was updated",
                                file.FileName, existingRecord.Id);
                            synchronizedJsons.Add($"{file.FileName} - ID: {existingRecord.Id}", SyncStatus.Updated);
                        }
                    }
                }
            }

            Log.Information("SyncService finished");
            return synchronizedJsons;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            Log.Information("SyncService finished with errors");
            return synchronizedJsons;
        }
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SyncStatus
{
    Success,
    Updated,
    Unsuccessful
}