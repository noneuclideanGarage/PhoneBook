using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Helpers.Interfaces;

public interface ITokenService
{
    string? CreateToken(AppUser user);
}