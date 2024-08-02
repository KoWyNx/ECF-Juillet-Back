using System;
using System.Collections.Generic;
using ECF.Models;
using Microsoft.EntityFrameworkCore;

namespace ECF.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PlayerScore> PlayerScores { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerScore>(entity =>
        {
            entity.HasKey(e => e.ScoreId).HasName("PK__PlayerSc__7DD229D12B5F343F");

            entity.Property(e => e.DateRecorded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PlayerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC9BEA00A0");

            entity.Property(e => e.CorrectAnswer).HasMaxLength(100);
            entity.Property(e => e.Text).HasMaxLength(500);
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.OptionId).HasName("PK__Question__92C7A1FF05FB56DB");

            entity.Property(e => e.OptionText).HasMaxLength(100);

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QuestionO__Quest__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
