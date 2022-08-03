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

        public virtual DbSet<Stick> Sticks { get; set; } = null!;
        public virtual DbSet<ShoppingCartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Finnish_Swedish_CI_AI");

            modelBuilder.Entity<Stick>(entity =>
            {
                entity.HasKey(e => e.Artikelnr)
                    .HasName("PK__Sticks__CB7A9C83119E417C");

                entity.HasIndex(e => e.Artikelnamn, "UQ__Sticks__6A6FEA8449436365")
                    .IsUnique();

                entity.Property(e => e.AbsBroms).HasColumnName("ABS broms");

                entity.Property(e => e.Artikelnamn).HasMaxLength(50);

                entity.Property(e => e.Beskrivning).HasMaxLength(1000);

                entity.Property(e => e.Material).HasMaxLength(50);

                entity.Property(e => e.Pris).HasColumnType("money");

                entity.Property(e => e.Tillverkningsland).HasMaxLength(50);

                entity.Property(e => e.Typ).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
