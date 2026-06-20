using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record LicenseCreateRequest(
    [Required] [MaxLength(20)] string Number,
    [Required] int? TypeId,
    [Required] int? StatusId,
    [Required] DateOnly IssuanceDate,
    [Required] [AllowedValues(3, 5)] int? Validity,
    [Required] [MaxLength(30)] string FirstName,
    [MaxLength(20)] string? MiddleName,
    [Required] [MaxLength(30)] string LastName,
    [Required] DateOnly BirthDate,
    [Required] [AllowedValues("M", "F")] string Sex,
    [Required] [MaxLength(100)] string Address,
    [Required] [MaxLength(30)] string Nationality,
    [Required] [MaxLength(20)] string EyeColor,
    [Required] [Range(1, 500)] int Height,
    [Required] [Range(1, 500)] int Weight,
    [Required] [AllowedValues("A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-")]
    string BloodType
);