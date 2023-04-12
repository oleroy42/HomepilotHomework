using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomePilot.Db.Model;

[Table("Tenants")]
public class TenantModel
{
    public TenantModel(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [Key, Required] public Guid Id { get; private init; } = Guid.NewGuid();
    public string FirstName { get; private init; } = null!;
    public string LastName { get; private init; } = null!;
}

