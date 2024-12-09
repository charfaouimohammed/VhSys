using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class AuditLog
{
    public Guid LogId { get; set; }

    public Guid? AdminId { get; set; }

    public string? Action { get; set; }

    public DateTime? Timestamp { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Administration? Admin { get; set; }
}
