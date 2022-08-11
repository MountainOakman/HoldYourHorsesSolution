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
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<Kategorier> Kategoriers { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Orderrader> Orderraders { get; set; } = null!;
        public virtual DbSet<Ordrar> Ordrars { get; set; } = null!;
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

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.Property(e => e.User).HasMaxLength(450);

                entity.HasOne(d => d.ArtikelnrNavigation)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.Artikelnr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Favourite__Artik__48CFD27E");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__Favourites__User__47DBAE45");
            });

            modelBuilder.Entity<Kategorier>(entity =>
            {
                entity.ToTable("Kategorier");

                entity.HasIndex(e => e.Namn, "UQ__Kategori__737584FD93780F24")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.HasIndex(e => e.Id, "UQ__Material__3214EC06C2A2A4EF")
                    .IsUnique();

                entity.HasIndex(e => e.Namn, "UQ__Material__737584FD8B88BC1C")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Orderrader>(entity =>
            {
                entity.ToTable("Orderrader");

                entity.Property(e => e.ArtikelNamn).HasMaxLength(50);

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.HasOne(d => d.ArtikelNamnNavigation)
                    .WithMany(p => p.OrderraderArtikelNamnNavigations)
                    .HasPrincipalKey(p => p.Artikelnamn)
                    .HasForeignKey(d => d.ArtikelNamn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderrade__Artik__4BAC3F29");

                entity.HasOne(d => d.ArtikelNrNavigation)
                    .WithMany(p => p.OrderraderArtikelNrNavigations)
                    .HasForeignKey(d => d.ArtikelNr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderrade__Artik__4AB81AF0");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderraders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderrade__Order__49C3F6B7");
            });

            modelBuilder.Entity<Ordrar>(entity =>
            {
                entity.ToTable("Ordrar");

                entity.Property(e => e.Adress).HasMaxLength(50);

                entity.Property(e => e.Efternamn).HasMaxLength(50);

                entity.Property(e => e.Epost).HasMaxLength(50);

                entity.Property(e => e.Förnamn).HasMaxLength(50);

                entity.Property(e => e.Land).HasMaxLength(50);

                entity.Property(e => e.Stad).HasMaxLength(50);

                entity.Property(e => e.User).HasMaxLength(450);

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Ordrars)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("FK__Ordrar__User__4CA06362");
            });

            modelBuilder.Entity<Stick>(entity =>
            {
                entity.HasKey(e => e.Artikelnr)
                    .HasName("PK__Sticks__CB7A9C838541894C");

                entity.HasIndex(e => e.Artikelnamn, "UQ__Sticks__6A6FEA84D0D31373")
                    .IsUnique();

                entity.Property(e => e.Artikelnamn).HasMaxLength(50);

                entity.Property(e => e.Beskrivning).HasMaxLength(1000);

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Kategori__4E88ABD4");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Material__4D94879B");

                entity.HasOne(d => d.Tillverkningsland)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.TillverkningslandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Tillverk__4F7CD00D");
            });

            modelBuilder.Entity<Tillverkningsländer>(entity =>
            {
                entity.ToTable("Tillverkningsländer");

                entity.HasIndex(e => e.Namn, "UQ__Tillverk__737584FD5635248C")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
