using System.ComponentModel.DataAnnotations;

namespace PhoneBook.WebApi.DTOs.AppUser;

public class LoginDto
{
    [Required] public required string Username { get; set; }

    [Required] public required string Password { get; set; }
}