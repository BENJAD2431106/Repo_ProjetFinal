using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;

namespace OnyraProjet.Services
{
    public class DonneeService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> _factory;

        public DonneeService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> factory)
        {
            _factory = factory;
        }

        // Récupère toutes les données d'un utilisateur
        public async Task<List<Donnee>> GetDonneesByUtilisateurAsync(int noUtilisateur)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Donnees
                .Where(d => d.NoUtilisateur == noUtilisateur)
                .OrderByDescending(d => d.DateHeure)
                .ToListAsync();
        }

        // Récupère toutes les dates uniques pour un utilisateur
        public async Task<List<DateOnly>> GetDatesDisponiblesAsync(int noUtilisateur)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Donnees
                .Where(d => d.NoUtilisateur == noUtilisateur)
                .Select(d => DateOnly.FromDateTime(d.DateHeure))
                .Distinct()
                .OrderByDescending(d => d)
                .ToListAsync();
        }

        // Récupère les données filtrées par date pour un utilisateur
        public async Task<List<Donnee>> GetDonneesParDateAsync(int noUtilisateur, DateOnly date)
        {
            using var context = await _factory.CreateDbContextAsync();
            return await context.Donnees
                .Where(d => d.NoUtilisateur == noUtilisateur && DateOnly.FromDateTime(d.DateHeure) == date)
                .OrderByDescending(d => d.DateHeure)
                .ToListAsync();
        }
    }
}
