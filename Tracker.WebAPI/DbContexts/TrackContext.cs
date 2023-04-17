﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Tracker.WebAPI.Models;

namespace Tracker.WebAPI.DbContexts;

public partial class TrackContext : DbContext
{
    public TrackContext()
    {
    }

    public TrackContext(DbContextOptions<TrackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TrackLocation> TrackLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=Track;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TackLocation_id");

            entity.ToTable("TrackLocation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateEvent).HasColumnType("datetime");
            entity.Property(e => e.DateTrack)
                .HasColumnType("datetime")
                .HasColumnName("date_track");
            entity.Property(e => e.Imei)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMEI");
            entity.Property(e => e.Latitude)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasColumnType("decimal(12, 9)")
                .HasColumnName("longitude");
            entity.Property(e => e.TypeSource).HasDefaultValueSql("((1))");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
