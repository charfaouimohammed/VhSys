using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Insurance
{
    public Guid InsuranceId { get; set; }

    public Guid? VehicleId { get; set; }

    public string? PolicyNumber { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Provider { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
