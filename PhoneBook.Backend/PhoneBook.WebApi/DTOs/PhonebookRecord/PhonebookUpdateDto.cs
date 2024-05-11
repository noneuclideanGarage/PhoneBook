using System.ComponentModel.DataAnnotations;

namespace PhoneBook.WebApi.DTOs.PhonebookRecord;

public class PhonebookUpdateDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Lastname cannot be over characters")]
    public required string Lastname { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Firstname cannot be over characters")]
    public required string Firstname { get; set; }
    
    [Required]
    [MaxLength(10, ErrorMessage = "Middlename cannot be over characters")]
    public required string Middlename { get; set; }
    
    [Required]
    public required List<PhoneNumberDto> PhoneNumbers { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Address { get; set; }
    
    [Required]
    public required string Post { get; set; }
    
    [Required]
    public required string Organization { get; set; }
    
    [Required]
    public required string Subdivision { get; set; }
}