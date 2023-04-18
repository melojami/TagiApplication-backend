using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class ResurssiTagiOperations : IResurssiTagiOperations
    {
        private IUnitOfWork _unitOfWork;

        public ResurssiTagiOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ResurssiTagiResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.ResurssiTagiRepository.GetListAsync();
            return list;
        }

        public async Task<ResurssiTagiResponse> GetByIdAsync(int id)
        {
            var resurssi = await this._unitOfWork.ResurssiTagiRepository.GetByIdAsync(id);
            return resurssi;
        }

        public async Task<int> AddAsync(ResurssiTagiRequest req)
        {
            int created = await this._unitOfWork.ResurssiTagiRepository.AddAsync(req);
            return created;
        }

        public async Task DeleteAsync(int id)
        {
            await this._unitOfWork.ResurssiTagiRepository.DeleteAsync(id);
        }

        //MULTIPLE
        public async Task InsertMany(int[] resurssiIds, int[] tagiIds)
        {
            await this._unitOfWork.ResurssiTagiMultipleRepository.InsertMany(resurssiIds, tagiIds);
        }
    }

    public interface IResurssiTagiOperations
    {
        public Task<ResurssiTagiResponse[]> GetListAsync();
        public Task<ResurssiTagiResponse> GetByIdAsync(int id);
        public Task<int> AddAsync(ResurssiTagiRequest req);
        public Task DeleteAsync(int id);

        //MULTIPLE 
        public Task InsertMany(int[] resurssiIds, int[] tagiIds);
    }
}

