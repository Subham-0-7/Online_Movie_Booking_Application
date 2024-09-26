using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class OnlineMovieBookingApplicationContext : DbContext
{
    public OnlineMovieBookingApplicationContext()
    {
    }

    public OnlineMovieBookingApplicationContext(DbContextOptions<OnlineMovieBookingApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=OnlineMovieBookingApplication;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AEDE5B9E649");

            entity.Property(e => e.ShowTime)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasComputedColumnSql("([NumberOfTickets]*[MoviePrice])", true);

            entity.HasOne(d => d.Movie).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Bookings_Movies");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Bookings_Users");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941A5B7CD5B0");

            entity.Property(e => e.Director)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MovieImage)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MovieName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Synopsis)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentD__9B556A38D220DE32");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Movie).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentDe__Movie__1699586C");

            entity.HasOne(d => d.User).WithMany(p => p.PaymentDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentDe__UserI__15A53433");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C33CE9936");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
