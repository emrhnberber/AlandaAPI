using System;
using System.Collections.Generic;

namespace AlandaProject.API.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
