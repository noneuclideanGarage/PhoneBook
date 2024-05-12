using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Helpers.Mapping;

public static class PhoneNumberMapper
{
    public static PhoneNumber ToPhoneNumber(this PhoneNumberDto dto)
    {
        return new PhoneNumber
        {
            Number = dto.Number,
            Type = dto.Type
        };
    }

    public static PhoneNumberDto ToPhoneNumberDto(this PhoneNumber model)
    {
        return new PhoneNumberDto
        {
            Number = model.Number,
            Type = model.Type
        };
    }
}