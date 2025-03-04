using Master.Models;
using Common.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;
using Common.Logging;

namespace Master.Services
{
    public class ProductService : IProductService
    {
        #region Constructor
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        #endregion

        #region Private Methods


        #endregion

        #region Public Methods

        public async Task<List<ProductObject>> GetAllProduct(SessionInfo sessionInfo, ProductInputObject input)
        {
            var output = new List<ProductObject>();
            try
            {
                output = await _repository.GetAllProductsAsync(input);
                //LogHelper.LogInformation(_logger, "Get all products service", input);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(_logger, ex, $"Session Info: , Input :");

            }
            return output;
        }

        public async Task<List<ProductObject>> GetProductsById(SessionInfo sessionInfo, ProductInputObject input)
        {
            var output = new List<ProductObject>();
            try
            {
                output = await _repository.GetProductsByIdAsync(input);
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }

        public async Task<List<ProductObject>> GetProductsByCategoryId(SessionInfo sessionInfo, ProductInputObject input)
        {
            var output = new List<ProductObject>();
            try
            {
                output = await _repository.GetProductsByCategoryIdAsync(input);
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }

        public async Task<OperationStatus> AddProduct(SessionInfo sessionInfo, ProductObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.AddProductAsync(input);
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";
            }
            return output;
        }

        public async Task<OperationStatus> UpdateProductById(SessionInfo sessionInfo, ProductObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.UpdateProductByIdAsync(input);
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";
            }
            return output;
        }
        public async Task<OperationStatus> DeleteProductById(SessionInfo sessionInfo, string prod_id)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.DeleteProductByIdAsync(prod_id);
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";
            }
            return output;
        }
        #endregion

    }
}
