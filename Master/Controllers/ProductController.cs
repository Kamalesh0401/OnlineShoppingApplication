using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Common.Models;
using Common.Core;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "admin")]
        [HttpPost("AddProduct")]
        public async Task<OperationStatus> AddProduct([FromBody] ProductObject input)
        {
            return await this._service.AddProduct(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("UpdateProductById")]
        public async Task<OperationStatus> UpdateProductById([FromBody] ProductObject input)
        {
            return await this._service.UpdateProductById(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteProductById")]
        public async Task<OperationStatus> DeleteProductById(string prod_id)
        {
            return await this._service.DeleteProductById(this.GetSessionInfo(), prod_id).ConfigureAwait(false);
        }
    }
}
