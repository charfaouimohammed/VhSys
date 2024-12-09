using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class RegistrationOffice
{
    public Guid OfficeId { get; set; }

    public string? OfficeName { get; set; }

    public string? Address { get; set; }

    public Guid? AdminId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Administration? Admin { get; set; }

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
