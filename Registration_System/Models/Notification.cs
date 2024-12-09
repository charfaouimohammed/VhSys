using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Notification
{
    public Guid NotificationId { get; set; }

    public Guid? OwnerId { get; set; }

    public string? Message { get; set; }

    public DateTime? Timestamp { get; set; }

    public string? Status { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Owner? Owner { get; set; }
}
