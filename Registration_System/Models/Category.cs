using System;
using System.Collections.Generic;

namespace Registration_System.Models;

public partial class Category
{
    public int CategorieId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LicenseCategory> LicenseCategories { get; set; } = new List<LicenseCategory>();
}
