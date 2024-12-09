using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Vehicle
{
    public Guid VehicleId { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }

    public string? Color { get; set; }

    public Guid? RegistrationOfficeId { get; set; }

    public Guid? CurrentOwnerId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Owner? CurrentOwner { get; set; }

    public virtual ICollection<InspectionRecord> InspectionRecords { get; set; } = new List<InspectionRecord>();

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    public virtual ICollection<LicensePlate> LicensePlates { get; set; } = new List<LicensePlate>();

    public virtual ICollection<OwnershipHistory> OwnershipHistories { get; set; } = new List<OwnershipHistory>();

    public virtual RegistrationOffice? RegistrationOffice { get; set; }
}
