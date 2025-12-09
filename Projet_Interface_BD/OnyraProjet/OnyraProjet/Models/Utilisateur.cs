using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Index("Courriel", Name = "Unique_Courriel", IsUnique = true)]
public partial class Utilisateur
{
    [Key]
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("nomUtilisateur")]
    [StringLength(100)]
    public string NomUtilisateur { get; set; } = null!;

    [Column("prenomUtilisateur")]
    [StringLength(100)]
    public string PrenomUtilisateur { get; set; } = null!;

    [Column("courriel")]
    [StringLength(255)]
    public string Courriel { get; set; } = null!;

    [Column("medecin")]
    public bool Medecin { get; set; }

    [Column("medecinAttitre")]
    public int? MedecinAttitre { get; set; }

    [Column("age")]
    public int Age { get; set; }

    [Column("photo")]
    public byte[]? Photo { get; set; }

    [Column("dateRdv")]
    public DateOnly? DateRdv { get; set; }

    [Column("ramQ")]
    [StringLength(12)]
    [Unicode(false)]
    public string? RamQ { get; set; }

    [Column("config")]
    public bool? Config { get; set; }

    [Column("sel")]
    public Guid? Sel { get; set; }

    [Column("motDePasse")]
    [MaxLength(64)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public byte[] MotDePasse { get; set; } = null!;

    [InverseProperty("NoUtilisateurNavigation")]
    public virtual ICollection<Calendrier> Calendriers { get; set; } = new List<Calendrier>();

    [InverseProperty("NoUtilisateurNavigation")]
    public virtual ICollection<Donnee> Donnees { get; set; } = new List<Donnee>();

    [InverseProperty("MedecinAttitreNavigation")]
    public virtual ICollection<Utilisateur> InverseMedecinAttitreNavigation { get; set; } = new List<Utilisateur>();

    [ForeignKey("MedecinAttitre")]
    [InverseProperty("InverseMedecinAttitreNavigation")]
    public virtual Utilisateur? MedecinAttitreNavigation { get; set; }
}
