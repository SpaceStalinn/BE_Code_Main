﻿using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public bool Status { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Fullname { get; set; }

    public DateTime? CreationDate { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<ClinicStaff> ClinicStaffs { get; set; } = new List<ClinicStaff>();

    public virtual ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual ICollection<Message> MessageReceiverNavigations { get; set; } = new List<Message>();

    public virtual Message? MessageSenderNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
