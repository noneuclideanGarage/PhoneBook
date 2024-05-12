using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Helpers.Mapping;
using Serilog;

namespace PhoneBook.WebApi.Services;

public class SyncService
{
    private readonly PhonebookDbContext _context;
    private readonly IPhonebookRepository _repository;

    public SyncService(PhonebookDbContext context, IPhonebookRepository repository)
    {
        _context = context;
        _repository = repository;
    }

    public async Task<Dictionary<string, SyncStatus>> SyncFormFiles(IList<IFormFile> files, CancellationToken token)
    {
        Log.Information("SyncService started");
        //result-dictionary
        Dictionary<string, SyncStatus> synchronizedJsons = [];

        //Add sync-statuses into result-dictionary
        foreach (var f in files)
        {
            synchronizedJsons.Add(f.FileName, SyncStatus.Unsuccessful);
        }

        Log.Information("Initial sync-status: {@synchronizedJsons}", synchronizedJsons);

        //options for JsonDeserialization
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        //Synchronization
        try
        {
            foreach (var file in files)
            {
                if (file.Length != 0)
                {
                    var newPhonebookDto =
                        await JsonSerializer.DeserializeAsync<PhonebookDto>(
                            file.OpenReadStream(),
                            cancellationToken: token,
                            options: jsonOptions);

                    if (newPhonebookDto != null)
                    {
                        var pbRecordFromDb = await _context.PhonebookRecords
                            .Include(pb => pb.PhoneNumbers)
                            .FirstOrDefaultAsync(
                                p => p.Id == newPhonebookDto.Id, cancellationToken: token);

                        if (pbRecordFromDb != null)
                        {
                            Log.Information(
                                "({filename}) : Record with ID={id} - was found",
                                file.FileName,
                                pbRecordFromDb.Id);

                            var result = await _repository.UpdateAsync(newPhonebookDto.Id,
                                newPhonebookDto.ToUpdateDto(),
                                cancellationToken: token);

                            if (result != null)
                            {
                                Log.Information(
                                    "({filename}) : Record with ID={id} - was updated",
                                    file.FileName, pbRecordFromDb.Id);
                                synchronizedJsons[file.FileName] = SyncStatus.Updated;
                            }
                        }
                        else
                        {
                            var createdRecord =
                                await _repository.CreateAsync(newPhonebookDto, cancellationToken: token);

                            Log.Information("{file} : Record with ID={id} - was created", 
                                file.FileName, createdRecord.Id);
                            synchronizedJsons[file.FileName] = SyncStatus.Success;
                        }
                    }
                }
            }
            
            Log.Information("SyncService finished");
            return synchronizedJsons;
        }
        catch (System.Exception ex)
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