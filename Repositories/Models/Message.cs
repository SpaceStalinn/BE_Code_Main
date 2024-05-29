using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class Message
{
    public Guid MessageId { get; set; }

    public int Sender { get; set; }

    public int Reciever { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual User RecieverNavigation { get; set; } = null!;

    public virtual User SenderNavigation { get; set; } = null!;
}
