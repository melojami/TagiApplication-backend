using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResurssitController : ControllerBase
    {
        private readonly IResurssiOperations _operations;

        public ResurssitController(IResurssiOperations operations)
        {
            this._operations = operations;
        }

        // GET: api/<TagitController>
        [HttpGet]
        public async Task<ResurssiResponse[]> Get()
        {
            var resurssit = await this._operations.GetListAsync();
            return resurssit;
        }

        // GET api/<TagitController>/5
        [HttpGet("{id}")]
        public async Task<ResurssiResponse> Get(int id)
        {
            var resurssi = await this._operations.GetByIdAsync(id);
            return resurssi;
        }
    }
}
