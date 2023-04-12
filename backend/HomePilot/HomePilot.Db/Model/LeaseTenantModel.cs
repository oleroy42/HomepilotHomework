using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HomePilot.Db.Model;


[Index(nameof(TenantId), nameof(LeaseId), IsUnique = true)]
[Table("LeaseTenants")]
public class LeaseTenantModel
{
    [Required, ForeignKey("Lease")] public Guid LeaseId { get; private init; }
    public virtual LeaseModel Lease { get; private init; } = null!;

    [Required, ForeignKey("Tenant")] public Guid TenantId { get; private init; }
    public virtual TenantModel Tenant { get; private init; } = null!;

    [ForeignKey("AmendmentEntry")] public Guid? AmendmentEntryId { get; private set; }
    public virtual AmendmentModel? AmendmentEntry { get; private set; } = null!;

    [ForeignKey("AmendmentExit")] public Guid? AmendmentExitId { get; private set; }
    public virtual AmendmentModel? AmendmentExit { get; private set; } = null!;
}

