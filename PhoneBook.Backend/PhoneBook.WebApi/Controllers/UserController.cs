using Microsoft.AspNetCore.Mvc;
using PhoneBook.WebApi.DTOs.AppUser;
using PhoneBook.WebApi.Helpers.Interfaces;
using Serilog;

namespace PhoneBook.WebApi.Controllers;

[ApiController]
[Route("api/account")]
public class UserController : ControllerBase
{
    private readonly IAccountRepository _repository;
    private readonly ITokenService _tokenService;

    public UserController(IAccountRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken token)
    {
        if (!ModelState.IsValid)
        {
            Log.Information("{@ModelState}", ModelState);
            return BadRequest(ModelState);
        }

        var (result, user, roles) = await _repository.Login(loginDto, token);

        if (result is null || !result.Succeeded)
        {
            Log.Information("Username/Password is not correct");
            return Unauthorized("Username/Password is not correct");
        }

        Log.Information("Login user - complete");
        return Ok(new NewUserDto
        {
            Username = loginDto.Username,
            Role = roles.ToList()[0],
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegistrationUserDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                Log.Information("{@ModelState}", ModelState);
                return BadRequest(ModelState);
            }

            var (result, user) = await _repository.Register(registerDto);

            if (result is not null && user is not null)
            {
                Log.Information("Registration complete");
                return Ok(new NewUserDto
                {
                    Username = registerDto.Username,
                    Role = registerDto.Role,
                    Token = _tokenService.CreateToken(user)!
                });
            }

            Log.Information("Registration not complete\nErrors: {@errors}",
                result?.Errors);
            return StatusCode(500, result?.Errors);
        }
        catch (Exception e)
        {
            Log.Information(e.Message);
            return StatusCode(500, e.Message);
        }
    }
}