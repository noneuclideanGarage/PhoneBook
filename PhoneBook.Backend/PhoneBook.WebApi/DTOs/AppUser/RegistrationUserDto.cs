using System.ComponentModel.DataAnnotations;

namespace PhoneBook.WebApi.DTOs.AppUser;

public class RegistrationUserDto
{
    [Required] 
    public required string Username { get; set; }

    [Required]
    [AllowedValues(["Admin", "User"])]
    public required string Role { get; set; }

    [Required] 
    public required string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords should be the same.")]
    public required string ConfirmPassword { get; set; }
}