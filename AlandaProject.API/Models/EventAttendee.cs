using System;
using System.Collections.Generic;

namespace AlandaProject.API.Models;

public partial class EventAttendee
{
    public int EventAttendeeId { get; set; }

    public int? EventUserId { get; set; }

    public int? EventId { get; set; }

    public string EventStatus { get; set; } = null!;

    public DateTime? EventRegistrationDate { get; set; }

    public virtual Event? Event { get; set; }

    public virtual User? EventUser { get; set; }
}
