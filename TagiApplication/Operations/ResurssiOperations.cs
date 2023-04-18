using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class ResurssiOperations : IResurssiOperations
    {
        private IUnitOfWork _unitOfWork;

        public ResurssiOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ResurssiResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.ResurssiRepository.GetListAsync();
            return list;
        }

        public async Task<ResurssiResponse> GetByIdAsync(int id)
        {
            var resurssi = await this._unitOfWork.ResurssiRepository.GetByIdAsync(id);
            return resurssi;
        }
    }

    public interface IResurssiOperations
    {
        public Task<ResurssiResponse[]> GetListAsync();

        public Task<ResurssiResponse> GetByIdAsync(int id);
    }
}
