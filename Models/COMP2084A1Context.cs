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

        public virtual DbSet<EcoScoreTally> EcoScoreTally { get; set; }
        public virtual DbSet<ItemInfo> ItemInfo { get; set; }

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
            modelBuilder.Entity<EcoScoreTally>(entity =>
            {
                entity.Property(e => e.Material).IsUnicode(false);

                entity.Property(e => e.Removal).IsUnicode(false);

                entity.Property(e => e.Reuse).IsUnicode(false);
            });

            modelBuilder.Entity<ItemInfo>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.UserName });

                entity.Property(e => e.ItemId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserName).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.EcoScore)
                    .WithMany(p => p.ItemInfo)
                    .HasForeignKey(d => d.EcoScoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemInfo_EcoScoreTally");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
