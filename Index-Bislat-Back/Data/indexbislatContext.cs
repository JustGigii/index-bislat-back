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

                entity.Property(e => e.Callid)
                    .HasColumnType("int(11)")
                    .HasColumnName("CALLID");

                entity.Property(e => e.First)
                    .HasColumnType("int(11)")
                    .HasColumnName("first")
                    .HasComment("עדיפות ראשונה");

                entity.Property(e => e.FullName)
                    .HasMaxLength(45)
                    .HasColumnName("fullName")
                    .HasComment("שם מלא של החייל");

                entity.Property(e => e.Id)
                    .HasMaxLength(45)
                    .HasColumnName("ID")
                    .HasComment("ת.ז");

                entity.Property(e => e.Second)
                    .HasColumnType("int(11)")
                    .HasColumnName("second")
                    .HasComment("עדיפות שנייה");

                entity.Property(e => e.SortFrame)
                    .HasColumnType("int(11)")
                    .HasColumnName("sortFrame")
                    .HasComment("מסגרת מיון");

                entity.Property(e => e.Third)
                    .HasColumnType("int(11)")
                    .HasColumnName("third")
                    .HasComment("עדיפות שלישית");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title")
                    .HasComment("מחזור מיון");
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
