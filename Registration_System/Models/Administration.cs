using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Administration
{
    public Guid AdminId { get; set; }

    public string? Username { get; set; }

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Email { get; set; }

    public string? Fullname { get; set; }

    public bool? IsActif { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<RegistrationOffice> RegistrationOffices { get; set; } = new List<RegistrationOffice>();
}
