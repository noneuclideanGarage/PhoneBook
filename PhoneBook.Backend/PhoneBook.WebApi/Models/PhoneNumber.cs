namespace PhoneBook.WebApi.Models;

public class PhoneNumber
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required string Number { get; set; }
}