using MobileDataTerminal.Api.Features.Licenses;

namespace MobileDataTerminal.Api.Features.Users;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public UserRole Role { get; set; } = null!;
    public int? LicenseId { get; set; }
    public License? License { get; set; }
}