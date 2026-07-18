namespace MobileDataTerminal.Api.Features.Ticketing;

public class TicketViolation
{
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; } = null!;
    public int ViolationId { get; set; }
    public Violation Violation { get; set; } = null!;
    public decimal Fine { get; set; }
}