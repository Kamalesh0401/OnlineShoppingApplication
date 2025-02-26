using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Master.Core;

namespace Master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : BaseController
    {
        #region Private Elements

        private readonly IProductService _service;

        #endregion

        #region Constructor

        public ProductController(IProductService service)
        {
            this._service = service;
        }
        #endregion

        [HttpPost("GetAllProduct")]
        public async Task<List<ProductObject>> GetAllProduct([FromBody] ProductObject input)
        {
            return await this._service.GetAllProduct(this.GetSessionInfo(), input).ConfigureAwait(false);
        }

        [HttpPost("AddProduct")]
        public async Task<OperationStatus> AddProduct([FromBody] ProductObject input)
        {
            return await this._service.AddProduct(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
    }
}
