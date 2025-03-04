using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Master.Core;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : BaseController
    {
        #region Private Elements

        private readonly IPriceService _service;

        #endregion

        #region Constructor

        public PriceController(IPriceService service)
        {
            this._service = service;
        }
        #endregion


        [HttpGet("GetPriceByProductId")]
        public async Task<List<PriceObject>> GetPriceByProductId([FromQuery] PriceObjectInputObject input)
        {
            return await this._service.GetPriceByProductId(this.GetSessionInfo(), input).ConfigureAwait(false);
        }

        [HttpPost("UpdatePriceByProductId")]
        public async Task<OperationStatus> UpdatePriceByProductId([FromBody] PriceUpdateObject input)
        {
            return await this._service.UpdatePriceByProductId(this.GetSessionInfo(), input).ConfigureAwait(false);
        }

    }
}
