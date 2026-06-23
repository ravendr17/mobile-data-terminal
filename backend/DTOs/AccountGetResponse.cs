namespace Backend.DTOs;

public record AccountGetResponse(
    int Id,
    int RoleId,
    string Role,
    int? LicenseId,
    string? LicenseNumber
);