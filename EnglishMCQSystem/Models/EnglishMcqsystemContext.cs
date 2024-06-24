﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07B0ED2E74");

            entity.Property(e => e.CorrectAnswer).HasMaxLength(255);
            entity.Property(e => e.Text).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC074CBCEA83");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tests__3214EC0786BB6FEB");

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
                        j.HasKey("TestId", "QuestionId").HasName("PK__TestQues__5C1F379AF4C0B262");
                        j.ToTable("TestQuestions");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FA582177");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4FDD4D5D2").IsUnique();

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
            entity.HasKey(e => e.Id).HasName("PK__UserTest__3214EC078ED3192F");

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
            entity.HasKey(e => e.Id).HasName("PK__UserTest__3214EC07A628D4DE");

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
