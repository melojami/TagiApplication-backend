using Microsoft.EntityFrameworkCore;
using TagiApplication.Models;

namespace TagiApplication.Repositories
{
    /**
     * Hoitaa resurssien tagien massalisäyksen, sillä se eroaa common Repon toimintalogiikasta
     * */
    public class ResurssiTagiMultipleRepository : IResurssiTagiMultipleRepository
    {
        public ResurssiTagiMultipleRepository(TagiContext context) 
        {
            this._context = context;
            this._dbSet = context.Set<Resurssi>();
        }

        // TODO: Tämä ei ole järkevä, ylipäätään koko repositoryn rakentaminen näin. Kunhan tein tämän nopeasti ja likaisesti, jotta voin demota massatyökalua.
        // Pitäisi käyttää bulk insertiä tms. tähän. Lisää nyt vain yhden tagin kerrallaan.
        public async Task InsertMany(int[] resurssiIds, int[] tagiIds)
        {
            /* Kanta huolehtii
            var result = this._context.Resurssit.Where(x => resurssiIds.Contains(x.Id)).ToArray();
            if(result.Length != resurssiIds.Length)
            {
                throw new Exception("Virheellinen resurssilista");
            }
            */

            List<ResurssiTagi> tagiLista = new List<ResurssiTagi>();
            foreach(int resId in resurssiIds)
            {
                foreach(int tagiId in tagiIds)
                {
                    ResurssiTagi tagi = new ResurssiTagi();
                    tagi.ResurssiId = resId;
                    tagi.TagiId = tagiId;
                    tagi.Poistettu = null;
                    tagiLista.Add(tagi);
                }
            }

            await this._context.ResurssiTagit.AddRangeAsync(tagiLista);
            await this._context.SaveChangesAsync(CancellationToken.None);
            
        }

        private readonly TagiContext _context;
        internal DbSet<Resurssi> _dbSet;
    }

    public interface IResurssiTagiMultipleRepository
    {
        public Task InsertMany(int[] resurssiIds, int[] tagiIds);
    }
}
