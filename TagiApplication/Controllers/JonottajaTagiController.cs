using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JonottajaTagitController : ControllerBase
    {
        private readonly IJonottajaTagiOperations _operations;

        public JonottajaTagitController(IJonottajaTagiOperations operations)
        {
            this._operations = operations;
        }

        [HttpGet]
        public async Task<JonottajaTagiResponse[]> Get()
        {
            var jonottajaTagit = await this._operations.GetListAsync();
            return jonottajaTagit;
        }

        [HttpGet("{id}")]
        public async Task<JonottajaTagiResponse> Get(int id)
        {
            var JonoTagi = await this._operations.GetByIdAsync(id);
            return JonoTagi;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JonottajaTagiRequest req)
        {
            int created = await this._operations.AddAsync(req);
            return Created("", new { id = created });
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this._operations.DeleteAsync(id);
        }
    }
}
