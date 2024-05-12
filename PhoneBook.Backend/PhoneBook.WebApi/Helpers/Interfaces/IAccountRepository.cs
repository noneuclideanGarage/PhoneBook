using Microsoft.AspNetCore.Identity;
using PhoneBook.WebApi.DTOs.AppUser;
using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Helpers.Interfaces;

public interface IAccountRepository
{
    Task<(SignInResult?, AppUser?, IList<string>)> Login(LoginDto loginDto, CancellationToken token);
    Task<(IdentityResult?, AppUser?)> Register(RegistrationUserDto registerDto);
}