using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.Data;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Helpers.Mapping;

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


        return await models
            .Select(m => m.ToPhonebookDto())
            .ToListAsync(cancellationToken);
    }

    public async Task<PhonebookDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var model = await _context.PhonebookRecords
            .Include(pb => pb.PhoneNumbers)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        return model?.ToPhonebookDto();
    }

    public async Task<PhonebookDto> CreateAsync(PhonebookDto createDto, CancellationToken cancellationToken)
    {
        var newPhonebookRecord = createDto.ToPhoneBookRecord();

        await _context.PhonebookRecords.AddAsync(newPhonebookRecord, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return createDto;
    }

    public async Task<PhonebookDto?> UpdateAsync(int id, PhonebookUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var model = await _context.PhonebookRecords
            .Include(pb => pb.PhoneNumbers)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (model is null) return null;

        model.Lastname = updateDto.Lastname;
        model.Firstname = updateDto.Firstname;
        model.Middlename = updateDto.Middlename;
        model.PhoneNumbers = updateDto.PhoneNumbers
            .Select(number => number.ToPhoneNumber()).ToList();
        model.Address = updateDto.Address;
        model.Email = updateDto.Email;
        model.Post = updateDto.Post;
        model.Organization = updateDto.Organization;
        model.Subdivision = model.Subdivision;

        await _context.SaveChangesAsync(cancellationToken);
        return model.ToPhonebookDto();
    }

    public async Task<PhonebookDto?> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var model = await _context.PhonebookRecords
            .Include(pb => pb.PhoneNumbers)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (model is null) return null;

        _context.PhonebookRecords.Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
        return model.ToPhonebookDto();
    }
}