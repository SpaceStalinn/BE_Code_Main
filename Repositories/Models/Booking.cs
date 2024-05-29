using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Booking
{
    public Guid BookId { get; set; }

    public int Customer { get; set; }

    public int Dentist { get; set; }

    public Guid Slot { get; set; }

    public DateOnly AppointmentDate { get; set; }

    public DateTime CreationDate { get; set; }

    public int Status { get; set; }

    public virtual User CustomerNavigation { get; set; } = null!;

    public virtual User DentistNavigation { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Slot SlotNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
