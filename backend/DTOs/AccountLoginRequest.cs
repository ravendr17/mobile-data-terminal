using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record AccountLoginRequest(
    [Required(ErrorMessage = "Email is required.")] 
    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    string Email,
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(16, ErrorMessage = "Password must be at least 16 characters long.")]
    [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    string Password
);