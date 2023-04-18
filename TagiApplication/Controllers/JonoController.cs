using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JonotController : ControllerBase
    {
        private readonly IJonoOperations _operations;

        public JonotController(IJonoOperations operations)
        {
            this._operations = operations;
        }

        [HttpGet]
        public async Task<JonoResponse[]> Get()
        {
            var jonot = await this._operations.GetListAsync();
            return jonot;
        }

        [HttpGet("{id}")]
        public async Task<JonoResponse> Get(int id)
        {
            var jono = await this._operations.GetByIdAsync(id);
            return jono;
        }
    }
}
