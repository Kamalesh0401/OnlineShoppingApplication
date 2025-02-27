// IProductRepository.cs
using Master.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IProductRepository
    {
        Task<List<ProductObject>> GetAllProductsAsync(ProductInputObject input);
        Task<List<ProductObject>> GetProductsByIdAsync(ProductInputObject input);
        Task<List<ProductObject>> GetProductsByCategoryIdAsync(ProductInputObject input);
        Task<OperationStatus> AddProductAsync(ProductObject input);
        Task<OperationStatus> UpdateProductByIdAsync(ProductObject input);
        Task<OperationStatus> DeleteProductByIdAsync(ProductObject input);
    }
}