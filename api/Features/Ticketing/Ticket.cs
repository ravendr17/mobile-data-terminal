using MobileDataTerminal.Api.Features.Licenses;

namespace MobileDataTerminal.Api.Features.Ticketing;

public class Ticket
{
    public int Id { get; set; }
    public int LicenseId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime SettledAt { get; set; }
    public int StatusId { get; set; }
    public TicketStatus Status { get; set; } = null!;
    public DateTime IncidentDate { get; set; }
    public string IncidentPlace { get; set; } = string.Empty;
    public string? OfficerNotes { get; set; }
    public List<TicketViolation> TicketViolations { get; set; } = new();
    public License License { get; set; } = null!;
}