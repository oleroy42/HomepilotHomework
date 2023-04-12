using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePilot.Db.Model;

[Table("Leases")]
public class LeaseModel
{
    [Key, Required] public Guid Id { get; private init; } = Guid.NewGuid();

    public string Name { get; private init; } = null!;

    public DateTimeOffset StartDate { get; private init; }
    public DateTimeOffset? EndDate { get; private init; }
    public int RentInCents { get; private init; }

    public LeaseModel(string name, DateTimeOffset startDate, DateTimeOffset? endDate, int rentInCents)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        RentInCents = rentInCents;
    }

    public LeaseModel(string name, DateTimeOffset startDate, int rentInCents)
    {
        Name = name;
        StartDate = startDate;
        RentInCents = rentInCents;
    }
}

