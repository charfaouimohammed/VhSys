using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class InspectionRecord
{
    public Guid InspectionId { get; set; }

    public Guid? VehicleId { get; set; }

    public DateOnly? InspectionDate { get; set; }

    public string? Result { get; set; }

    public string? InspectorName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
