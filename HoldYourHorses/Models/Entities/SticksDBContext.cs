using System;
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
        public virtual DbSet<Stick> Sticks { get; set; } = null!;
        public virtual DbSet<Tillverkningsländer> Tillverkningsländers { get; set; } = null!;
        //public virtual DbSet<ShoppingCartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategorier>(entity =>
            {
                entity.ToTable("Kategorier");

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.ToTable("Material");

                entity.Property(e => e.Namn).HasMaxLength(50);
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

                entity.Property(e => e.Namn).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
