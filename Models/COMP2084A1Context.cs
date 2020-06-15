using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace COMP2084___A1.Models
{
    public partial class COMP2084A1Context : DbContext
    {
        public COMP2084A1Context()
        {
        }

        public COMP2084A1Context(DbContextOptions<COMP2084A1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductIn> ProductIn { get; set; }
        public virtual DbSet<UserIn> UserIn { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=WKIRSCHN\\SQLSERVER2019;Initial Catalog=COMP2084A1;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductIn>(entity =>
            {
                entity.Property(e => e.ItemId).ValueGeneratedNever();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EcoScore).ValueGeneratedOnAdd();

                entity.Property(e => e.Material).IsUnicode(false);

                entity.Property(e => e.Photo).IsUnicode(false);
            });

            modelBuilder.Entity<UserIn>(entity =>
            {
                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.UserIn)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserIn_ProductIn");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
