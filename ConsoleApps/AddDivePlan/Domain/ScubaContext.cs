using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AddDivePlan.Domain;

public partial class ScubaContext : DbContext
{
    public ScubaContext()
    {
    }

    public ScubaContext(DbContextOptions<ScubaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgencyInstructor> AgencyInstructors { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<AttributeAssociation> AttributeAssociations { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Dife> Dives { get; set; }

    public virtual DbSet<DiveAgency> DiveAgencies { get; set; }

    public virtual DbSet<DiveLocation> DiveLocations { get; set; }

    public virtual DbSet<DivePlan> DivePlans { get; set; }

    public virtual DbSet<DiveShop> DiveShops { get; set; }

    public virtual DbSet<DiveSite> DiveSites { get; set; }

    public virtual DbSet<DiveSiteUrl> DiveSiteUrls { get; set; }

    public virtual DbSet<DiveTeam> DiveTeams { get; set; }

    public virtual DbSet<DiveType> DiveTypes { get; set; }

    public virtual DbSet<DiveUrl> DiveUrls { get; set; }

    public virtual DbSet<Diver> Divers { get; set; }

    public virtual DbSet<DiverCertification> DiverCertifications { get; set; }

    public virtual DbSet<DiverQualification> DiverQualifications { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<GasMix> GasMixes { get; set; }

    public virtual DbSet<Gase> Gases { get; set; }

    public virtual DbSet<Gear> Gears { get; set; }

    public virtual DbSet<GearServiceEvent> GearServiceEvents { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<InsurancePolicy> InsurancePolicies { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceSchedule> ServiceSchedules { get; set; }

    public virtual DbSet<SoldGear> SoldGears { get; set; }

    public virtual DbSet<Tank> Tanks { get; set; }

    public virtual DbSet<TanksOnDive> TanksOnDives { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VwCertification> VwCertifications { get; set; }

    public virtual DbSet<VwDiveShop> VwDiveShops { get; set; }

    public virtual DbSet<VwDiver> VwDivers { get; set; }

    public virtual DbSet<VwInstructor> VwInstructors { get; set; }

    public virtual DbSet<VwTanksOnDive> VwTanksOnDives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost\\mssqlserver01;Database=SCUBA;TrustServerCertificate=True;Trusted_Connection=True;");
        => optionsBuilder.UseSqlServer("server=sql2k1401.discountasp.net;Initial Catalog=SQL2014_754043_larryhack;User Id=SQL2014_754043_larryhack_user;password=nnihuee");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgencyInstructor>(entity =>
        {
            entity.HasKey(e => new { e.InstructorId, e.DiveAgencyId });

            entity.Property(e => e.InstructorAgencyId)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.DiveAgency).WithMany(p => p.AgencyInstructors)
                .HasForeignKey(d => d.DiveAgencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiveAgencies_AgencyInstructors");

            entity.HasOne(d => d.Instructor).WithMany(p => p.AgencyInstructors)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Instructors_AgencyInstructors");
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.TableToAssociate)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Attributes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Attributes");
        });

        modelBuilder.Entity<AttributeAssociation>(entity =>
        {
            entity.HasKey(e => new { e.AttributeId, e.TableRowId });

            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeAssociations)
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Attributes_AttributeAssociations");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.DiveAgency).WithMany(p => p.Certifications)
                .HasForeignKey(d => d.DiveAgencyId)
                .HasConstraintName("DiveAgencies_Certifications");

            entity.HasOne(d => d.User).WithMany(p => p.Certifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Certifications");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CellPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Company)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkPhone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.CountryCode)
                .HasConstraintName("Countries_Contacts");

            entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Contacts");

            entity.HasMany(d => d.DiveShopsNavigation).WithMany(p => p.Contacts)
                .UsingEntity<Dictionary<string, object>>(
                    "DiveShopStaff",
                    r => r.HasOne<DiveShop>().WithMany()
                        .HasForeignKey("DiveShopId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("DiveShops_DiveShopStaff"),
                    l => l.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Contacts_DiveShopStaff"),
                    j =>
                    {
                        j.HasKey("ContactId", "DiveShopId");
                        j.ToTable("DiveShopStaff");
                    });
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode);

            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dife>(entity =>
        {
            entity.HasKey(e => e.DiveId);

            entity.Property(e => e.AdditionalWeight).HasDefaultValueSql("((0))");
            entity.Property(e => e.AvgDepth).HasDefaultValueSql("((0))");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DescentTime).HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaxDepth).HasDefaultValueSql("((0))");
            entity.Property(e => e.Minutes).HasDefaultValueSql("((0))");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Temperature).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.DivePlan).WithMany(p => p.Dives)
                .HasForeignKey(d => d.DivePlanId)
                .HasConstraintName("Dives_DiveDetails");

            entity.HasOne(d => d.User).WithMany(p => p.Dives)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Users_Dives");
        });

        modelBuilder.Entity<DiveAgency>(entity =>
        {
            entity.Property(e => e.Notes).IsUnicode(false);

            entity.HasOne(d => d.Contact).WithMany(p => p.DiveAgencies)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_DiveAgencies");
        });

        modelBuilder.Entity<DiveLocation>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Contact).WithMany(p => p.DiveLocations)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("Contacts_DiveLocations");

            entity.HasOne(d => d.User).WithMany(p => p.DiveLocations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_DiveLocations");
        });

        modelBuilder.Entity<DivePlan>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("N"));

            entity.HasIndex(e => e.DivePlanId, "IDX_DivePlans_DiveId");

            entity.Property(e => e.DivePlanId).HasComment("N");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.DiveSiteId).HasComment("N");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.MaxDepth).HasDefaultValueSql("((0))");
            entity.Property(e => e.Notes)
                .IsUnicode(false)
                .HasComment("N");
            entity.Property(e => e.ScheduledTime)
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasComment("N");
            entity.Property(e => e.UserId).HasComment("N");

            entity.HasOne(d => d.DiveSite).WithMany(p => p.DivePlans)
                .HasForeignKey(d => d.DiveSiteId)
                .HasConstraintName("DiveSites_Dives");

            entity.HasOne(d => d.User).WithMany(p => p.DivePlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_DivePlans");
        });

        modelBuilder.Entity<DiveShop>(entity =>
        {
            entity.Property(e => e.Notes).IsUnicode(false);

            entity.HasOne(d => d.Contact).WithMany(p => p.DiveShops)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_DiveShops");

            entity.HasMany(d => d.Services).WithMany(p => p.DiveShops)
                .UsingEntity<Dictionary<string, object>>(
                    "DiveShopService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Services_DiveShopServices"),
                    l => l.HasOne<DiveShop>().WithMany()
                        .HasForeignKey("DiveShopId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("DiveShops_DiveShopServices"),
                    j =>
                    {
                        j.HasKey("DiveShopId", "ServiceId");
                        j.ToTable("DiveShopServices");
                    });
        });

        modelBuilder.Entity<DiveSite>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GeoCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.DiveLocation).WithMany(p => p.DiveSites)
                .HasForeignKey(d => d.DiveLocationId)
                .HasConstraintName("DiveLocations_DiveSites");

            entity.HasOne(d => d.User).WithMany(p => p.DiveSites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_DiveSites");
        });

        modelBuilder.Entity<DiveSiteUrl>(entity =>
        {
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.DiveSite).WithMany(p => p.DiveSiteUrls)
                .HasForeignKey(d => d.DiveSiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiveSites_DiveSiteUrls");
        });

        modelBuilder.Entity<DiveTeam>(entity =>
        {
            entity.HasKey(e => new { e.DivePlanId, e.DiverId }).HasName("PK_DiveTeam");

            entity.HasOne(d => d.DivePlan).WithMany(p => p.DiveTeams)
                .HasForeignKey(d => d.DivePlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Dives_DiveTeam");

            entity.HasOne(d => d.Diver).WithMany(p => p.DiveTeams)
                .HasForeignKey(d => d.DiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Divers_DiveTeam");

            entity.HasOne(d => d.Role).WithMany(p => p.DiveTeams)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Roles_DiveTeams");
        });

        modelBuilder.Entity<DiveType>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.DiveTypes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_DiveTypes");

            entity.HasMany(d => d.DivePlans).WithMany(p => p.DiveTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "DivePlansDiveType",
                    r => r.HasOne<DivePlan>().WithMany()
                        .HasForeignKey("DivePlanId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Dives_DiveTypesXref"),
                    l => l.HasOne<DiveType>().WithMany()
                        .HasForeignKey("DiveTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("DiveTypes_DiveTypesXref"),
                    j =>
                    {
                        j.HasKey("DiveTypeId", "DivePlanId");
                        j.ToTable("DivePlansDiveTypes");
                    });
        });

        modelBuilder.Entity<DiveUrl>(entity =>
        {
            entity.HasKey(e => e.DiveUrlId).HasName("PK_ContentLinks");

            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.Dive).WithMany(p => p.DiveUrls)
                .HasForeignKey(d => d.DiveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DiveDetails_ContentLinks");
        });

        modelBuilder.Entity<Diver>(entity =>
        {
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.RestingSacRate).HasDefaultValueSql("((0))");
            entity.Property(e => e.WorkingSacRate).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Contact).WithMany(p => p.Divers)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_Divers");

            entity.HasMany(d => d.Gears).WithMany(p => p.Divers)
                .UsingEntity<Dictionary<string, object>>(
                    "DiverGear",
                    r => r.HasOne<Gear>().WithMany()
                        .HasForeignKey("GearId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Gear_DiverGear"),
                    l => l.HasOne<Diver>().WithMany()
                        .HasForeignKey("DiverId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Divers_DiverGear"),
                    j =>
                    {
                        j.HasKey("DiverId", "GearId");
                        j.ToTable("DiverGear");
                    });
        });

        modelBuilder.Entity<DiverCertification>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("N"));

            entity.Property(e => e.DiverCertificationId).HasComment("N");
            entity.Property(e => e.CertificationId).HasComment("N");
            entity.Property(e => e.CertificationNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("N");
            entity.Property(e => e.Certified)
                .HasComment("N")
                .HasColumnType("date");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.DiverId).HasComment("N");
            entity.Property(e => e.InstructorId).HasComment("N");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .IsUnicode(false)
                .HasComment("N");

            entity.HasOne(d => d.Certification).WithMany(p => p.DiverCertifications)
                .HasForeignKey(d => d.CertificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Certifications_DiverCertifications");

            entity.HasOne(d => d.Diver).WithMany(p => p.DiverCertifications)
                .HasForeignKey(d => d.DiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Divers_DiverCertifications");

            entity.HasOne(d => d.Instructor).WithMany(p => p.DiverCertifications)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("Instructors_DiverCertifications");
        });

        modelBuilder.Entity<DiverQualification>(entity =>
        {
            entity.HasKey(e => new { e.DiverId, e.QualificationId });

            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Qualified).HasColumnType("date");

            entity.HasOne(d => d.Diver).WithMany(p => p.DiverQualifications)
                .HasForeignKey(d => d.DiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Divers_DiverQualifications");

            entity.HasOne(d => d.Qualification).WithMany(p => p.DiverQualifications)
                .HasForeignKey(d => d.QualificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Qualifications_DiverQualifications");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => new { e.RequestorUserId, e.RecipientUserId, e.DateRequested });

            entity.Property(e => e.DateRequested).HasColumnType("date");
            entity.Property(e => e.LastUpdated).HasColumnType("date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.RecipientUser).WithMany(p => p.FriendRecipientUsers)
                .HasForeignKey(d => d.RecipientUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Friends2");

            entity.HasOne(d => d.RequestorUser).WithMany(p => p.FriendRequestorUsers)
                .HasForeignKey(d => d.RequestorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Friends1");
        });

        modelBuilder.Entity<GasMix>(entity =>
        {
            entity.HasKey(e => new { e.DivePlanId, e.GearId, e.GasId }).HasName("PK__GasMixes__56FD4E221D9B5BB6");

            entity.ToTable(tb => tb.HasComment("N"));

            entity.Property(e => e.DivePlanId).HasComment("N");
            entity.Property(e => e.GearId).HasComment("N");
            entity.Property(e => e.GasId).HasComment("N");
            entity.Property(e => e.CostPerVolumeOfMeasure)
                .HasComment("N")
                .HasColumnType("money");
            entity.Property(e => e.Percentage).HasComment("N");
            entity.Property(e => e.VolumeAdded).HasComment("N");

            entity.HasOne(d => d.Gas).WithMany(p => p.GasMixes)
                .HasForeignKey(d => d.GasId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gases_GasMixes");

            entity.HasOne(d => d.TanksOnDive).WithMany(p => p.GasMixes)
                .HasForeignKey(d => new { d.DivePlanId, d.GearId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TanksOnDive_GasMixes");
        });

        modelBuilder.Entity<Gase>(entity =>
        {
            entity.HasKey(e => e.GasId);

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Gear>(entity =>
        {
            entity.ToTable("Gear");

            entity.Property(e => e.Acquired).HasColumnType("date");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoLongerUse).HasColumnType("date");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Paid)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.RetailPrice)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Sn)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SN");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Weight).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Gears)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("Manufacturers_Gear");

            entity.HasOne(d => d.PurchasedFromContact).WithMany(p => p.Gears)
                .HasForeignKey(d => d.PurchasedFromContactId)
                .HasConstraintName("Contacts_Gear");

            entity.HasOne(d => d.User).WithMany(p => p.Gears)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Gear");

            entity.HasMany(d => d.DivePlans).WithMany(p => p.Gears)
                .UsingEntity<Dictionary<string, object>>(
                    "GearOnDive",
                    r => r.HasOne<DivePlan>().WithMany()
                        .HasForeignKey("DivePlanId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("DivePlans_GearOnDive"),
                    l => l.HasOne<Gear>().WithMany()
                        .HasForeignKey("GearId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Gear_GearOnDive"),
                    j =>
                    {
                        j.HasKey("GearId", "DivePlanId");
                        j.ToTable("GearOnDive");
                    });

            entity.HasMany(d => d.InsurancePolicies).WithMany(p => p.Gears)
                .UsingEntity<Dictionary<string, object>>(
                    "InsuredGear",
                    r => r.HasOne<InsurancePolicy>().WithMany()
                        .HasForeignKey("InsurancePolicyId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("InsurancePolicies_InsuredGear"),
                    l => l.HasOne<Gear>().WithMany()
                        .HasForeignKey("GearId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Gear_InsuredGear"),
                    j =>
                    {
                        j.HasKey("GearId", "InsurancePolicyId");
                        j.ToTable("InsuredGear");
                    });
        });

        modelBuilder.Entity<GearServiceEvent>(entity =>
        {
            entity.HasKey(e => e.GearServiceEventsId);

            entity.Property(e => e.GearServiceEventsId).ValueGeneratedNever();
            entity.Property(e => e.Cost)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.ServicedDate).HasColumnType("date");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Gear).WithMany(p => p.GearServiceEvents)
                .HasForeignKey(d => d.GearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gear_GearServiceEvents");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.ToTable(tb => tb.HasComment("N"));

            entity.Property(e => e.InstructorId).HasComment("N");
            entity.Property(e => e.ContactId).HasComment("N");
            entity.Property(e => e.Notes)
                .IsUnicode(false)
                .HasComment("N");

            entity.HasOne(d => d.Contact).WithMany(p => p.Instructors)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_Instructors");
        });

        modelBuilder.Entity<InsurancePolicy>(entity =>
        {
            entity.Property(e => e.Deductible)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.EndOfTerm).HasColumnType("date");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.StartOfTerm).HasColumnType("date");
            entity.Property(e => e.ValueAmount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");

            entity.HasOne(d => d.Contact).WithMany(p => p.InsurancePolicies)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_InsurancePolicies");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasOne(d => d.Contact).WithMany(p => p.Manufacturers)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contacts_Manufactures");
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Qualifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Qualifications");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_Roles");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ServiceSchedule>(entity =>
        {
            entity.Property(e => e.NextServiceDate).HasColumnType("date");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.Gear).WithMany(p => p.ServiceSchedules)
                .HasForeignKey(d => d.GearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gear_ServiceSchedules");
        });

        modelBuilder.Entity<SoldGear>(entity =>
        {
            entity.HasKey(e => e.GearId);

            entity.ToTable("SoldGear");

            entity.Property(e => e.GearId).ValueGeneratedNever();
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.SoldOn).HasColumnType("date");

            entity.HasOne(d => d.Gear).WithOne(p => p.SoldGear)
                .HasForeignKey<SoldGear>(d => d.GearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gear_SoldGear");

            entity.HasOne(d => d.SoldToContact).WithMany(p => p.SoldGears)
                .HasForeignKey(d => d.SoldToContactId)
                .HasConstraintName("Contacts_SoldGear");
        });

        modelBuilder.Entity<Tank>(entity =>
        {
            entity.HasKey(e => e.GearId);

            entity.Property(e => e.GearId).ValueGeneratedNever();
            entity.Property(e => e.Volume).HasDefaultValueSql("((0))");
            entity.Property(e => e.WorkingPressure).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Gear).WithOne(p => p.Tank)
                .HasForeignKey<Tank>(d => d.GearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gear_Tanks");
        });

        modelBuilder.Entity<TanksOnDive>(entity =>
        {
            entity.HasKey(e => new { e.DivePlanId, e.GearId });

            entity.ToTable("TanksOnDive", tb => tb.HasComment("N"));

            entity.Property(e => e.DivePlanId).HasComment("N");
            entity.Property(e => e.GearId).HasComment("N");
            entity.Property(e => e.EndingPressure)
                .HasDefaultValueSql("((0))")
                .HasComment("N");
            entity.Property(e => e.FillCost)
                .HasComment("N")
                .HasColumnType("money");
            entity.Property(e => e.FillDate)
                .HasComment("N")
                .HasColumnType("datetime");
            entity.Property(e => e.GasContentTitle)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("N");
            entity.Property(e => e.StartingPressure)
                .HasDefaultValueSql("((0))")
                .HasComment("N");

            entity.HasOne(d => d.DivePlan).WithMany(p => p.TanksOnDives)
                .HasForeignKey(d => d.DivePlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DivePlans_TanksOnDive");

            entity.HasOne(d => d.Gear).WithMany(p => p.TanksOnDives)
                .HasForeignKey(d => d.GearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tanks_TanksOnDive");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.LoginId, "TUC_Users_1").IsUnique();

            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LastModified)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.LoginCount).HasDefaultValueSql("((0))");
            entity.Property(e => e.LoginId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Contact).WithMany(p => p.Users)
                .HasForeignKey(d => d.ContactId)
                .HasConstraintName("Contacts_Users");
        });

        modelBuilder.Entity<VwCertification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwCertifications");

            entity.Property(e => e.Agency)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CertificationNum)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Certified).HasColumnType("date");
            entity.Property(e => e.DiverFirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiverLastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.InstructorFirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InstructorLastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDiveShop>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwDiveShops");

            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CellPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Company)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShopNotes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDiver>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwDivers");

            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.CellPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Company)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.DiverNotes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HomePhone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwInstructor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwInstructors");

            entity.Property(e => e.Agency)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InstructorAgencyId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwTanksOnDive>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwTanksOnDive");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
