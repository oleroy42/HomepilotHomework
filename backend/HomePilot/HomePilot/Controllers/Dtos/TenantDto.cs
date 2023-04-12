namespace HomePilot.Controllers.Dtos; 
public class TenantDto {
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
