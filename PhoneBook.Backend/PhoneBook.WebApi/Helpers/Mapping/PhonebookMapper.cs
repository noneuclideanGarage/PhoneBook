using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Helpers.Mapping;

public static class PhonebookMapper
{
    public static PhonebookDto ToPhonebookDto(this PhonebookRecord model)
    {
        return new PhonebookDto
        {
            Id = model.Id,
            Firstname = model.Firstname,
            Middlename = model.Middlename,
            Lastname = model.Lastname,
            PhoneNumbers = model.PhoneNumbers
                .Select(p => p.ToPhoneNumberDto())
                .ToList(),
            Address = model.Address,
            Email = model.Email,
            Post = model.Post,
            Organization = model.Organization,
            Subdivision = model.Subdivision
        };
    }
    
    public static PhonebookRecord ToPhoneBookRecord(this PhonebookDto dto)
    {
        return new PhonebookRecord
        {
            Id = dto.Id,
            Firstname = dto.Firstname ?? "",
            Middlename = dto.Middlename ?? "",
            Lastname = dto.Lastname ?? "",
            PhoneNumbers = dto.PhoneNumbers
                .Select(d => d.ToPhoneNumber())
                .ToList(),
            Email = dto.Email ?? "",
            Address = dto.Address ?? "",
            Post = dto.Post ?? "",
            Organization = dto.Organization ?? "",
            Subdivision = dto.Subdivision ?? ""
        };
    }
    
    public static PhonebookUpdateDto ToUpdateDto(this PhonebookDto dto)
    {
        return new PhonebookUpdateDto
        {
            Firstname = dto.Firstname ?? "",
            Middlename = dto.Middlename ?? "",
            Lastname = dto.Lastname ?? "",
            PhoneNumbers = dto.PhoneNumbers ?? [],
            Email = dto.Email ?? "",
            Address = dto.Address ?? "",
            Organization = dto.Organization ?? "",
            Post = dto.Post ?? "",
            Subdivision = dto.Subdivision ?? ""
        };
    }
}