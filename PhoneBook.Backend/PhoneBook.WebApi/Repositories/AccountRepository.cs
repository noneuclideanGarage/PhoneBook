using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.WebApi.DTOs.AppUser;
using PhoneBook.WebApi.Helpers.Interfaces;
using PhoneBook.WebApi.Models;
using Serilog;

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
        Log.Information("Logining started");
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.UserName == loginDto.Username, token);

        if (user is null)
        {
            Log.Information("User didn't found in DB");
            return (null, null, []);
        }

        Log.Information("Checking password");
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
        Log.Information("Logining complete");
        return (result, user, await _userManager.GetRolesAsync(user));
    }

    public async Task<(IdentityResult?, AppUser?)> Register(RegistrationUserDto registerDto)
    {
        Log.Information("Registering started");
        var appUser = new AppUser
        {
            UserName = registerDto.Username
        };
        
        var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

        if (createdUser.Succeeded)
        {
            var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);

            Log.Information("User with {username} was created", appUser.UserName);
            return (roleResult, appUser);
        }
        
        Log.Information("Registration new user was unsuccessful");
        return (null, null);
    }
}