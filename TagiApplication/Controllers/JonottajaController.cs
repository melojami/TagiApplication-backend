using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JonottajatController : ControllerBase
    {
        private readonly IJonottajaOperations _operations;

        public JonottajatController(IJonottajaOperations operations)
        {
            this._operations = operations;
        }

        [HttpGet]
        public async Task<JonottajaResponse[]> Get()
        {
            var jonottajat = await this._operations.GetListAsync();
            return jonottajat;
        }

        [HttpGet("{id}")]
        public async Task<JonottajaResponse> Get(int id)
        {
            var jonottaja = await this._operations.GetByIdAsync(id);
            return jonottaja;
        }
    }
}