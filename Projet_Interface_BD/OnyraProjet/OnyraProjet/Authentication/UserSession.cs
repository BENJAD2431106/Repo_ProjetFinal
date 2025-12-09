using Microsoft.EntityFrameworkCore;
using OnyraProjet.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnyraProjet.Authentication
{
    public class UserSession : Utilisateur
    {
        public string Role { get; set; }

        //public UserSession(Utilisateur u, string role)
        //{
        //    Role = role;
        //    NoUtilisateur = u.NoUtilisateur;
        //    NomUtilisateur = u.NomUtilisateur;
        //    PrenomUtilisateur = u.PrenomUtilisateur;
        //    Courriel = u.Courriel;
        //    Medecin = u.Medecin;
        //    MedecinAttitre = u.MedecinAttitre;
        //    Age = u.Age;
        //    Photo = u.Photo;
        //    DateRdv = u.DateRdv;
        //    RamQ = u.RamQ;
        //    Config = u.Config;
        //    Sel = u.Sel;
        //    MotDePasse = u.MotDePasse;
        //}
    }
}
