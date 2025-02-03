using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AlandaProject.API.Models;

public partial class MyDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public MyDbContext(DbContextOptions<MyDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AlandaDb"));
        }
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventAttendee> EventAttendees { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<UpcomingEvent> UpcomingEvents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(150)
                .HasColumnName("event_name");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.StartDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<EventAttendee>(entity =>
        {
            entity.HasKey(e => e.EventAttendeeId).HasName("event_attendees_pkey");

            entity.ToTable("event_attendees");

            entity.Property(e => e.EventAttendeeId).HasColumnName("event_attendee_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventRegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("event_registration_date");
            entity.Property(e => e.EventStatus)
                .HasMaxLength(20)
                .HasColumnName("event_status");
            entity.Property(e => e.EventUserId).HasColumnName("event_user_id");

            entity.HasOne(d => d.Event).WithMany(p => p.EventAttendees)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("event_attendees_event_id_fkey");

            entity.HasOne(d => d.EventUser).WithMany(p => p.EventAttendees)
                .HasForeignKey(d => d.EventUserId)
                .HasConstraintName("event_attendees_event_user_id_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.MessageContent).HasColumnName("message_content");
            entity.Property(e => e.MessageIsRead)
                .HasDefaultValue(false)
                .HasColumnName("message_is_read");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.NotificationIsRead)
                .HasDefaultValue(false)
                .HasColumnName("notification_is_read");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_fkey");
        });

        modelBuilder.Entity<UpcomingEvent>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.ReminderDate }).HasName("upcoming_event_pkey");

            entity.ToTable("upcoming_event");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.ReminderDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("reminder_date");
            entity.Property(e => e.NotificationSent)
                .HasDefaultValue(false)
                .HasColumnName("notification_sent");

            entity.HasOne(d => d.Event).WithMany(p => p.UpcomingEvents)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("upcoming_event_event_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("users_roles_pkey");

            entity.ToTable("users_roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
