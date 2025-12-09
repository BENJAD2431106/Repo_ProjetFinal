using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Keyless]
public partial class MedecinUtilisateur
{
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("nomUtilisateur")]
    [StringLength(100)]
    public string NomUtilisateur { get; set; } = null!;

    [Column("prenomUtilisateur")]
    [StringLength(100)]
    public string PrenomUtilisateur { get; set; } = null!;

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

    [Column("medecinAttitre")]
    public int? MedecinAttitre { get; set; }

    [Column("noMedecin")]
    public int? NoMedecin { get; set; }

    [Column("prenomMedecin")]
    [StringLength(100)]
    public string? PrenomMedecin { get; set; }

    [Column("nomMedecin")]
    [StringLength(100)]
    public string? NomMedecin { get; set; }
}
