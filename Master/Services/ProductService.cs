using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;

namespace Master.Services
{
    public class ProductService : IProductService
    {
        #region Constructor
        //public ProductService(IServiceProvider serviceProvider, ILogger<ProductService> logger) { }

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
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
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

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
        public async Task<OperationStatus> DeleteProductById (SessionInfo sessionInfo, ProductObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.DeleteProductByIdAsync(input);
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
