namespace PhoneBook.WebApi.DTOs.AppUser;

public class NewUserDto
{
    public required string Username { get; set; }

    public required string Role { get; set; }

    public required string Token { get; set; }
}