using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagiApplication.Models;

namespace TagiApplication.Repositories
{
    [Obsolete("Tageissa käytetään CommonRepositorya", true)]
    public class TagiRepository : ITagiRepository
    {
        public TagiRepository(TagiContext context)
        {
            this._context = context;
        }

        public async Task<TagiResponse[]> GetListAsync()
        {
            /* Tämä on se käsin kirjoitettu versio, toimii myös 
            var tagit = await _context.Tagit.Select(t => new TagiResponse
            {
                Id = t.Id,
                ResurssityyppiId = t.ResurssityyppiId,
                Nimi = t.Nimi,
            }).ToArrayAsync();
            return tagit;
            */


            /* Tämä toimii staticprojektion kautta, malliversio
            var tagit = await _context.Tagit.Select(Tagi.Projection).ToArrayAsync();
            return tagit;
            */

            /* Projektio ei-staattisen metodin kautta, toimii */
            var tagit = await _context.Tagit.Select(x => x.ToDTO(x)).ToArrayAsync();

            return tagit;


        }

        public async Task<TagiResponse> GetTagiByIdAsync(int Id)
        {
            var tagi = await _context.Tagit.Where(t => t.Id == Id)
                .Select(r => new TagiResponse
                {
                    Id = r.Id,
                    ResurssityyppiId = r.ResurssityyppiId,
                    Nimi = r.Nimi
                }).FirstOrDefaultAsync();
            return tagi;
        }

        public async Task<int> AddTagiAsync(TagiRequest req)
        {
            Tagi tagi = new Tagi
            {
                ResurssityyppiId = req.Resurssityyppiid,
                Nimi = req.Nimi
            };

            await _context.Tagit.AddAsync(tagi);
            await _context.SaveChangesAsync(CancellationToken.None);
            return tagi.Id;
        }

        public async Task UpdateTagiAsync(int Id, TagiRequest req)
        {
            if (Id != req.Id)
                throw new ArgumentException();
            
            Tagi? tagi = await _context.Tagit.Where(t => t.Id == Id).SingleOrDefaultAsync();
            
            if (tagi is null)
                throw new Exception();

            tagi.ResurssityyppiId = req.Resurssityyppiid;
            tagi.Nimi= req.Nimi;

            _context.Update(tagi);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            Tagi tagi = await _context.Tagit.FindAsync(id);
            if (tagi == null) return;

            _context.Tagit.Remove(tagi);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        private TagiContext _context;
    }

    public interface ITagiRepository
    {
        public Task<TagiResponse[]> GetListAsync();

        public Task<TagiResponse> GetTagiByIdAsync(int id);

        public Task<int> AddTagiAsync(TagiRequest req);

        public Task UpdateTagiAsync(int Id, TagiRequest req);

        public Task DeleteAsync(int id);
    }
}
