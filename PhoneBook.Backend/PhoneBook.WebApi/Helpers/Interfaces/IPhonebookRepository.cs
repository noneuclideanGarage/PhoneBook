using PhoneBook.WebApi.DTOs.PhonebookRecord;

namespace PhoneBook.WebApi.Helpers.Interfaces;

public interface IPhonebookRepository
{
    Task<List<PhonebookDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PhonebookDto?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<PhonebookDto> CreateAsync(PhonebookDto createDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> UpdateAsync(string id, PhonebookUpdateDto updateDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> DeleteAsync(string id, CancellationToken cancellationToken);
}