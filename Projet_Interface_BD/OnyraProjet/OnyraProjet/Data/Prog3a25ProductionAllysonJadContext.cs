using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Models;

namespace OnyraProjet.Data;

public partial class Prog3a25ProductionAllysonJadContext : DbContext
{
    public Prog3a25ProductionAllysonJadContext()
    {
    }

    public Prog3a25ProductionAllysonJadContext(DbContextOptions<Prog3a25ProductionAllysonJadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calendrier> Calendriers { get; set; } 

    public virtual DbSet<CalendrierUtilisateur> CalendrierUtilisateurs { get; set; }

    public virtual DbSet<Donnee> Donnees { get; set; }

    public virtual DbSet<Donnees> DonneesCalendriers { get; set; }

    public virtual DbSet<DonneesUtilisateur> DonneesUtilisateurs { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calendrier>(entity =>
        {
            entity.HasKey(e => e.NoCalendrier).HasName("PK__Calendri__CCC567B214529EE0");

            entity.HasOne(d => d.NoUtilisateurNavigation).WithMany(p => p.Calendriers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Calendrier");
        });

        modelBuilder.Entity<CalendrierUtilisateur>(entity =>
        {
            entity.ToView("calendrierUtilisateur");
        });

        modelBuilder.Entity<Donnee>(entity =>
        {
            entity.HasKey(e => e.NoDonnees).HasName("PK__Donnees__E486EBFAACD5A18E");

            entity.HasOne(d => d.NoUtilisateurNavigation).WithMany(p => p.Donnees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donnees");
        });

        modelBuilder.Entity<Donnees>(entity =>
        {
            entity.ToView("DonneesCalendrier");
        });

        modelBuilder.Entity<DonneesUtilisateur>(entity =>
        {
            entity.ToView("DonneesUtilisateur");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.NoUtilisateur).HasName("PK__Utilisat__CB66E30B166703CC");

            entity.Property(e => e.Config).HasDefaultValue(true);
            entity.Property(e => e.MotDePasse).IsFixedLength();
            entity.ToTable(tb => tb.HasTrigger("trig_Default_Medecin"));

            entity.HasOne(d => d.MedecinAttitreNavigation).WithMany(p => p.InverseMedecinAttitreNavigation).HasConstraintName("FK_Utilisateurs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
