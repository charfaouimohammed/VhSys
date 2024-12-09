using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class LicenseCategory
{
    public int LicenseCategorieId { get; set; }

    public Guid LicenseId { get; set; }

    public int CategorieId { get; set; }

    public virtual Category Categorie { get; set; } = null!;

    public virtual DriverLicense License { get; set; } = null!;
}
