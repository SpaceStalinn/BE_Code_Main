using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class ClinicService
{
    public Guid ClainService { get; set; }

    public int ServiceserviceId { get; set; }

    public int ClinicclinicId { get; set; }

    public virtual Clinic Clinicclinic { get; set; } = null!;

    public virtual Service Serviceservice { get; set; } = null!;
}
