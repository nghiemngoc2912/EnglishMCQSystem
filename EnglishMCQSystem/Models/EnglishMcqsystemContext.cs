using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnglishMCQSystem.Models;

public partial class EnglishMcqsystemContext : DbContext
{
    public EnglishMcqsystemContext()
    {
    }

    public EnglishMcqsystemContext(DbContextOptions<EnglishMcqsystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserTest> UserTests { get; set; }

    public virtual DbSet<UserTestAnswer> UserTestAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=WOLFGIRL2912;database=EnglishMCQSystem;uid=sa;pwd=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07F01B0E6F");

            entity.Property(e => e.CorrectAnswer).HasMaxLength(255);
            entity.Property(e => e.Text).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07F1C87D3C");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tests__3214EC0754B9D67C");

            entity.Property(e => e.DifficultyLevel)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Questions).WithMany(p => p.Tests)
                .UsingEntity<Dictionary<string, object>>(
                    "TestQuestion",
                    r => r.HasOne<Question>().WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TestQuest__Quest__2F10007B"),
                    l => l.HasOne<Test>().WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TestQuest__TestI__2E1BDC42"),
                    j =>
                    {
                        j.HasKey("TestId", "QuestionId").HasName("PK__TestQues__5C1F379A322627B3");
                        j.ToTable("TestQuestions");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D6DA2E6F");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4A6B8B6C0").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__276EDEB3");
        });

        modelBuilder.Entity<UserTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTest__3214EC07E15D0239");

            entity.Property(e => e.TestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Test).WithMany(p => p.UserTests)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__UserTests__TestI__33D4B598");

            entity.HasOne(d => d.User).WithMany(p => p.UserTests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserTests__UserI__32E0915F");
        });

        modelBuilder.Entity<UserTestAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserTest__3214EC07A138BD5F");

            entity.Property(e => e.UserAnswer).HasMaxLength(255);

            entity.HasOne(d => d.Question).WithMany(p => p.UserTestAnswers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__UserTestA__Quest__37A5467C");

            entity.HasOne(d => d.UserTest).WithMany(p => p.UserTestAnswers)
                .HasForeignKey(d => d.UserTestId)
                .HasConstraintName("FK__UserTestA__UserT__36B12243");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
