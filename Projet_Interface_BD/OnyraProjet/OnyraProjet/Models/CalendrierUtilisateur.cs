using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Keyless]
public partial class CalendrierUtilisateur
{
    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [Column("heureCoucher", TypeName = "datetime")]
    public DateTime HeureCoucher { get; set; }

    [Column("heureLever", TypeName = "datetime")]
    public DateTime HeureLever { get; set; }

    [Column("dates")]
    public DateOnly Dates { get; set; }

    [Column("ressenti")]
    public int Ressenti { get; set; }

    [Column("nbreReveil")]
    public int? NbreReveil { get; set; }

    [Column("nomUtilisateur")]
    [StringLength(100)]
    public string NomUtilisateur { get; set; } = null!;

    [Column("prenomUtilisateur")]
    [StringLength(100)]
    public string PrenomUtilisateur { get; set; } = null!;

    [Column("age")]
    public int Age { get; set; }
}
