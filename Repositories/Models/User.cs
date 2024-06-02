using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfilePic { get; set; }

    public string? Fullname { get; set; }

    public string Email { get; set; } = String.Empty;

    public string? Phone { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Insurance { get; set; }

    public int Status { get; set; }

    public int Role { get; set; }

    public int? ClinicDentist { get; set; }

    public virtual ICollection<Booking> BookingCustomerNavigations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingDentistNavigations { get; set; } = new List<Booking>();

    public virtual Clinic? ClinicDentistNavigation { get; set; }

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual ICollection<Message> MessageRecieverNavigations { get; set; } = new List<Message>();

    public virtual Message? MessageSenderNavigation { get; set; }

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual Status StatusNavigation { get; set; } = null!;
}
