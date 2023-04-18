using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class JonottajaOperations : IJonottajaOperations
    {
        private IUnitOfWork _unitOfWork;

        public JonottajaOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<JonottajaResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.JonottajaRepository.GetListAsync();
            return list;
        }

        public async Task<JonottajaResponse> GetByIdAsync(int id)
        {
            var jono = await this._unitOfWork.JonottajaRepository.GetByIdAsync(id);
            return jono;
        }
    }

    public interface IJonottajaOperations
    {
        public Task<JonottajaResponse[]> GetListAsync();

        public Task<JonottajaResponse> GetByIdAsync(int id);
    }
}
