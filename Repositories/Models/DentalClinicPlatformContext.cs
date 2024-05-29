using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Clinic> Clinics { get; set; }

    public virtual DbSet<ClinicService> ClinicServices { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=COLLINLAPTOP\\SQLEXPRESS;database=DentalClinicPlatform;Encrypt=True;TrustServerCertificate=True;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Booking__490D1AE1FCE25E25");

            entity.ToTable("Booking");

            entity.Property(e => e.BookId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("book_id");
            entity.Property(e => e.AppointmentDate).HasColumnName("appointment_date");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Customer).HasColumnName("customer");
            entity.Property(e => e.Dentist).HasColumnName("dentist");
            entity.Property(e => e.Slot).HasColumnName("slot");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.BookingCustomerNavigations)
                .HasForeignKey(d => d.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking941670");

            entity.HasOne(d => d.DentistNavigation).WithMany(p => p.BookingDentistNavigations)
                .HasForeignKey(d => d.Dentist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking369135");

            entity.HasOne(d => d.SlotNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Slot)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking797160");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKBooking12906");
        });

        modelBuilder.Entity<Clinic>(entity =>
        {
            entity.HasKey(e => e.ClinicId).HasName("PK__Clinic__A0C8D19BF6A824FA");

            entity.ToTable("Clinic");

            entity.Property(e => e.ClinicId).HasColumnName("clinic_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CloseHour).HasColumnName("close_hour");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OpenHour).HasColumnName("open_hour");
            entity.Property(e => e.Owner).HasColumnName("owner");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.OwnerNavigation).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.Owner)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinic810483");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Clinics)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinic804252");
        });

        modelBuilder.Entity<ClinicService>(entity =>
        {
            entity.HasKey(e => e.ClainService).HasName("PK__Clinic_S__08AC041E62DDCB97");

            entity.ToTable("Clinic_Service");

            entity.Property(e => e.ClainService)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("clain_service");
            entity.Property(e => e.ClinicclinicId).HasColumnName("Clinicclinic_id");
            entity.Property(e => e.ServiceserviceId).HasColumnName("Serviceservice_id");

            entity.HasOne(d => d.Clinicclinic).WithMany(p => p.ClinicServices)
                .HasForeignKey(d => d.ClinicclinicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinic_Ser816210");

            entity.HasOne(d => d.Serviceservice).WithMany(p => p.ClinicServices)
                .HasForeignKey(d => d.ServiceserviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClinic_Ser554189");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Media__D0A840F41129BE85");

            entity.Property(e => e.MediaId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("media_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Creator).HasColumnName("creator");
            entity.Property(e => e.MediaPath).HasColumnName("media_path");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Media)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMedia288296");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__0BBF6EE67DE43C7F");

            entity.ToTable("Message");

            entity.HasIndex(e => e.Sender, "UQ__Message__C605FA96F20592D5").IsUnique();

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
            entity.Property(e => e.Reciever).HasColumnName("reciever");
            entity.Property(e => e.Sender).HasColumnName("sender");

            entity.HasOne(d => d.RecieverNavigation).WithMany(p => p.MessageRecieverNavigations)
                .HasForeignKey(d => d.Reciever)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMessage948125");

            entity.HasOne(d => d.SenderNavigation).WithOne(p => p.MessageSenderNavigation)
                .HasForeignKey<Message>(d => d.Sender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKMessage130039");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED1FC9EA209F90B8");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("payment_id");
            entity.Property(e => e.Appointment).HasColumnName("appointment");
            entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");
            entity.Property(e => e.PaymentType).HasColumnName("payment_type");

            entity.HasOne(d => d.AppointmentNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Appointment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPayment993691");

            entity.HasOne(d => d.PaymentStatusNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPayment787020");

            entity.HasOne(d => d.PaymentTypeNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPayment556319");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__PaymentT__2C00059866CEAEF9");

            entity.ToTable("PaymentType");

            entity.HasIndex(e => e.TypeProvider, "UQ__PaymentT__8E93B7A3FA49B73C").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeProvider)
                .HasMaxLength(45)
                .HasColumnName("type_provider");
            entity.Property(e => e.TypeProviderSecret)
                .HasMaxLength(255)
                .HasColumnName("type_provider_secret");
            entity.Property(e => e.TypeProviderToken)
                .HasMaxLength(255)
                .HasColumnName("type_provider_token");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Result__AFB3C3161BF0D31B");

            entity.ToTable("Result");

            entity.Property(e => e.ResultId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("result_id");
            entity.Property(e => e.Appointment).HasColumnName("appointment");
            entity.Property(e => e.CreatiomDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creatiom_date");
            entity.Property(e => e.Creator).HasColumnName("creator");
            entity.Property(e => e.Media).HasColumnName("media");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");

            entity.HasOne(d => d.AppointmentNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.Appointment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKResult540405");

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.Creator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKResult492515");

            entity.HasOne(d => d.MediaNavigation).WithMany(p => p.Results)
                .HasForeignKey(d => d.Media)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKResult627124");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__760965CCF7C593E2");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__783254B11903203B").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__3E0DB8AFA3D2EF98");

            entity.ToTable("Service");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slot__971A01BB14EC7BEF");

            entity.ToTable("Slot");

            entity.Property(e => e.SlotId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("slot_id");
            entity.Property(e => e.Clinic).HasColumnName("clinic");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Weekdays).HasColumnName("weekdays");

            entity.HasOne(d => d.ClinicNavigation).WithMany(p => p.Slots)
                .HasForeignKey(d => d.Clinic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKSlot545721");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Status__3683B531F31AF59D");

            entity.ToTable("Status");

            entity.HasIndex(e => e.StatusName, "UQ__Status__501B375359B7B488").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(255)
                .HasColumnName("status_description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__B9BE370F88D5CC48");

            entity.ToTable("User");

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC572F4BAFB03").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ClinicDentist).HasColumnName("clinic_dentist");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(60)
                .HasColumnName("fullname");
            entity.Property(e => e.Insurance)
                .HasMaxLength(20)
                .HasColumnName("insurance");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(255)
                .HasColumnName("profile_pic");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .HasColumnName("username");

            entity.HasOne(d => d.ClinicDentistNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.ClinicDentist)
                .HasConstraintName("FKUser6807");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser590756");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser682274");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
