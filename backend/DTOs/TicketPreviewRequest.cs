using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs;

public record TicketPreviewRequest(
    [Required(ErrorMessage = "License ID is required.")]
    int LicenseId,
    
    [Required(ErrorMessage = "Incident date is required.")]
    DateTime IncidentDate,
    
    [Required(ErrorMessage = "Incident place is required.")]
    [MaxLength(100, ErrorMessage = "Incident place cannot exceed 100 characters.")]
    string IncidentPlace,
    
    [MaxLength(255, ErrorMessage = "Officer notes cannot exceed 255 characters.")]
    string? OfficerNotes,
    
    [Required(ErrorMessage = "At least 1 violation required.")]
    [MinLength(1, ErrorMessage = "At least 1 violation required.")]
    List<int> ViolationsIds
);