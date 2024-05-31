namespace PhoneBook.WebApi.DTOs.PhonebookRecord;

public class PhonebookSyncDto
{
    public string Id { get; set; }

    public string Lastname { get; set; } = "";

    public string Firstname { get; set; } = "";

    public string Middlename { get; set; } = "";

    public List<PhoneNumberDto> PhoneNumbers { get; set; } = [];

    public string Email { get; set; } = "";

    public string Address { get; set; } = "";

    public string Post { get; set; } = "";

    public string Organization { get; set; } = "";

    public string Subdivision { get; set; } = "";
}