using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public int PaymentType { get; set; }

    public int PaymentStatus { get; set; }

    public Guid Appointment { get; set; }

    public virtual Booking AppointmentNavigation { get; set; } = null!;

    public virtual Status PaymentStatusNavigation { get; set; } = null!;

    public virtual PaymentType PaymentTypeNavigation { get; set; } = null!;
}
