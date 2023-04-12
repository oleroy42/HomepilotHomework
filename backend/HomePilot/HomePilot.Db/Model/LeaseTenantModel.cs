using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HomePilot.Db.Model;


[Index(nameof(TenantId), nameof(LeaseId), IsUnique = true)]
[Table("LeaseTenants")]
public class LeaseTenantModel
{
    public LeaseTenantModel(Guid leaseId, Guid tenantId, Guid? amendmentEntryId)
    {
        LeaseId = leaseId;
        TenantId = tenantId;
        AmendmentEntryId = amendmentEntryId;
    }

    [Required, ForeignKey("Lease")] public Guid LeaseId { get; private init; }
    public virtual LeaseModel Lease { get; private init; } = null!;

    [Required, ForeignKey("Tenant")] public Guid TenantId { get; private init; }
    public virtual TenantModel Tenant { get; private init; } = null!;

    [ForeignKey("AmendmentEntry")] public Guid? AmendmentEntryId { get; set; }
    public virtual AmendmentModel? AmendmentEntry { get; private set; } = null!;

    [ForeignKey("AmendmentExit")] public Guid? AmendmentExitId { get; set; }
    public virtual AmendmentModel? AmendmentExit { get; private set; } = null!;
}

