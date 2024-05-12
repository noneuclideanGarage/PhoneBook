using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Helpers.Interfaces;

namespace PhoneBook.WebApi.Repositories;

public class PhonebookRepository : IPhonebookRepository
{
    private readonly PhonebookDbContext _context;
    
    public PhonebookRepository(PhonebookDbContext context)
    {
        _context = context;
    }

    public async Task<List<PhonebookDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var models = _context.PhonebookRecords
            .Include(pb => pb.PhoneNumbers)
            .AsQueryable();
        
        
        throw new NotImplementedException();
    }

    public async Task<PhonebookDto?> GetById(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PhonebookDto> Create(PhonebookDto createDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PhonebookDto?> Update(int id, PhonebookUpdateDto updateDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<PhonebookDto?> Delete(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}