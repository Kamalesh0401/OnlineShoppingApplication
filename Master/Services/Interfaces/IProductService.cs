using Master.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductObject>> GetAllProduct(SessionInfo sessionInfo, ProductInputObject input);
        Task<List<ProductObject>> GetProductsById(SessionInfo sessionInfo, ProductInputObject input);
        Task<List<ProductObject>> GetProductsByCategoryId(SessionInfo sessionInfo, ProductInputObject input);
        Task<OperationStatus> AddProduct(SessionInfo sessionInfo, ProductObject input);
        Task<OperationStatus> UpdateProductById(SessionInfo sessionInfo, ProductObject input);
        Task<OperationStatus> DeleteProductById(SessionInfo sessionInfo, ProductObject input);
    }
}
