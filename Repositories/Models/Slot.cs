using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Slot
{
    public Guid SlotId { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public byte Weekdays { get; set; }

    public int Clinic { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Clinic ClinicNavigation { get; set; } = null!;
}
