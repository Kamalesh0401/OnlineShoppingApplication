using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Master.Core;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("GetAllProducts")]
        public async Task<List<ProductObject>> GetAllProduct([FromQuery] ProductInputObject input)
        {
            return await this._service.GetAllProduct(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [HttpGet("GetProductsById")]
        public async Task<List<ProductObject>> GetProductsById([FromQuery] ProductInputObject input)
        {
            return await this._service.GetProductsById(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [HttpGet("GetProductsByCategoryId")]
        public async Task<List<ProductObject>> GetProductsByCategoryId([FromQuery] ProductInputObject input)
        {
            return await this._service.GetProductsByCategoryId(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [HttpPost("AddProduct")]
        public async Task<OperationStatus> AddProduct([FromBody] ProductObject input)
        {
            return await this._service.AddProduct(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [HttpPost("UpdateProductById")]
        public async Task<OperationStatus> UpdateProductById([FromBody] ProductObject input)
        {
            return await this._service.UpdateProductById(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [HttpPost("DeleteProductById")]
        public async Task<OperationStatus> DeleteProductById([FromBody] ProductObject input)
        {
            return await this._service.DeleteProductById(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
    }
}
