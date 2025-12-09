using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

[Keyless]
public partial class DonneesUtilisateur
{
    [Column("valLumiere")]
    public double ValLumiere { get; set; }

    [Column("valSon")]
    public double ValSon { get; set; }

    [Column("dateHeure", TypeName = "datetime")]
    public DateTime DateHeure { get; set; }

    [Column("nomUtilisateur")]
    [StringLength(100)]
    public string NomUtilisateur { get; set; } = null!;

    [Column("prenomUtilisateur")]
    [StringLength(100)]
    public string PrenomUtilisateur { get; set; } = null!;

    [Column("age")]
    public int Age { get; set; }
}
