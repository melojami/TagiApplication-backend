using Microsoft.AspNetCore.Mvc;
using TagiApplication.Models;
using TagiApplication.Operations;

namespace TagiApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResurssiTagitController : ControllerBase
    {
        private readonly IResurssiTagiOperations _operations;

        public ResurssiTagitController(IResurssiTagiOperations operations)
        {
            this._operations = operations;
        }

        [HttpGet]
        public async Task<ResurssiTagiResponse[]> Get()
        {
            var resurssiTagit = await this._operations.GetListAsync();
            return resurssiTagit;
        }

        [HttpGet("{id}")]
        public async Task<ResurssiTagiResponse> Get(int id)
        {
            var resurssiTagi = await this._operations.GetByIdAsync(id);
            return resurssiTagi;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResurssiTagiRequest req)
        {
            int created = await this._operations.AddAsync(req);
            return Created("", new { id = created });
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await this._operations.DeleteAsync(id);
        }

        [Route("InsertMany")]
        [HttpPost]
        public async Task<IActionResult> PostMany([FromBody] ResurssiTagiMultipleRequest req)
        {
            try
            {
                int[] resurssiIds = req.resurssiIds.ToArray();
                int[] tagiIds = req.tagiIds.ToArray();
                await this._operations.InsertMany(resurssiIds, tagiIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
