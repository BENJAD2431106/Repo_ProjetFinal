using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OnyraProjet.Models;

public partial class Donnee /*son vrai nom : DonneeCapteur */
{
    [Key]
    [Column("noDonnees")]
    public int NoDonnees { get; set; }

    [Column("valLumiere")]
    public double ValLumiere { get; set; }

    [Column("valSon")]
    public double ValSon { get; set; }

    [Column("dateHeure", TypeName = "datetime")]
    public DateTime DateHeure { get; set; }

    [Column("noUtilisateur")]
    public int NoUtilisateur { get; set; }

    [ForeignKey("NoUtilisateur")]
    [InverseProperty("Donnees")]
    public virtual Utilisateur NoUtilisateurNavigation { get; set; } = null!;
}
