namespace HomePilot.Controllers.Dtos;

public class LeaseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Rent { get; set; }
}

