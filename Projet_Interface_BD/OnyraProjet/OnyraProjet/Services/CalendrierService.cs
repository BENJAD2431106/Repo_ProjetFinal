using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;
using OnyraProjet.Partials;

namespace OnyraProjet.Services
{
    public class CalendrierService
    {

        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> myCalendarFactory;

        public CalendrierService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory)
        {
            this.myCalendarFactory = myFactory;
        }
        public async Task AjouterCalendrier(Calendrier calendrier)
        {
            var dbContext = await myCalendarFactory.CreateDbContextAsync();

            try
            {
                dbContext.Calendriers.Add(calendrier);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public async Task<List<DateOnly>> SavoirSiRempli(int idUtilisateur)
        {
            var db = await myCalendarFactory.CreateDbContextAsync();
            var joursRemplis = new List<DateOnly>();
            joursRemplis = await db.Calendriers
                    .Where(c => c.NoUtilisateur == idUtilisateur)
                    .Select(c => c.Dates) 
                    .Distinct()
                    .ToListAsync();
            return joursRemplis;
        }
        public async Task ModifierCalendrier(Calendrier cal)
        {
            var db = await myCalendarFactory.CreateDbContextAsync();
            db.Calendriers.Update(cal);
            await db.SaveChangesAsync();
        }
        public async Task<bool> ModifierCalendrier(int idUtilisateur, DateOnly jour, string note, TimeOnly? heureCoucher, TimeOnly? heureLever, DateOnly dateCoucher,DateOnly dateLever)
        {
            var db = await myCalendarFactory.CreateDbContextAsync();

            var existant = await db.Calendriers
                .FirstOrDefaultAsync(c => c.NoUtilisateur == idUtilisateur
                                       && c.Dates == jour);

            if (existant == null)
                return false; // pas trouvé

            existant.Commentaire = note;
            existant.HeureCoucher = dateCoucher.ToDateTime(heureCoucher.Value);
            existant.HeureLever = dateLever.ToDateTime(heureLever.Value);

            await db.SaveChangesAsync();
            return true;
        }


    }
}


