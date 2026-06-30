using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record AccountLoginRequest(
    [Required(ErrorMessage = "Username or Email is required.")] 
    [MaxLength(100, ErrorMessage = "Username or Email cannot exceed 100 characters.")]
    string Identifier,
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(16, ErrorMessage = "Password must be at least 16 characters long.")]
    [MaxLength(255, ErrorMessage = "Password cannot exceed 255 characters.")]
    string Password
);