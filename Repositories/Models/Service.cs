using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ClinicService> ClinicServices { get; set; } = new List<ClinicService>();
}
