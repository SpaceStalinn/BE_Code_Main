using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Clinic
{
    public int ClinicId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Address { get; set; }

    public int Owner { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int Status { get; set; }

    public TimeOnly? OpenHour { get; set; }

    public TimeOnly? CloseHour { get; set; }

    public virtual ICollection<ClinicService> ClinicServices { get; set; } = new List<ClinicService>();

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual Status StatusNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
