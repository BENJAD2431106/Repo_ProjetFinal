using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class ProfilService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _contextFactory;

        public ProfilService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// Cherche un utilisateur par son numéro dans la base de données et, si elle le trouve, elle renvoie son profi, sinon, elle renvoie null.
        /// </summary>
        /// <param name="noUtilisateur"></param>
        /// <returns></returns>
        public async Task<ProfilModel?> GetProfilAsync(int noUtilisateur)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = await context.Utilisateurs
                                    .FirstOrDefaultAsync(u => u.NoUtilisateur == noUtilisateur);

            if (user == null)
                return null;

            return new ProfilModel
            {
                NomUtilisateur = user.NomUtilisateur,
                PrenomUtilisateur = user.PrenomUtilisateur,
                Courriel = user.Courriel,
                Age = user.Age,
                RamQ = user.RamQ,
                Photo = user.Photo
            };
        }

        /// <summary>
        /// Met à jour le profil d’un utilisateur dans la base de données et renvoie true si tout s’est bien passé.
        /// </summary>
        /// <param name="noUtilisateur"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProfilAsync(int noUtilisateur, ProfilModel model)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var user = await context.Utilisateurs
                                    .FirstOrDefaultAsync(u => u.NoUtilisateur == noUtilisateur);

            if (user == null)
                return false;

            user.NomUtilisateur = model.NomUtilisateur;
            user.PrenomUtilisateur = model.PrenomUtilisateur;
            user.Courriel = model.Courriel;
            user.Age = model.Age;
            user.RamQ = model.RamQ;

            if (model.Photo != null && model.Photo.Length > 0)
            {
                user.Photo = model.Photo;
            }

            await context.SaveChangesAsync();
            return true;
        }
    }
}
