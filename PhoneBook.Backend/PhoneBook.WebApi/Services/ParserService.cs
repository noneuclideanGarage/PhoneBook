using PhoneBook.WebApi.DTOs.PhonebookRecord;

namespace PhoneBook.WebApi.Services.JsonParser;

public class ParserService
{
    public async Task<List<PhonebookDto>> ParsingJsonFile(IFormFile file, CancellationToken ctk)
    {
        using var sr = new StreamReader(file.OpenReadStream());
        string jsonText = await sr.ReadToEndAsync(ctk);
        
        
        
        throw new NotImplementedException();
    }
}