namespace Backend.DTOs;

public record LicenseGetResponse(
    int Id,
    string Number,
    int TypeId,
    string Type,
    int StatusId,
    string Status,
    DateOnly IssuanceDate,
    DateOnly ExpirationDate,
    string FullName,
    DateOnly BirthDate,
    string Sex,
    string Address,
    string Nationality,
    string EyeColor,
    int Height,
    int Weight,
    string BloodType
);