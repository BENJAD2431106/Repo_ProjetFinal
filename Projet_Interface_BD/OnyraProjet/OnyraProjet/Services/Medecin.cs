using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class Medecin
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory;

        public Medecin(IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory)
        {
            this.myFactory = myFactory;
        }
        public async Task<List<Utilisateur>> GetMedecinsDisponibles()
        {
            var db = await myFactory.CreateDbContextAsync();
            return await db.Utilisateurs
                .Where(u => u.Medecin == true)
                .ToListAsync();
        }

        public async Task AssignMedecin(int idUser, int idMedecin)
        {
            var db = await myFactory.CreateDbContextAsync();
            var user = await db.Utilisateurs.FindAsync(idUser);

            if (user != null)
            {
                user.MedecinAttitre = idMedecin;
                await db.SaveChangesAsync();
            }
        }
        public async Task EnregistrerMedecin(int idUtilisateur, int idMedecin, DateOnly dateRdv)
        {
            var db = await myFactory.CreateDbContextAsync();

            var utilisateur = await db.Utilisateurs
                .FirstOrDefaultAsync(u => u.NoUtilisateur == idUtilisateur);

            if (utilisateur != null)
            {
                utilisateur.MedecinAttitre = idMedecin;
                utilisateur.DateRdv = dateRdv;

                await db.SaveChangesAsync();
            }
        }
        public async Task<DateOnly?> ChargerRendezVous(int idUtilisateur)
        {
            var db = await myFactory.CreateDbContextAsync();

            var utilisateur = await db.Utilisateurs
                .FirstOrDefaultAsync(u => u.NoUtilisateur == idUtilisateur);

            if (utilisateur == null)
                return null;

            return utilisateur.DateRdv;
        }
        public async Task<(DateOnly? dateRdv, Utilisateur? medecin)> ChargerDossierMedecin(int idUtilisateur)
        {
            var db = await myFactory.CreateDbContextAsync();

            var utilisateur = await db.Utilisateurs
                .FirstOrDefaultAsync(u => u.NoUtilisateur == idUtilisateur);

            if (utilisateur == null)
                return (null, null);

            Utilisateur? medecin = null;

            if (utilisateur.MedecinAttitre != null)
            {
                medecin = await db.Utilisateurs
                    .FirstOrDefaultAsync(u => u.NoUtilisateur == utilisateur.MedecinAttitre);
            }

            return (utilisateur.DateRdv, medecin);
        }

        public async Task RetirerMedecin(int idUtilisateur)
        {
            var db = await myFactory.CreateDbContextAsync();

            var utilisateur = await db.Utilisateurs
                .FirstOrDefaultAsync(u => u.NoUtilisateur == idUtilisateur);

            if (utilisateur != null)
            {
                utilisateur.MedecinAttitre = null; 
                utilisateur.DateRdv = null; 

                await db.SaveChangesAsync();
            }
        }




    }
}
