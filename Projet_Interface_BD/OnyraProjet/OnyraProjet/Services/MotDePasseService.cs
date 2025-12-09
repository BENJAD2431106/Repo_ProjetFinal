using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Components.Pages;
using OnyraProjet.Data;
using System.Data;

namespace OnyraProjet.Services
{
    public class MotDePasseService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

        public MotDePasseService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        public async Task<bool> VerifierMotDePasseAsync(int userId, string motDePasse)
        {
            await using var context = await _factory.CreateDbContextAsync();  // await using = ferme automatiquement et proprement la connexion SQL d'une manière optimisée pour l’asynchrone. (Sinon : fuite de connexions ,base “saturée”, ralentissements, comportements imprévisibles)
            await using var connection = (SqlConnection)context.Database.GetDbConnection();
            await connection.OpenAsync();

            var requeteVerification = @"
            SELECT COUNT(*) 
            FROM Utilisateurs
            WHERE NoUtilisateur = @UserId
              AND MotDePasse = HASHBYTES('SHA2_512', @MotDePasse + CAST(sel AS NVARCHAR(36)))";

            await using var cmd = new SqlCommand(requeteVerification, connection);
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@MotDePasse", SqlDbType.NVarChar, 255).Value = motDePasse;

            return (int)await cmd.ExecuteScalarAsync() == 1;
        }

        public async Task<bool> ModifierMotDePasseAsync(int userId, string nouveauMotDePasse)
        {
            await using var context = await _factory.CreateDbContextAsync();
            await using var connection = (SqlConnection)context.Database.GetDbConnection();
            await connection.OpenAsync();

            var requeteMiseAJour = @"
            UPDATE Utilisateurs
            SET MotDePasse = HASHBYTES('SHA2_512', @NouveauMotDePasse + CAST(sel AS NVARCHAR(36)))
            WHERE NoUtilisateur = @UserId";

            await using var cmd = new SqlCommand(requeteMiseAJour, connection);
            cmd.Parameters.Add("@NouveauMotDePasse", SqlDbType.NVarChar, 255).Value = nouveauMotDePasse;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            return await cmd.ExecuteNonQueryAsync() == 1;
        }
    }
}
