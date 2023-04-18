using TagiApplication.DAL;
using TagiApplication.Models;

namespace TagiApplication.Operations
{
    public class TagityyppiOperations : ITagityyppiOperations
    {
        private IUnitOfWork _unitOfWork;

        public TagityyppiOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TagityyppiResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.TagityyppiRepository.GetListAsync();
            return list;
        }

        public async Task<TagityyppiResponse> GetByIdAsync(int id)
        {
            var tagityyppi = await this._unitOfWork.TagityyppiRepository.GetByIdAsync(id);
            return tagityyppi;
        }
    }

    public interface ITagityyppiOperations
    {
        public Task<TagityyppiResponse[]> GetListAsync();

        public Task<TagityyppiResponse> GetByIdAsync(int id);
    }
}
