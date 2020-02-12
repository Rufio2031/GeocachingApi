using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GeocachingApi.Domain.DataAccess.Geocaching
{
    public partial class geocachingContext : DbContext
    {
        public geocachingContext()
        {
        }

        public geocachingContext(DbContextOptions<geocachingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Geocache> Geocache { get; set; }
        public virtual DbSet<GeocacheItem> GeocacheItem { get; set; }
        public virtual DbSet<GeocacheLocation> GeocacheLocation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=geocaching.ccasgfquunfb.us-east-1.rds.amazonaws.com;Database=geocaching;User Id=geoadmin;Password=K3mkyekfhUA3IEk3kKz6;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Geocache>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Geocache)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_LocationId_Location");
            });

            modelBuilder.Entity<GeocacheItem>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Geocache__737584F63FA7F711")
                    .IsUnique();

                entity.Property(e => e.ActiveEndDate).HasColumnType("datetime");

                entity.Property(e => e.ActiveStartDate).HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Geocache)
                    .WithMany(p => p.GeocacheItem)
                    .HasForeignKey(d => d.GeocacheId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GeocacheId_Geocache");
            });

            modelBuilder.Entity<GeocacheLocation>(entity =>
            {
                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
