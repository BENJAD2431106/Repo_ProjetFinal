using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using System.Data;

public class MotDePasseService
{
    private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

    public MotDePasseService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
    {
        _factory = factory;
    }

    // Vérifie le mot de passe actuel
    public async Task<bool> VerifierMotDePasseAsync(int noUtilisateur, string motDePasseActuel)
    {
        var dbContext = await _factory.CreateDbContextAsync();

        var paramNoUtilisateur = new SqlParameter("@noUtilisateur", noUtilisateur);
        var paramMotDePasse = new SqlParameter("@motDePasseActuel", motDePasseActuel);
        var paramNouveauMotDePasse = new SqlParameter("@nouveauMotDePasse", DBNull.Value);
        var paramResult = new SqlParameter("@result", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC UP_VerifierEtModifierMotDePasse @noUtilisateur, @motDePasseActuel, @nouveauMotDePasse, @result OUTPUT",
            paramNoUtilisateur, paramMotDePasse, paramNouveauMotDePasse, paramResult
        );

        int result = (int)paramResult.Value;
        return result != -1;
    }

    // Modifie le mot de passe
    public async Task<bool> ModifierMotDePasseAsync(int noUtilisateur, string motDePasseActuel, string nouveauMotDePasse)
    {
        var dbContext = await _factory.CreateDbContextAsync();

        var paramNoUtilisateur = new SqlParameter("@noUtilisateur", noUtilisateur);
        var paramMotDePasse = new SqlParameter("@motDePasseActuel", motDePasseActuel);
        var paramNouveauMotDePasse = new SqlParameter("@nouveauMotDePasse", nouveauMotDePasse);
        var paramResult = new SqlParameter("@result", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await dbContext.Database.ExecuteSqlRawAsync(
            "EXEC UP_VerifierEtModifierMotDePasse @noUtilisateur, @motDePasseActuel, @nouveauMotDePasse, @result OUTPUT",
            paramNoUtilisateur, paramMotDePasse, paramNouveauMotDePasse, paramResult
        );

        int result = (int)paramResult.Value;
        return result != -1;
    }
}
