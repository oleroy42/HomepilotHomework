using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HomePilot.Db.Model;


[Index(nameof(TenantId), nameof(LeaseId), IsUnique = true)]
[Table("LeaseTenants")]
public class LeaseTenantModel
{
    [Required, ForeignKey("Lease")] public Guid LeaseId { /* TODO : private */ get; private init; }
    public virtual LeaseModel Lease { get; private init; } = null!;

    [Required, ForeignKey("Tenant")] public Guid TenantId { /* TODO : private */ get; private init; }
    public virtual TenantModel Tenant { get; private init; } = null!;
}

