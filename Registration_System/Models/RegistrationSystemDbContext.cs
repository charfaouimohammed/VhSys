using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Registration_System.Models;

public partial class RegistrationSystemDbContext : DbContext
{
    public RegistrationSystemDbContext()
    {
    }

    public RegistrationSystemDbContext(DbContextOptions<RegistrationSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administration> Administrations { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DriverLicense> DriverLicenses { get; set; }

    public virtual DbSet<InspectionRecord> InspectionRecords { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<LicenseCategory> LicenseCategories { get; set; }

    public virtual DbSet<LicensePlate> LicensePlates { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<OwnershipHistory> OwnershipHistories { get; set; }

    public virtual DbSet<RegistrationOffice> RegistrationOffices { get; set; }

    public virtual DbSet<TrafficViolation> TrafficViolations { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-V6UG0P6\\MS22;Database=Registration_SystemDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administration>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Administ__43AA41418D2F31D8");

            entity.ToTable("Administration");

            entity.Property(e => e.AdminId)
                .ValueGeneratedNever()
                .HasColumnName("admin_id");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Fullname).HasMaxLength(255);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__AuditLog__9E2397E0B566BADE");

            entity.ToTable("AuditLog");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("log_id");
            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .HasColumnName("action");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Admin).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__AuditLog__admin___2E1BDC42");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategorieId).HasName("PK__Categori__F643ADA693990DD8");

            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DriverLicense>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__DriverLi__BBBB75787C05A033");

            entity.ToTable("DriverLicense");

            entity.Property(e => e.LicenseId)
                .ValueGeneratedNever()
                .HasColumnName("license_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("license_number");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.DriverLicenses)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__DriverLic__owner__36B12243");
        });

        modelBuilder.Entity<InspectionRecord>(entity =>
        {
            entity.HasKey(e => e.InspectionId).HasName("PK__Inspecti__C3C4E7431E5F9814");

            entity.ToTable("InspectionRecord");

            entity.Property(e => e.InspectionId)
                .ValueGeneratedNever()
                .HasColumnName("inspection_id");
            entity.Property(e => e.InspectionDate).HasColumnName("inspection_date");
            entity.Property(e => e.InspectorName)
                .HasMaxLength(100)
                .HasColumnName("inspector_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Result)
                .HasMaxLength(50)
                .HasColumnName("result");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.InspectionRecords)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Inspectio__vehic__4AB81AF0");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceId).HasName("PK__Insuranc__58B60F45F2E4DBF8");

            entity.ToTable("Insurance");

            entity.Property(e => e.InsuranceId)
                .ValueGeneratedNever()
                .HasColumnName("insurance_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(50)
                .HasColumnName("policy_number");
            entity.Property(e => e.Provider)
                .HasMaxLength(100)
                .HasColumnName("provider");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Insurance__vehic__46E78A0C");
        });

        modelBuilder.Entity<LicenseCategory>(entity =>
        {
            entity.HasKey(e => e.LicenseCategorieId).HasName("PK__LicenseC__0CC5B035F2B44CCC");

            entity.ToTable("LicenseCategories");

            entity.HasOne(d => d.Categorie).WithMany(p => p.LicenseCategories)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LicenseCategories_Categories");

            entity.HasOne(d => d.License).WithMany(p => p.LicenseCategoriesNavigation)
                .HasForeignKey(d => d.LicenseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LicenseCategories_DriverLicense");
        });

        modelBuilder.Entity<LicensePlate>(entity =>
        {
            entity.HasKey(e => e.LicensePlateId).HasName("PK__LicenseP__9132323940DEE6A2");

            entity.ToTable("LicensePlate");

            entity.Property(e => e.LicensePlateId)
                .ValueGeneratedNever()
                .HasColumnName("license_plate_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(20)
                .HasColumnName("plate_number");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.LicensePlates)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__LicensePl__vehic__3E52440B");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F975B2C25");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId)
                .ValueGeneratedNever()
                .HasColumnName("notification_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .HasColumnName("message");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Owner).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Notificat__owner__3A81B327");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owner__3C4FBEE46ACF20FA");

            entity.ToTable("Owner");

            entity.Property(e => e.OwnerId)
                .ValueGeneratedNever()
                .HasColumnName("owner_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Cin)
                .HasMaxLength(20)
                .HasColumnName("cin");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<OwnershipHistory>(entity =>
        {
            entity.HasKey(e => e.OwnershipId).HasName("PK__Ownershi__056D1137FC5DE03B");

            entity.ToTable("OwnershipHistory");

            entity.Property(e => e.OwnershipId)
                .ValueGeneratedNever()
                .HasColumnName("ownership_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnershipHistories)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Ownership__owner__4316F928");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.OwnershipHistories)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Ownership__vehic__4222D4EF");
        });

        modelBuilder.Entity<RegistrationOffice>(entity =>
        {
            entity.HasKey(e => e.OfficeId).HasName("PK__Registra__2A1963757C081695");

            entity.ToTable("RegistrationOffice");

            entity.Property(e => e.OfficeId)
                .ValueGeneratedNever()
                .HasColumnName("office_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.OfficeName)
                .HasMaxLength(100)
                .HasColumnName("office_name");

            entity.HasOne(d => d.Admin).WithMany(p => p.RegistrationOffices)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__Registrat__admin__276EDEB3");
        });

        modelBuilder.Entity<TrafficViolation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("PK__TrafficV__8A9893638A034777");

            entity.ToTable("TrafficViolation");

            entity.Property(e => e.ViolationId)
                .ValueGeneratedNever()
                .HasColumnName("violation_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DriverLicenseId).HasColumnName("driver_license_id");
            entity.Property(e => e.FineAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("fine_amount");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.LicensePlateId).HasColumnName("license_plate_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasColumnName("payment_status");
            entity.Property(e => e.ViolationType)
                .HasMaxLength(100)
                .HasColumnName("violation_type");

            entity.HasOne(d => d.DriverLicense).WithMany(p => p.TrafficViolations)
                .HasForeignKey(d => d.DriverLicenseId)
                .HasConstraintName("FK__TrafficVi__drive__4F7CD00D");

            entity.HasOne(d => d.LicensePlate).WithMany(p => p.TrafficViolations)
                .HasForeignKey(d => d.LicensePlateId)
                .HasConstraintName("FK__TrafficVi__licen__4E88ABD4");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__F2947BC19BCC30B0");

            entity.ToTable("Vehicle");

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("vehicle_id");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.CurrentOwnerId).HasColumnName("current_owner_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .HasColumnName("make");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.RegistrationOfficeId).HasColumnName("registration_office_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.CurrentOwner).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CurrentOwnerId)
                .HasConstraintName("FK__Vehicle__current__32E0915F");

            entity.HasOne(d => d.RegistrationOffice).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.RegistrationOfficeId)
                .HasConstraintName("FK__Vehicle__registr__31EC6D26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
