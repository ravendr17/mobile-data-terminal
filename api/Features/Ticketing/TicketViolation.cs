namespace MobileDataTerminal.Api.Features.Ticketing;

public class TicketViolation
{
    public int TicketId { get; set; }
    public int ViolationId { get; set; }
    public decimal Fine { get; set; }
}