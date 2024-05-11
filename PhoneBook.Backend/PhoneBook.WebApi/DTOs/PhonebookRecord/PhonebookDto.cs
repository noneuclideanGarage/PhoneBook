using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBook.WebApi.DTOs.PhonebookRecord;

public class PhonebookDto
{
    [Required]
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Lastname cannot be over characters")]
    [JsonPropertyName("lastname")]
    public required string Lastname { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Firstname cannot be over characters")]
    [JsonPropertyName("firstname")]
    public required string Firstname { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Middlename cannot be over characters")]
    [JsonPropertyName("middlename")]
    public required string Middlename { get; set; }
    
    [Required]
    [JsonPropertyName("phonenumbers")]
    public required List<PhoneNumberDto> PhoneNumbers { get; set; }
    
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    
    [Required]
    [JsonPropertyName("address")]
    public required string Address { get; set; }
    
    [Required]
    [JsonPropertyName("post")]
    public required string Post { get; set; }
    
    [Required]
    [JsonPropertyName("organization")]
    public required string Organization { get; set; }
    
    [Required]
    [JsonPropertyName("subdivision")]
    public required string Subdivision { get; set; }
}