using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record VehicleCreateRequest(
    [Required] [MaxLength(20)] string PlateNumber,
    [MaxLength(20)] string? MvFileNumber,
    [Required] [MaxLength(20)] string Vin,
    [Required] DateOnly RegisterIssuanceDate,
    [Required] [AllowedValues(1, 3)] int? Validity,
    [Required] [MaxLength(30)] string Make,
    [Required] [MaxLength(30)] string Model,
    [Required] [Range(1900, 2999)] int? Year,
    [Required] [MaxLength(30)] string Color,
    [Required] [MaxLength(30)] string LicenseNumber
);