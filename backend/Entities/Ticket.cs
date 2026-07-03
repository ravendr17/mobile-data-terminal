namespace Backend.Entities;

public class Ticket
{
    public int Id { get; set; }
    public int LicenseId { get; set; }
    public ulong ReferenceNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SettledAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; }
    public string IncidentPlace { get; set; } = string.Empty;
    public string OfficerNotes { get; set; } = string.Empty;
    public List<TicketViolation> TicketViolations { get; set; } = new();

    public License License { get; set; } = null!;
};