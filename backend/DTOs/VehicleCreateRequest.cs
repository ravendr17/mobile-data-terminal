using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record VehicleCreateRequest(
    [Required(ErrorMessage = "Plate number is required.")] 
    [MaxLength(10, ErrorMessage = "Plate number cannot exceed 10 characters.")]
    [RegularExpression(@"^[A-Z0-9 -]+$",
        ErrorMessage = "Plate number must only contain capital letters, numbers, spaces, or dashes.")]
    string PlateNumber,
    
    [MaxLength(20, ErrorMessage = "MV File number cannot exceed 20 characters.")] 
    [RegularExpression(@"^[A-Z0-9 -]+$",
        ErrorMessage = "MV File number must only contain capital letters, numbers, spaces, or dashes.")]
    string? MvFileNumber,
    
    [Required(ErrorMessage = "VIN is required.")] 
    [MaxLength(20, ErrorMessage = "VIN cannot exceed 20 characters.")] 
    [RegularExpression(@"^[A-Z0-9]+$",
        ErrorMessage = "VIN must only contain capital letters or numbers.")]
    string Vin,
    
    [Required(ErrorMessage = "Register issuance date is required.")] 
    DateOnly RegisterIssuanceDate,
    
    [Required(ErrorMessage = "Validity period is required.")] 
    [AllowedValues(1, 3, ErrorMessage = "Validity must be either 1 or 3 (years).")] 
    int? Validity,
    
    [Required(ErrorMessage = "Make is required.")] 
    [MaxLength(30, ErrorMessage = "Make cannot exceed 30 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9 .'-]+$",
        ErrorMessage = "Make must only contain letters, numbers, spaces, dots, apostrophes or dashes.")]
    string Make,
    
    [Required(ErrorMessage = "Model is required.")] 
    [MaxLength(30, ErrorMessage = "Model cannot exceed 30 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9 .'-]+$",
        ErrorMessage = "Model must only contain letters, numbers, spaces, dots, apostrophes or dashes.")]
    string Model,
    
    [Required(ErrorMessage = "Year is required.")] 
    [Range(1900, 2999, ErrorMessage = "Year must be between 1900 and 2999.")] 
    int? Year,
    
    [Required(ErrorMessage = "Color is required.")] 
    [MaxLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
    [RegularExpression(@"^[a-zA-Z ]+$",
        ErrorMessage = "Color must only contain letters or spaces.")]
    string Color,
    
    [Required(ErrorMessage = "License number is required.")] 
    [MaxLength(30, ErrorMessage = "License number cannot exceed 30 characters.")]
    [RegularExpression(@"^[A-Z0-9-]+$",
        ErrorMessage = "License number must only contain numbers, capital letters, or dashes.")]
    string LicenseNumber
);