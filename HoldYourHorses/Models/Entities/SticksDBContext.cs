﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HoldYourHorses.Models.Entities
{
    public partial class SticksDBContext : DbContext
    {
        public SticksDBContext()
        {
        }

        public SticksDBContext(DbContextOptions<SticksDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Kategorier> Kategoriers { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Stick> Sticks { get; set; } = null!;
        public virtual DbSet<Tillverkningsländer> Tillverkningsländers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Kategorier>(entity =>
            {
                entity.ToTable("Kategorier");

                entity.HasIndex(e => e.Namn, "UQ__Kategori__737584FD9C29E4A4")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.HasIndex(e => e.Id, "UQ__Material__3214EC067C94AEA9")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Stick>(entity =>
            {
                entity.HasKey(e => e.Artikelnr)
                    .HasName("PK__Sticks__CB7A9C8315D2DB06");

                entity.HasIndex(e => e.Artikelnamn, "UQ__Sticks__6A6FEA84587C7EA6")
                    .IsUnique();

                entity.Property(e => e.Artikelnamn).HasMaxLength(50);

                entity.Property(e => e.Beskrivning).HasMaxLength(1000);

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Kategori__4CA06362");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Material__4BAC3F29");

                entity.HasOne(d => d.Tillverkningsland)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.TillverkningslandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Tillverk__4D94879B");
            });

            modelBuilder.Entity<Tillverkningsländer>(entity =>
            {
                entity.ToTable("Tillverkningsländer");

                entity.HasIndex(e => e.Namn, "UQ__Tillverk__737584FD5DAEC99C")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
