using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;
using TagiApplication.Repositories;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagitController : ControllerBase
    {

        //public ITagiRepository _repository;
        private readonly ICommonRepository<Tagi, TagiResponse, TagiRequest> _repository;
        private readonly ITagiOperations _operations;
        
        //private readonly ICommonRepository<Tagi> _commonRepo;

        public TagitController(ITagiOperations operations)
        //public TagitController(ICommonRepository<Tagi, TagiResponse, TagiRequest> repository)
        {
            //this._repository = repository;
            this._operations = operations;
        }

        // GET: api/<TagitController>
        [HttpGet]
        public async Task<TagiResponse[]> Get()
        {
            //var tagit = await _repository.GetListAsync();
            var tagit = await this._operations.GetListAsync();
            return tagit;
        }

        // GET api/<TagitController>/5
        [HttpGet("{id}")]
        public async Task<TagiResponse> Get(int id)
        {
            //var tagi = await this._repository.GetByIdAsync(id);
            var tagi = await this._operations.GetByIdAsync(id);
            return tagi;
        }

        // POST api/<TagitController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagiRequest req)
        {
            //int created = await this._repository.AddAsync(req);
            int created = await this._operations.AddAsync(req);
            return Created("", new { id = created });
        }

        // PUT api/<TagitController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TagiRequest req)
        {
            //await this._repository.UpdateAsync(id, req);
            await this._operations.UpdateAsync(id, req);
            return Ok(req);
        }

        // DELETE api/<TagitController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            //await this._repository.DeleteAsync(id);
            await this._operations.DeleteAsync(id);
        }
    }
}
