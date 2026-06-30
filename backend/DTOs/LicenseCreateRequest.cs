using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record LicenseCreateRequest(
    [Required(ErrorMessage = "License number is required.")] 
    [MaxLength(30, ErrorMessage = "License number cannot exceed 30 characters.")]
    [RegularExpression(@"^[A-Z0-9-]+$",
        ErrorMessage = "License number must only contain numbers, capital letters, and dashes.")]
    string Number,
    
    [Required(ErrorMessage = "License type ID is required.")] 
    int? TypeId,
    
    [Required(ErrorMessage = "License status ID is required.")] 
    int? StatusId,
    
    [Required(ErrorMessage = "Issuance date is required.")] 
    DateOnly IssuanceDate,
    
    [Required(ErrorMessage = "Validity period is required.")] 
    [AllowedValues(3, 5, ErrorMessage = "Validity must be either 3 or 5 (years).")] 
    int? Validity,
    
    [Required(ErrorMessage = "First name is required.")] 
    [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    string FirstName,
    
    [MaxLength(50, ErrorMessage = "Middle name cannot exceed 50 characters.")]
    string? MiddleName,
    
    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")] 
    string LastName,
    
    [Required(ErrorMessage = "Birth date is required.")] 
    DateOnly BirthDate,
    
    [Required(ErrorMessage = "Sex is required.")] 
    [AllowedValues("M", "F", ErrorMessage = "Sex must be either 'M' or 'F'.")] 
    string Sex,
    
    [Required(ErrorMessage = "Address is required.")] 
    [MaxLength(100, ErrorMessage = "Address cannot exceed 100 characters.")] 
    string Address,
    
    [Required(ErrorMessage = "Nationality is required.")] 
    [MaxLength(30, ErrorMessage = "Nationality cannot exceed 30 characters.")] 
    string Nationality,
    
    [Required(ErrorMessage = "Eye color is required.")] 
    [MaxLength(30, ErrorMessage = "Eye color cannot exceed 30 characters.")] 
    string EyeColor,
    
    [Required(ErrorMessage = "Height is required.")] 
    [Range(1, 999, ErrorMessage = "Height must be between 1 and 999 (cm).")] 
    int? Height,
    
    [Required(ErrorMessage = "Weight is required.")] 
    [Range(1, 999, ErrorMessage = "Weight must be between 1 and 999 (kg).")] 
    int? Weight,
    
    [Required(ErrorMessage = "Blood type is required.")] 
    [AllowedValues("A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-", 
        ErrorMessage = "Blood type must be: 'A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+' or 'O-'.")]
    string BloodType
);