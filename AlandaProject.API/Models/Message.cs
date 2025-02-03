using System;
using System.Collections.Generic;

namespace AlandaProject.API.Models;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string MessageContent { get; set; } = null!;

    public bool? MessageIsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
