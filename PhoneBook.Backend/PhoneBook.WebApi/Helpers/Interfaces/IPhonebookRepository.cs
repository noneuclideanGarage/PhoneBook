using PhoneBook.WebApi.DTOs.PhonebookRecord;

namespace PhoneBook.WebApi.Helpers.Interfaces;

public interface IPhonebookRepository
{
    Task<List<PhonebookDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PhonebookDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PhonebookDto> CreateAsync(PhonebookDto createDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> UpdateAsync(int id, PhonebookUpdateDto updateDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> DeleteAsync(int id, CancellationToken cancellationToken);
}