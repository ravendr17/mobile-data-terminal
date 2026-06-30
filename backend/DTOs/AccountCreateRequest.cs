using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record AccountCreateRequest(
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(8, ErrorMessage = "Username must be at least 8 characters long.")]
    [MaxLength(30, ErrorMessage = "Username cannot exceed 30 characters.")]
    string Username,
    
    [Required(ErrorMessage = "Email is required.")] 
    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    string Email,
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(16, ErrorMessage = "Password must be at least 16 characters long.")]
    [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    string Password,
    
    [Required(ErrorMessage = "Role ID is required.")]
    int RoleId,
    
    [MaxLength(30, ErrorMessage = "License number cannot exceed 30 characters.")]
    string? LicenseNumber
);