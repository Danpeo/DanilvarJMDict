using System;
using System.Collections.Generic;
using Danilvar.JMDict.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Danilvar.JMDict.Api.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gloss> Glosses { get; set; }

    public virtual DbSet<JmdictData> JmdictDatas { get; set; }

    public virtual DbSet<Kana> Kanas { get; set; }

    public virtual DbSet<Kanji> Kanjis { get; set; }

    public virtual DbSet<Sense> Senses { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("ru_RU.utf8")
            .HasPostgresExtension("hstore");

        modelBuilder.Entity<Gloss>(entity =>
        {
            entity.HasIndex(e => e.SenseId, "IX_Glosses_SenseId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            //entity.HasOne(d => d.Sense).WithMany(p => p.Glosses).HasForeignKey(d => d.SenseId);
        });

        modelBuilder.Entity<JmdictData>(entity =>
        {
            entity.ToTable("JMDictDatas");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Kana>(entity =>
        {
            entity.ToTable("Kana");

            entity.HasIndex(e => e.WordId, "IX_Kana_WordId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            //entity.HasOne(d => d.Word).WithMany(p => p.Kanas).HasForeignKey(d => d.WordId);
        });

        modelBuilder.Entity<Kanji>(entity =>
        {
            entity.ToTable("Kanji");

            entity.HasIndex(e => e.WordId, "IX_Kanji_WordId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            //entity.HasOne(d => d.Word).WithMany(p => p.Kanjis).HasForeignKey(d => d.WordId);
        });

        modelBuilder.Entity<Sense>(entity =>
        {
            entity.HasIndex(e => e.WordId, "IX_Senses_WordId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            //entity.HasOne(d => d.Word).WithMany(p => p.Senses).HasForeignKey(d => d.WordId);
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasIndex(e => e.JmdictDataId, "IX_Words_JMDictDataId");

            entity.Property(e => e.JmdictDataId).HasColumnName("JMDictDataId");

            entity.HasOne(d => d.JmdictData).WithMany(p => p.Words).HasForeignKey(d => d.JmdictDataId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
