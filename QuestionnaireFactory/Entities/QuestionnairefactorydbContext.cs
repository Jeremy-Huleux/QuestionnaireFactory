using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuestionnaireFactory.Models.PassageQuizz;

namespace QuestionnaireFactory.Entities;

public partial class QuestionnairefactorydbContext : DbContext
{
    public QuestionnairefactorydbContext()
    {
    }

    public QuestionnairefactorydbContext(DbContextOptions<QuestionnairefactorydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidat> Candidats { get; set; }

    public virtual DbSet<NiveauQuestion> NiveauQuestions { get; set; }

    public virtual DbSet<NiveauQuizz> NiveauQuizzs { get; set; }

    public virtual DbSet<NiveauQuizzNiveauQuestion> NiveauQuizzNiveauQuestions { get; set; }

    public virtual DbSet<OptionQuestion> OptionQuestions { get; set; }

    public virtual DbSet<QuestionEnregistree> QuestionEnregistrees { get; set; }

    public virtual DbSet<Quizz> Quizzs { get; set; }

    public virtual DbSet<QuizzQuestionEnregistree> QuizzQuestionEnregistrees { get; set; }

    public virtual DbSet<QuizzReponsePossible> QuizzReponsePossibles { get; set; }

    public virtual DbSet<ReponsePossible> ReponsePossibles { get; set; }

    public virtual DbSet<Technologie> Technologies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DB_CONNECTION_STRING");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidat>(entity =>
        {
            entity.HasKey(e => e.CandidatId).HasName("PK__Candidat__322722EA703BFE6E");

            entity.ToTable("Candidat");

            entity.Property(e => e.AgentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NiveauQuestion>(entity =>
        {
            entity.HasKey(e => e.NiveauQuestionId).HasName("PK__NiveauQu__1D9A401A7AE6CCF8");

            entity.ToTable("NiveauQuestion");

            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NiveauQuizz>(entity =>
        {
            entity.HasKey(e => e.NiveauQuizzId).HasName("PK__NiveauQu__D792AD402D9F4A1A");

            entity.ToTable("NiveauQuizz");

            entity.Property(e => e.Libelle)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<NiveauQuizzNiveauQuestion>(entity =>
        {
            entity.HasKey(e => new { e.NiveauQuizzId, e.NiveauQuestionId }).HasName("PK__NiveauQu__664B09418906A9FE");

            entity.ToTable("NiveauQuizzNiveauQuestion");

            entity.HasOne(d => d.NiveauQuestion).WithMany(p => p.NiveauQuizzNiveauQuestions)
                .HasForeignKey(d => d.NiveauQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NiveauQui__Nivea__4AB81AF0");

            entity.HasOne(d => d.NiveauQuizz).WithMany(p => p.NiveauQuizzNiveauQuestions)
                .HasForeignKey(d => d.NiveauQuizzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NiveauQui__Nivea__4BAC3F29");
        });

        modelBuilder.Entity<OptionQuestion>(entity =>
        {
            entity.HasKey(e => e.OptionQuestionId).HasName("PK__OptionQu__63A1E583CAEA2872");

            entity.ToTable("OptionQuestion");

            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<QuestionEnregistree>(entity =>
        {
            entity.HasKey(e => e.QuestionEnregistreeId).HasName("PK__Question__749063AD8F3AFA22");

            entity.ToTable("QuestionEnregistree");

            entity.Property(e => e.ContenuQuestion).IsUnicode(false);
            entity.Property(e => e.Explication).IsUnicode(false);

            entity.HasOne(d => d.NiveauQuestion).WithMany(p => p.QuestionEnregistrees)
                .HasForeignKey(d => d.NiveauQuestionId)
                .HasConstraintName("FK__QuestionE__Nivea__4CA06362");

            entity.HasOne(d => d.OptionQuestion).WithMany(p => p.QuestionEnregistrees)
                .HasForeignKey(d => d.OptionQuestionId)
                .HasConstraintName("FK__QuestionE__Optio__4D94879B");

            entity.HasOne(d => d.Technologie).WithMany(p => p.QuestionEnregistrees)
                .HasForeignKey(d => d.TechnologieId)
                .HasConstraintName("FK__QuestionE__Techn__4E88ABD4");
        });

        modelBuilder.Entity<Quizz>(entity =>
        {
            entity.HasKey(e => e.QuizzId).HasName("PK__Quizz__2F101A8DE7FFC166");

            entity.ToTable("Quizz");

            entity.Property(e => e.AgentId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodeUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NomQuizz)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Candidat).WithMany(p => p.Quizzs)
                .HasForeignKey(d => d.CandidatId)
                .HasConstraintName("FK__Quizz__CandidatI__4F7CD00D");

            entity.HasOne(d => d.NiveauQuizz).WithMany(p => p.Quizzs)
                .HasForeignKey(d => d.NiveauQuizzId)
                .HasConstraintName("FK__Quizz__NiveauQui__5070F446");
        });

        modelBuilder.Entity<QuizzQuestionEnregistree>(entity =>
        {
            entity.HasKey(e => new { e.QuizzId, e.QuestionEnregistreeId }).HasName("PK__QuizzQue__E8591CB76AC85397");

            entity.ToTable("QuizzQuestionEnregistree");

            entity.Property(e => e.ReponseCandidatLibre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.QuestionEnregistree).WithMany(p => p.QuizzQuestionEnregistrees)
                .HasForeignKey(d => d.QuestionEnregistreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizzQues__Quest__5165187F");

            entity.HasOne(d => d.Quizz).WithMany(p => p.QuizzQuestionEnregistrees)
                .HasForeignKey(d => d.QuizzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizzQues__Quizz__52593CB8");
        });

        modelBuilder.Entity<QuizzReponsePossible>(entity =>
        {
            entity.HasKey(e => new { e.QuizzId, e.ReponsePossibleId }).HasName("PK__QuizzRep__4182EF979837EBB9");

            entity.ToTable("QuizzReponsePossible");

            entity.Property(e => e.Commentaire)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Quizz).WithMany(p => p.QuizzReponsePossibles)
                .HasForeignKey(d => d.QuizzId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizzRepo__Quizz__534D60F1");

            entity.HasOne(d => d.ReponsePossible).WithMany(p => p.QuizzReponsePossibles)
                .HasForeignKey(d => d.ReponsePossibleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuizzRepo__Repon__5441852A");
        });

        modelBuilder.Entity<ReponsePossible>(entity =>
        {
            entity.HasKey(e => e.ReponsePossibleId).HasName("PK__ReponseP__E92F51A765920280");

            entity.ToTable("ReponsePossible");

            entity.Property(e => e.ReponsePossible1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ReponsePossible");

            entity.HasOne(d => d.QuestionEnregistree).WithMany(p => p.ReponsePossibles)
                .HasForeignKey(d => d.QuestionEnregistreeId)
                .HasConstraintName("FK__ReponsePo__Quest__5535A963");
        });

        modelBuilder.Entity<Technologie>(entity =>
        {
            entity.HasKey(e => e.TechnologieId).HasName("PK__Technolo__93212F4BFD4365ED");

            entity.ToTable("Technologie");

            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<QuestionnaireFactory.Models.PassageQuizz.QuestionViewModel> QuestionViewModel { get; set; } = default!;

//public DbSet<QuestionnaireFactory.Models.PassageQuizz.PassageQuizzViewModel> PassageQuizzViewModel { get; set; } = default!;
}
