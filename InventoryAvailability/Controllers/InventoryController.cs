using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Common.Models;
using Common.Core;
namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : BaseController
    {
        #region Private Elements

        private readonly IInventoryService _service;

        #endregion

        #region Constructor

        public InventoryController(IInventoryService service)
        {
            this._service = service;
        }
        #endregion


        [HttpGet("GetAvailabilityByProductId")]
        public async Task<List<InventoryObject>> GetAvailabilityByProductId([FromQuery] InventoryInputObject input)
        {
            return await this._service.GetAvailabilityByProductId(this.GetSessionInfo(), input).ConfigureAwait(false);
        }

        [HttpPost("UpdateAvailabilityByProductId")]
        public async Task<OperationStatus> UpdateAvailabilityByProductId([FromBody] InventoryObject input)
        {
            return await this._service.UpdateAvailabilityByProductId(this.GetSessionInfo(), input).ConfigureAwait(false);
        }

    }
}
