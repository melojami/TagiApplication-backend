using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagityypitController : ControllerBase
    {
        private readonly ITagityyppiOperations _operations;

        public TagityypitController(ITagityyppiOperations operations)
        {
            this._operations = operations;
        }

        // GET: api/<TagitController>
        [HttpGet]
        public async Task<TagityyppiResponse[]> Get()
        {
            var tagityypit = await this._operations.GetListAsync();
            return tagityypit;
        }

        // GET api/<TagitController>/5
        [HttpGet("{id}")]
        public async Task<TagityyppiResponse> Get(int id)
        {
            var tagityyppi = await this._operations.GetByIdAsync(id);
            return tagityyppi;
        }
    }
}
