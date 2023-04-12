namespace HomePilot.Controllers.Dtos;

public class AmendmentDto
{
    public Guid Id { get; set; }
    public LeaseDto Lease { get; set; } = null!;

    public DateTimeOffset EffectiveDate { get; set; }
    public List<TenantDto> Entries { get; set; } = new List<TenantDto>();
    public List<TenantDto> Exits { get; set; } = new List<TenantDto>();
    public int OldRent { get; set; }
}
