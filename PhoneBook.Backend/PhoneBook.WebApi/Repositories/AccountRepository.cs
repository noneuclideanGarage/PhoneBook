using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.DTOs.AppUser;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Models;

namespace PhoneBook.WebApi.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountRepository(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<(SignInResult?, AppUser?, IList<string>)> Login(LoginDto loginDto, CancellationToken token)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == loginDto.Username, token);

        if (user is null)
            return (null, null, []);

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        return (result, user, await _userManager.GetRolesAsync(user));
    }

    public async Task<(IdentityResult?, AppUser?)> Register(RegistrationUserDto registerDto)
    {
        var appUser = new AppUser
        {
            UserName = registerDto.Username
        };

        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);

            return (roleResult, appUser);
        }

        return (null, null);
    }
}