﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HomePilot.Db.Model;

[Table("Amendments")]

public class AmendmentModel
{
    [Key, Required] public Guid Id { get; private init; } = Guid.NewGuid();
    public DateTimeOffset CreationDate { get; private init; } = DateTime.UtcNow;

    [Required, ForeignKey("Lease")] public Guid LeaseId { get; private init; }
    public virtual LeaseModel Lease { get; private init; } = null!;

    public DateTimeOffset EffectiveDate { get; private init; } = default!;

    public int? OldRentInCents { get; private init; }

    public string? Comment { get;private init; }

    [InverseProperty("AmendmentEntry")]
    public virtual IList<LeaseTenantModel> Entries { get; set; } = new List<LeaseTenantModel>();

    [InverseProperty("AmendmentExit")]
    public virtual IList<LeaseTenantModel> Exits { get; set; } = new List<LeaseTenantModel>();

    public AmendmentModel(Guid leaseId, DateTimeOffset effectiveDate, int? oldRentInCents, string? comment)
    {
        LeaseId = leaseId;
        EffectiveDate = effectiveDate;
        OldRentInCents = oldRentInCents;
        Comment = comment;
    }

}

