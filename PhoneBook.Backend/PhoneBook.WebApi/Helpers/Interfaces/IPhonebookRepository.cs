using PhoneBook.WebApi.DTOs.PhonebookRecord;

namespace PhoneBook.WebApi.Helpers.Interfaces;

public interface IPhonebookRepository
{
    Task<List<PhonebookDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PhonebookDto?> GetById(int id, CancellationToken cancellationToken);
    Task<PhonebookDto> Create(PhonebookDto createDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> Update(int id, PhonebookUpdateDto updateDto, CancellationToken cancellationToken);
    Task<PhonebookDto?> Delete(int id, CancellationToken cancellationToken);
}