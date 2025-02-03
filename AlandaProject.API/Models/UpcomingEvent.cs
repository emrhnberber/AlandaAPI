using System;
using System.Collections.Generic;

namespace AlandaProject.API.Models;

public partial class UpcomingEvent
{
    public int EventId { get; set; }

    public DateTime ReminderDate { get; set; }

    public bool? NotificationSent { get; set; }

    public virtual Event Event { get; set; } = null!;
}
