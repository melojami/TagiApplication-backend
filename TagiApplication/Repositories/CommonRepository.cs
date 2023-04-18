using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagiApplication.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace TagiApplication.Repositories
{
    public class CommonRepository<Model, Response, Request> : ICommonRepository<Model, Response, Request> 
        where Model : ModelBase<Model, Response, Request>, new()
        where Response : class, new()
        where Request : class
    {
        public CommonRepository(TagiContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<Model>();
        }

        public async Task<Response[]> GetListAsync()
        {
            IQueryable<Model> query = this._dbSet;
            var ret = await query
                .Select(x => x.ToDTO(x))
                .ToArrayAsync();
            return ret;
        }

        public async Task<Response> GetByIdAsync(int Id)
        {
            IQueryable<Model> query = this._dbSet;
            var ret = await query
                .Where(x => x.Id == Id)
                .Select(m => m.ToDTO(m))
                .FirstOrDefaultAsync();
            return ret;
        }

        public async Task<int> AddAsync(Request req)
        {
            Model model = new Model().FromDTO(req);

            this._context.Add<Model>(model);
            await _context.SaveChangesAsync(CancellationToken.None);
            int created = (int)model.GetType().GetProperty("Id").GetValue(model); // TODO: järkevämmin haku. Ei nyt jostain syystä saa modelin Id:tä kiinni jos vaan kysyy, että model.Id
            return created;
        }

        public async Task UpdateAsync(int Id, Request req)
        {
            if (req.GetType().GetProperty("Id") is null || req.GetType().GetProperty("Id") != null && (int)req.GetType().GetProperty("Id").GetValue(req, null) != Id)
            {
                throw new ArgumentException("ID:t eivät täsmää");
            }

            IQueryable<Model> query = this._dbSet;
            
            var model = await query
                .Where(x => x.Id == Id)
                .AsNoTracking() // Tärkeä. Muuten tulee konflikti, kun alkaa seurata tätä haettua
                .Select(m => m.FromDTO(req))
                .FirstOrDefaultAsync();
            if (model is null) return;

            this._context.Update<Model>(model);
            await this._context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task DeleteAsync(int id)
        {
            IQueryable<Model> query = this._dbSet;

            var model = await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (model is null) return;

            this._context.Remove<Model>(model);

            await this._context.SaveChangesAsync(CancellationToken.None);
        }

        private readonly TagiContext _context;
        internal DbSet<Model> _dbSet;        
    }

    public interface ICommonRepository<Model, Response, Request>
        where Model : ModelBase<Model, Response, Request>, new()
        where Response : class, new()
        where Request: class
    {
        public Task<Response[]> GetListAsync();

        public Task<Response> GetByIdAsync(int id);

        public Task<int> AddAsync(Request req);

        public Task UpdateAsync(int Id, Request req);

        public Task DeleteAsync(int id);
    }
}
