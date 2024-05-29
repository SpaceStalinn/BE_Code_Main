using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Result
{
    public Guid ResultId { get; set; }

    public int Creator { get; set; }

    public Guid Appointment { get; set; }

    public Guid Media { get; set; }

    public string? Note { get; set; }

    public DateTime CreatiomDate { get; set; }

    public virtual Booking AppointmentNavigation { get; set; } = null!;

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual Medium MediaNavigation { get; set; } = null!;
}
