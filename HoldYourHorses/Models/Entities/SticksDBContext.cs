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

        public virtual DbSet<Kategorier> Kategoriers { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Orderrader> Orderraders { get; set; } = null!;
        public virtual DbSet<Ordrar> Ordrars { get; set; } = null!;
        public virtual DbSet<Stick> Sticks { get; set; } = null!;
        public virtual DbSet<Tillverkningsländer> Tillverkningsländers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategorier>(entity =>
            {
                entity.ToTable("Kategorier");

                entity.HasIndex(e => e.Namn, "UQ__Kategori__737584FDC002A6F4")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.HasIndex(e => e.Id, "UQ__Material__3214EC06BA815724")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Orderrader>(entity =>
            {
                entity.ToTable("Orderrader");

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.HasOne(d => d.ArtikelNrNavigation)
                    .WithMany(p => p.Orderraders)
                    .HasForeignKey(d => d.ArtikelNr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderrade__Artik__5FB337D6");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderraders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orderrade__Order__5EBF139D");
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
            });

            modelBuilder.Entity<Stick>(entity =>
            {
                entity.HasKey(e => e.Artikelnr)
                    .HasName("PK__tmp_ms_x__CB7A9C83A8A8073B");

                entity.HasIndex(e => e.Artikelnamn, "UQ__tmp_ms_x__6A6FEA841723A3DA")
                    .IsUnique();

                entity.Property(e => e.Artikelnamn).HasMaxLength(50);

                entity.Property(e => e.Beskrivning).HasMaxLength(1000);

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.KategoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Kategori__5165187F");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Material__5070F446");

                entity.HasOne(d => d.Tillverkningsland)
                    .WithMany(p => p.Sticks)
                    .HasForeignKey(d => d.TillverkningslandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sticks__Tillverk__52593CB8");
            });

            modelBuilder.Entity<Tillverkningsländer>(entity =>
            {
                entity.ToTable("Tillverkningsländer");

                entity.HasIndex(e => e.Namn, "UQ__Tillverk__737584FDDC9601BF")
                    .IsUnique();

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
