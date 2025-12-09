using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.VisualBasic;
using OnyraProjet.Authentication;
using OnyraProjet.Data;
using OnyraProjet.Models;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace OnyraProjet.Services
{
    public class ConnexionService
    {
        private IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;
        public ConnexionService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        public async Task<UserSession2> ConnexionEtRecupererSession(string courriel, string motDePasse)
        {
            using var context = await _factory.CreateDbContextAsync();

            var paramCourriel = new SqlParameter("@courriel", courriel);
            var paramMotDePasse = new SqlParameter("@motDePasse", motDePasse);

            var paramNoUtilisateur = new SqlParameter
            {
                ParameterName = "@noUtilisateur",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await context.Database.ExecuteSqlRawAsync(
                "EXEC UP_ConnexionUtilisateur @courriel, @motDePasse, @noUtilisateur OUTPUT",
                paramCourriel, paramMotDePasse, paramNoUtilisateur);

            int id;

            if (paramNoUtilisateur.Value == DBNull.Value || paramNoUtilisateur.Value == null)
            {
                id = -1; // On met id = -1 pour signaler une erreur / pas d’utilisateur
            }
            else
            {
                id = (int)paramNoUtilisateur.Value; // Réussi on récupère l’ID
            }

            if (id == -1) // signifie “pas de session” / “connexion échouée”.
                return null; 

            var user = await context.Utilisateurs
                .Where(u => u.NoUtilisateur == id)
                .Select(u => new UserSession2
                {
                    Id = u.NoUtilisateur,
                    Name = u.PrenomUtilisateur + " " + u.NomUtilisateur,
                    Role = "User"
                })
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
