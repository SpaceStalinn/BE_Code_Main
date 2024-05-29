using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Medium
{
    public Guid MediaId { get; set; }

    public int? MediaPath { get; set; }

    public int Creator { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual User CreatorNavigation { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
