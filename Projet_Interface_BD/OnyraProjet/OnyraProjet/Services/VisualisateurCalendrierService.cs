using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OnyraProjet.Data;
using OnyraProjet.Models;
using OnyraProjet.Partials;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace OnyraProjet.Services
{
    public class VisualisateurCalendrierService
    {
        private readonly IDbContextFactory<Prog3a25ProductionAllysonJadContext> myCalendarVisFactory;

        public VisualisateurCalendrierService(IDbContextFactory<Prog3a25ProductionAllysonJadContext> myFactory)
        {
            this.myCalendarVisFactory = myFactory;
        }
        public async Task<Calendrier?> VisualiserDate(DateTime date, int noUtilisateur)
        {
            var dbContext = await myCalendarVisFactory.CreateDbContextAsync();

            try
            {
                return await dbContext.Calendriers
                    .Where(c => c.NoUtilisateur == noUtilisateur 
                    && c.HeureLever.Date == date.Date)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
    }
}
