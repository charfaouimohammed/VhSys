using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class LicensePlate
{
    public Guid LicensePlateId { get; set; }

    public Guid? VehicleId { get; set; }

    public string? PlateNumber { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<TrafficViolation> TrafficViolations { get; set; } = new List<TrafficViolation>();

    public virtual Vehicle? Vehicle { get; set; }
}
