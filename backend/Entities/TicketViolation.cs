namespace Backend.Entities;

public class TicketViolation
{
    public int TicketId { get; set; }
    public int ViolationId { get; set; }
    public decimal FineCharged { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public Violation Violation { get; set; } = null!;
}