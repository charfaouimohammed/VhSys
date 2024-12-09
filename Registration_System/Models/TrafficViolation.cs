using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class TrafficViolation
{
    public Guid ViolationId { get; set; }

    public Guid? LicensePlateId { get; set; }

    public Guid? DriverLicenseId { get; set; }

    public string? ViolationType { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? FineAmount { get; set; }

    public string? PaymentStatus { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual DriverLicense? DriverLicense { get; set; }

    public virtual LicensePlate? LicensePlate { get; set; }
}
