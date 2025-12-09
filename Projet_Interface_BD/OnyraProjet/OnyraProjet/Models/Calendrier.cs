using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Table("Calendrier")]
public partial class Calendrier
{
    [Key]
    [Column("noCalendrier")]
    public int NoCalendrier { get; set; }

    [Column("heureCoucher", TypeName = "datetime")]
    public DateTime HeureCoucher { get; set; }

    [Column("heureLever", TypeName = "datetime")]
    public DateTime HeureLever { get; set; }

    [Column("dates")]
    public DateOnly Dates { get; set; }

    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("ressenti")]
    public int Ressenti { get; set; }

    [Column("nbreReveil")]
    public int? NbreReveil { get; set; }

    [Column("commentaire")]
    [StringLength(255)]
    public string? Commentaire { get; set; }

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("Calendriers")]
    public virtual Utilisateur NoUtilisateurNavigation { get; set; } = null!;
}
