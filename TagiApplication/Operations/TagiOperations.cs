using Azure.Core;
using TagiApplication.DAL;
using TagiApplication.Models;
using TagiApplication.Repositories;

namespace TagiApplication.Operations
{
    public class TagiOperations : ITagiOperations
    {
        private IUnitOfWork _unitOfWork;

        public TagiOperations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<TagiResponse[]> GetListAsync()
        {
            var list = await this._unitOfWork.TagiRepository.GetListAsync();
            return list;
        }

        public async Task<TagiResponse> GetByIdAsync(int id)
        {
            var tagi = await this._unitOfWork.TagiRepository.GetByIdAsync(id);
            return tagi;
        }

        public async Task<int> AddAsync(TagiRequest req)
        {
            int created = await this._unitOfWork.TagiRepository.AddAsync(req);
            return created;
        }

        public async Task UpdateAsync(int id, TagiRequest req)
        {
            await this._unitOfWork.TagiRepository.UpdateAsync(id, req);
        }

        public async Task DeleteAsync(int id)
        {
            await this._unitOfWork.TagiRepository.DeleteAsync(id);
        }
    }


    public interface ITagiOperations
    {
        public Task<TagiResponse[]> GetListAsync();

        public Task<TagiResponse> GetByIdAsync(int id);

        public Task<int> AddAsync(TagiRequest req);

        public Task UpdateAsync(int id, TagiRequest req);

        public Task DeleteAsync(int id);
    }
}
