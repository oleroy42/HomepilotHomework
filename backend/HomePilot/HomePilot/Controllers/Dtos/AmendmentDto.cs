namespace HomePilot.Controllers.Dtos;

public class AmendmentDto
{
    public Guid Id { get; set; }
    public LeaseDto Lease { get; set; } = null!;

    public DateTimeOffset EffectiveDate { get; set; }
    public List<TenantDto> Entries { get; set; } = new List<TenantDto>();
    public List<TenantDto> Exits { get; set; } = new List<TenantDto>();
    public int? OldRent { get; set; }

    public static AmendmentDto GetDefaultAmendmentDto()
    {
        return new AmendmentDto
        {
            EffectiveDate = DateTime.Now,
            Entries = new() { new TenantDto { FirstName = "In", LastName = "LastName", Id = Guid.NewGuid() } },
            Id = Guid.NewGuid(),
            OldRent = 25000,
            Exits = new() { new TenantDto { FirstName = "Out", LastName = "LastName", Id = Guid.NewGuid() } },
            Lease = new LeaseDto { Id = Guid.NewGuid(), Name = "Lease 1", Rent = 20000 }
        };
    }
}
