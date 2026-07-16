namespace MobileDataTerminal.Api.Features.Licenses;

public class License
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int TypeId { get; set; }
    public LicenseType Type { get; set; } = null!;
    public int StatusId { get; set; }
    public LicenseStatus Status { get; set; } = null!;
    public DateOnly IssuanceDate { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public int SexId { get; set; }
    public Sex Sex { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = string.Empty;
    public int NationalityId { get; set; }
    public Nationality Nationality { get; set; } = null!;
    public int EyeColorId { get; set; }
    public EyeColor EyeColor { get; set; } = null!;
    public int Height { get; set; }
    public int Weight { get; set; }
    public int BloodTypeId { get; set; }
    public BloodType BloodType { get; set; } = null!;

    // public List<Vehicle> Vehicles { get; set; } = new();
    // public List<Ticket> Tickets { get; set; } = new();
}