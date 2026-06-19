namespace Backend.DTOs;

public record VehicleGetResponse(
    int Id,
    string PlateNumber,
    string? MvFileNumber,
    string Vin,
    DateOnly RegisterIssuanceDate,
    DateOnly RegisterExpiryDate,
    string Make,
    string Model,
    int Year,
    string Color,
    string OwnerName,
    string LicenseNumber,
    string Address
);