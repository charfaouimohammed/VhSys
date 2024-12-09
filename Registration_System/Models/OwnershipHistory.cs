using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class OwnershipHistory
{
    public Guid OwnershipId { get; set; }

    public Guid? VehicleId { get; set; }

    public Guid? OwnerId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsCurrentOwner { get; set; }

    public virtual Owner? Owner { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
