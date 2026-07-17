using MobileDataTerminal.Api.Features.Licenses;

namespace MobileDataTerminal.Api.Features.Vehicles;

public class Vehicle
{
    public int Id { get; set; }
    public string PlateNumber { get; set; } = string.Empty;
    public string? MvFileNumber { get; set; } = string.Empty;
    public string Vin { get; set; } = string.Empty;
    public DateOnly RegisterIssuanceDate { get; set; }
    public DateOnly RegisterExpiryDate { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public int? LicenseId { get; set; }
    public License? License;
}