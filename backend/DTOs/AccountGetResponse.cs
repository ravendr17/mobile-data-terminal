namespace Backend.DTOs;

public record AccountGetResponse(
    int Id,
    string Username,
    string Email,
    int RoleId,
    string Role,
    int? LicenseId,
    string? LicenseNumber
);