using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class JonoOperations : IJonoOperations
    {
        private IUnitOfWork _unitOfWork;

        public JonoOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<JonoResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.JonoRepository.GetListAsync();
            return list;
        }

        public async Task<JonoResponse> GetByIdAsync(int id)
        {
            var jono = await this._unitOfWork.JonoRepository.GetByIdAsync(id);
            return jono;
        }
    }

    public interface IJonoOperations
    {
        public Task<JonoResponse[]> GetListAsync();

        public Task<JonoResponse> GetByIdAsync(int id);
    }
}

