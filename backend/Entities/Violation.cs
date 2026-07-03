namespace Backend.Entities;

public class Violation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsTiered { get; set; }
    public decimal InitialFine { get; set; }
    public decimal? SecondFine { get; set; }
    public decimal? ThirdFine { get; set; }
}