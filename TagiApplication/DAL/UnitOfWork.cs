using NuGet.Protocol.Core.Types;
using TagiApplication.Models;
using TagiApplication.Repositories;

namespace TagiApplication.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private TagiContext _context;

        private ICommonRepository<Tagi, TagiResponse, TagiRequest> tagiRepository;
        private ICommonRepository<Tagityyppi, TagityyppiResponse, TagityyppiRequest> tagityyppiRepository;
        private ICommonRepository<Resurssi, ResurssiResponse, ResurssiRequest> resurssiRepository;
        private ICommonRepository<ResurssiTagi, ResurssiTagiResponse, ResurssiTagiRequest> resurssiTagiRepository;
        private ICommonRepository<Jono, JonoResponse, JonoRequest> jonoRepository;
        private ICommonRepository<Jonottaja, JonottajaResponse, JonottajaRequest> jonottajaRepository;
        private ICommonRepository<JonottajaTagi, JonottajaTagiResponse, JonottajaTagiRequest> jonottajaTagiRepository;
        private IResurssiTagiMultipleRepository resurssiTagiMultipleRepository;//MULTIPLE

        public UnitOfWork(TagiContext context)
        {
            this._context = context;
        }

        public ICommonRepository<Tagi, TagiResponse, TagiRequest> TagiRepository
        {
            get
            {
                if (this.tagiRepository == null)
                {
                    this.tagiRepository = new CommonRepository<Tagi, TagiResponse, TagiRequest>(_context);
                }
                return this.tagiRepository;
            }
        }

        public ICommonRepository<Tagityyppi, TagityyppiResponse, TagityyppiRequest> TagityyppiRepository
        {
            get
            {
                if (this.tagityyppiRepository == null)
                {
                    this.tagityyppiRepository = new CommonRepository<Tagityyppi, TagityyppiResponse, TagityyppiRequest>(_context);
                }
                return this.tagityyppiRepository;
            }
        }

        public ICommonRepository<Resurssi, ResurssiResponse, ResurssiRequest> ResurssiRepository
        {
            get
            {
                if (this.resurssiRepository == null)
                {
                    this.resurssiRepository = new CommonRepository<Resurssi, ResurssiResponse, ResurssiRequest>(_context);
                }
                return this.resurssiRepository;
            }
        }

        public ICommonRepository<ResurssiTagi, ResurssiTagiResponse, ResurssiTagiRequest> ResurssiTagiRepository
        {
            get
            {
                if (this.resurssiTagiRepository == null)
                {
                    this.resurssiTagiRepository = new CommonRepository<ResurssiTagi, ResurssiTagiResponse, ResurssiTagiRequest>(_context);
                }
                return this.resurssiTagiRepository;
            }
        }

        public ICommonRepository<Jono, JonoResponse, JonoRequest> JonoRepository
        {
            get
            {
                if (this.jonoRepository == null)
                {
                    this.jonoRepository = new CommonRepository<Jono, JonoResponse, JonoRequest>(_context);
                }
                return this.jonoRepository;
            }
        }

        public ICommonRepository<Jonottaja, JonottajaResponse, JonottajaRequest> JonottajaRepository
        {
            get
            {
                if (this.jonottajaRepository == null)
                {
                    this.jonottajaRepository = new CommonRepository<Jonottaja, JonottajaResponse, JonottajaRequest>(_context);
                }
                return this.jonottajaRepository;
            }
        }

        public ICommonRepository<JonottajaTagi, JonottajaTagiResponse, JonottajaTagiRequest> JonottajaTagiRepository
        {
            get
            {
                if (this.jonottajaTagiRepository == null)
                {
                    this.jonottajaTagiRepository = new CommonRepository<JonottajaTagi, JonottajaTagiResponse, JonottajaTagiRequest>(_context);
                }
                return this.jonottajaTagiRepository;
            }
        }

        //MULTIPLE
        public IResurssiTagiMultipleRepository ResurssiTagiMultipleRepository
        {
            get
            {
                if (this.resurssiTagiMultipleRepository == null)
                {
                    this.resurssiTagiMultipleRepository = new ResurssiTagiMultipleRepository(_context);
                }
                return this.resurssiTagiMultipleRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public interface IUnitOfWork
    {
        ICommonRepository<Tagi, TagiResponse, TagiRequest> TagiRepository { get; }
        ICommonRepository<Tagityyppi, TagityyppiResponse, TagityyppiRequest> TagityyppiRepository { get; }
        ICommonRepository<Resurssi, ResurssiResponse, ResurssiRequest> ResurssiRepository { get; }
        ICommonRepository<ResurssiTagi, ResurssiTagiResponse, ResurssiTagiRequest> ResurssiTagiRepository { get; }
        ICommonRepository<Jono, JonoResponse, JonoRequest> JonoRepository { get; }
        ICommonRepository<Jonottaja, JonottajaResponse, JonottajaRequest> JonottajaRepository { get; }
        ICommonRepository<JonottajaTagi, JonottajaTagiResponse, JonottajaTagiRequest> JonottajaTagiRepository { get; }

        // Monen resurssitagin päivittämiseen //MULTIPLE
        IResurssiTagiMultipleRepository ResurssiTagiMultipleRepository { get; }
    }
}
