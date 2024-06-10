﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories.Models;

public partial class DentalClinicPlatformContext : DbContext
{
    public DentalClinicPlatformContext()
    {
    }

    public DentalClinicPlatformContext(DbContextOptions<DentalClinicPlatformContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicService> ClinicServices { get; set; }

    public virtual DbSet<ClinicStaff> ClinicStaffs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ScheduledSlot> ScheduledSlots { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

        var connectionString = config.GetConnectionString("Database");

        if (connectionString == null)
        {
            throw new Exception("Connection string not found! Please check your configuration files.");
        }

        return connectionString;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Booking__490D1AE155187CE4");

            entity.ToTable("Booking");

            entity.Property(e => e.BookId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("book_id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
            entity.Property(e => e.BookingType)
                .HasMaxLength(50)
                .HasColumnName("booking_type");
            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DentistId).HasColumnName("dentist_id");
            entity.Property(e => e.ScheduleSlotId).HasColumnName("schedule_slot_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking367844");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking965192");

            entity.HasOne(d => d.Dentist).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.DentistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking214257");

            entity.HasOne(d => d.ScheduleSlot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ScheduleSlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking760309");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.HasKey(e => e.CertificationId).HasName("PK__Certific__185D5AEC91A28E82");

            entity.ToTable("Certification");

            entity.Property(e => e.CertificationId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("certification_id");
            entity.Property(e => e.CertificationUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("certification_url");
            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Clinic).WithMany(p => p.Certifications)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCertificat536417");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__A0C8D19B31BC48AB");

            entity.ToTable("Clinic");

            entity.HasIndex(e => e.Email, "UQ__Clinic__AB6E6164F2CEFA22").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Clinic__B43B145FD0B8AEFE").IsUnique();

            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CloseHour).HasColumnName("close_hour");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OpenHour).HasColumnName("open_hour");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Owner).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinic176906");
        });

        modelBuilder.Entity<ClinicService>(entity =>
        {
            entity.HasKey(e => e.ClinicServiceId).HasName("PK__ClinicSe__916E631CCCD232B4");

            entity.Property(e => e.ClinicServiceId).HasColumnName("clinic_service_id");
            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicServices)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinicServ622868");

            entity.HasOne(d => d.Service).WithMany(p => p.ClinicServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinicServ95478");
        });

        modelBuilder.Entity<ClinicStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__ClinicSt__1963DD9CDEB5F211");

            entity.ToTable("ClinicStaff");

            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.IsOwner).HasColumnName("is_owner");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Clinic).WithMany(p => p.ClinicStaffs)
                .HasForeignKey(d => d.ClinicId)
                .HasConstraintName("FKClinicStaf705438");

            entity.HasOne(d => d.User).WithMany(p => p.ClinicStaffs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinicStaf352227");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB8503200D85");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Insurance)
                .HasMaxLength(20)
                .HasColumnName("insurance");
            entity.Property(e => e.Sex)
                .HasMaxLength(10)
                .HasColumnName("sex");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCustomer199874");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__MediaTyp__2C000598C6784532");

            entity.ToTable("MediaType");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Media__D0A840F42165CB26");

            entity.Property(e => e.MediaId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("media_id");
            entity.Property(e => e.CertificationId).HasColumnName("certification_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatorId).HasColumnName("creator_id");
            entity.Property(e => e.MediaPath).HasColumnName("media_path");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Certification).WithMany(p => p.Media)
                .HasForeignKey(d => d.CertificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMedia365831");

            entity.HasOne(d => d.Creator).WithMany(p => p.Media)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMedia66473");

            entity.HasOne(d => d.Type).WithMany(p => p.Media)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMedia498708");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__0BBF6EE6F4698E99");

            entity.HasIndex(e => e.Sender, "UQ__Messages__C605FA9695B7B7B0").IsUnique();

            entity.Property(e => e.MessageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("message_id");
            entity.Property(e => e.Content)
                .HasMaxLength(1000)
                .HasColumnName("content");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Receiver).HasColumnName("receiver");
            entity.Property(e => e.Sender).HasColumnName("sender");

            entity.HasOne(d => d.ReceiverNavigation).WithMany(p => p.MessageReceiverNavigations)
                .HasForeignKey(d => d.Receiver)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMessages658033");

            entity.HasOne(d => d.SenderNavigation).WithOne(p => p.MessageSenderNavigation)
                .HasForeignKey<Message>(d => d.Sender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMessages901196");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA32A84930");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.MadeOn)
                .HasColumnType("datetime")
                .HasColumnName("made_on");
            entity.Property(e => e.PaymentType).HasColumnName("payment_type");
            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPayment428514");

            entity.HasOne(d => d.PaymentTypeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FKPayment702148");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__PaymentT__2C000598B7BA90E8");

            entity.ToTable("PaymentType");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeProvider)
                .HasMaxLength(50)
                .HasColumnName("type_provider");
            entity.Property(e => e.TypeProviderSecret)
                .HasMaxLength(255)
                .HasColumnName("type_provider_secret");
            entity.Property(e => e.TypeProviderToken)
                .HasMaxLength(255)
                .HasColumnName("type_provider_token");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CC12CEE71B");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__783254B1868A9293").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<ScheduledSlot>(entity =>
        {
            entity.HasKey(e => e.ScheduleSlotId).HasName("PK__Schedule__54B44F592FACD241");

            entity.ToTable("ScheduledSlot");

            entity.Property(e => e.ScheduleSlotId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("schedule_slot_id");
            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.DateOfWeek).HasColumnName("date_of_week");
            entity.Property(e => e.MaxAppointments).HasColumnName("max_appointments");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");

            entity.HasOne(d => d.Clinic).WithMany(p => p.ScheduledSlots)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKScheduledS448005");

            entity.HasOne(d => d.Slot).WithMany(p => p.ScheduledSlots)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKScheduledS501966");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__3E0DB8AF52ED01E7");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .HasColumnName("service_name");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__971A01BBE90B4B19");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("slot_id");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F5FB8F396");

            entity.ToTable("User");

            entity.HasIndex(e => e.PhoneNumber, "UQ__User__A1936A6BF931A799").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User__AB6E616456D43466").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC5725B18CA45").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser1655");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
