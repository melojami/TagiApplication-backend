using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class JonottajaTagiOperations : IJonottajaTagiOperations
    {
        private IUnitOfWork _unitOfWork;

        public JonottajaTagiOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<JonottajaTagiResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.JonottajaTagiRepository.GetListAsync();
            return list;
        }

        public async Task<JonottajaTagiResponse> GetByIdAsync(int id)
        {
            var Jonottaja = await this._unitOfWork.JonottajaTagiRepository.GetByIdAsync(id);
            return Jonottaja;
        }

        public async Task<int> AddAsync(JonottajaTagiRequest req)
        {
            int created = await this._unitOfWork.JonottajaTagiRepository.AddAsync(req);
            return created;
        }

        public async Task DeleteAsync(int id)
        {
            await this._unitOfWork.JonottajaTagiRepository.DeleteAsync(id);
        }
    }

    public interface IJonottajaTagiOperations
    {
        public Task<JonottajaTagiResponse[]> GetListAsync();
        public Task<JonottajaTagiResponse> GetByIdAsync(int id);
        public Task<int> AddAsync(JonottajaTagiRequest req);
        public Task DeleteAsync(int id);
    }
}