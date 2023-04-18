using TagiApplication.Models;
using TagiApplication.Repositories;
using TagiApplication.DAL;

namespace TagiApplication.Operations
{
    [Obsolete("Ei käytössä, menisi unit of workin kanssa liian monimutkaiseksi ainakin tässä vaiheessa", true)]
    public class CommonOperations<Model, Response, Request> : ICommonOperations<Model, Response, Request>
        where Model : ModelBase<Model, Response, Request>, new()
        where Response : class, new()
        where Request : class
    {
        private ICommonRepository<Model, Response, Request> _commonRepository;
        private UnitOfWork _unitOfWork;

        public CommonOperations(ICommonRepository<Model, Response, Request> _commonRepository, UnitOfWork unitOfWork) 
        { 
            this._commonRepository = _commonRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Response[]> GetListAsync()
        {
            var list = await this._commonRepository.GetListAsync();
            return list;
        }
    }


    public interface ICommonOperations<Model, Response, Request>
    {
        public Task<Response[]> GetListAsync();
    }
}
