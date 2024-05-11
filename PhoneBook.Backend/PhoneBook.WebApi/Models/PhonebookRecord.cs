namespace PhoneBook.WebApi.Models;

public class PhonebookRecord
{
    public int Id { get; set; }
    public required string Lastname { get; set; }
    public required string Firstname { get; set; }
    public required string Middlename { get; set; }
    public required ICollection<PhoneNumber> PhoneNumbers { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public required string Post { get; set; }
    public required string Organization { get; set; }
    public required string Subdivision { get; set; }
}