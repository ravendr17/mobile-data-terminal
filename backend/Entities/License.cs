namespace MobileDataTerminalAPI.Entities;

public class License
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public int TypeId { get; set; }
    public int StatusId { get; set; }
    public DateOnly IssuanceDate { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Sex { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string EyeColor { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Weight { get; set; }
    public string BloodType { get; set; } = string.Empty;

    public List<Vehicle> Vehicles { get; set; } = [];
}