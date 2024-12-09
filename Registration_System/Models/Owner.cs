using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Owner
{
    public Guid OwnerId { get; set; }

    public string? Cin { get; set; }

    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<DriverLicense> DriverLicenses { get; set; } = new List<DriverLicense>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<OwnershipHistory> OwnershipHistories { get; set; } = new List<OwnershipHistory>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
