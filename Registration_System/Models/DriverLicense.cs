using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class DriverLicense
{
    public Guid LicenseId { get; set; }

    public Guid? OwnerId { get; set; }

    public string? LicenseNumber { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<LicenseCategory> LicenseCategoriesNavigation { get; set; } = new List<LicenseCategory>();

    public virtual Owner? Owner { get; set; }

    public virtual ICollection<TrafficViolation> TrafficViolations { get; set; } = new List<TrafficViolation>();
}
