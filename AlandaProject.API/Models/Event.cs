using System;
using System.Collections.Generic;

namespace AlandaProject.API.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();

    public virtual ICollection<UpcomingEvent> UpcomingEvents { get; set; } = new List<UpcomingEvent>();
}
