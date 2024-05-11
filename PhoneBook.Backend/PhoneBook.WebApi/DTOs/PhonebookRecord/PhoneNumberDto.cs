using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneBook.WebApi.DTOs.PhonebookRecord;

public class PhoneNumberDto
{
    [Required]
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [Required]
    [Phone]
    [JsonPropertyName("number")]
    public required string Number { get; set; }
}