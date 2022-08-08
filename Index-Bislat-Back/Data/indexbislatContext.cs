using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace index_bislatContext
{
    public partial class indexbislatContext : DbContext
    {
        public indexbislatContext()
        {
        }

        public indexbislatContext(DbContextOptions<indexbislatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aifbase> Aifbases { get; set; } = null!;
        public virtual DbSet<Baseofcourse> Baseofcourses { get; set; } = null!;
        public virtual DbSet<Choisetable> Choisetables { get; set; } = null!;
        public virtual DbSet<Coursetable> Coursetables { get; set; } = null!;
        public virtual DbSet<Couseofsort> Couseofsorts { get; set; } = null!;
        public virtual DbSet<SortCycle> SortCycles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<Aifbase>(entity =>
            {
                entity.ToTable("aifbase");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.BaseName)
                    .HasMaxLength(45)
                    .HasColumnName("baseName");
            });

            modelBuilder.Entity<Baseofcourse>(entity =>
            {
                entity.ToTable("baseofcourse");

                entity.HasIndex(e => e.Baseid, "BASE_idx");

                entity.HasIndex(e => e.CourseId, "COURSEKEY_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Baseid)
                    .HasColumnType("int(11)")
                    .HasColumnName("BASEID");

                entity.Property(e => e.CourseId)
                    .HasColumnType("int(11)")
                    .HasColumnName("courseId");

                entity.HasOne(d => d.Base)
                    .WithMany(p => p.Baseofcourses)
                    .HasForeignKey(d => d.Baseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("BASEKEY");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Baseofcourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("COURSEKEY");
            });

            modelBuilder.Entity<Choisetable>(entity =>
            {
                entity.HasKey(e => e.Callid)
                    .HasName("PRIMARY");

                entity.ToTable("choisetable");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Sortid, "Sort_key_idx");

                entity.Property(e => e.Callid)
                    .HasColumnType("int(11)")
                    .HasColumnName("CALLID");

                entity.Property(e => e.First)
                    .HasMaxLength(45)
                    .HasColumnName("first")
                    .HasComment("עדיפות ראשונה");

                entity.Property(e => e.FullName)
                    .HasMaxLength(45)
                    .HasColumnName("fullName")
                    .HasComment("שם מלא של החייל")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Id)
                    .HasMaxLength(45)
                    .HasColumnName("ID")
                    .HasComment("ת.ז")
                    .UseCollation("latin1_swedish_ci")
                    .HasCharSet("latin1");

                entity.Property(e => e.Second)
                    .HasMaxLength(45)
                    .HasColumnName("second")
                    .HasComment("עדיפות שנייה");

                entity.Property(e => e.SortFrame)
                    .HasColumnType("int(11)")
                    .HasColumnName("sortFrame")
                    .HasComment("מסגרת מיון");

                entity.Property(e => e.Sortid)
                    .HasColumnType("int(11)")
                    .HasColumnName("SORTID");

                entity.Property(e => e.Third)
                    .HasMaxLength(45)
                    .HasColumnName("third")
                    .HasComment("עדיפות שלישית");

                entity.HasOne(d => d.Sort)
                    .WithMany(p => p.Choisetables)
                    .HasForeignKey(d => d.Sortid)
                    .HasConstraintName("Sort_key");
            });

            modelBuilder.Entity<Coursetable>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PRIMARY");

                entity.ToTable("coursetable");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.CourseId)
                    .HasColumnType("int(11)")
                    .HasColumnName("courseId");

                entity.Property(e => e.Category)
                    .HasMaxLength(45)
                    .HasColumnName("category");

                entity.Property(e => e.CourseDescription).HasMaxLength(1000);

                entity.Property(e => e.CourseName).HasMaxLength(45);

                entity.Property(e => e.CourseNumber).HasMaxLength(45);

                entity.Property(e => e.CourseTime).HasMaxLength(45);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(45)
                    .HasColumnName("ImgURL");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .HasColumnName("note");

                entity.Property(e => e.YouTubeUrl)
                    .HasMaxLength(45)
                    .HasColumnName("YouTubeURL");
            });

            modelBuilder.Entity<Couseofsort>(entity =>
            {
                entity.ToTable("couseofsort");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CourseId, "CourseKey_idx");

                entity.HasIndex(e => e.Sortid, "SortKey_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CourseId)
                    .HasColumnType("int(11)")
                    .HasColumnName("courseId");

                entity.Property(e => e.Sortid)
                    .HasColumnType("int(11)")
                    .HasColumnName("SORTID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Couseofsorts)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CourseSortKey");

                entity.HasOne(d => d.Sort)
                    .WithMany(p => p.Couseofsorts)
                    .HasForeignKey(d => d.Sortid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SortKey");
            });

            modelBuilder.Entity<SortCycle>(entity =>
            {
                entity.HasKey(e => e.Sortid)
                    .HasName("PRIMARY");

                entity.ToTable("sort cycles");

                entity.HasComment("מחזור מיון ")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Sortid)
                    .HasColumnType("int(11)")
                    .HasColumnName("SORTID");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasComment("שם מיון ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
