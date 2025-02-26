// IProductRepository.cs
using Master.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IProductRepository
    {
        Task<List<ProductObject>> GetAllProductsAsync(ProductObject input);
        Task<OperationStatus> AddProductAsync(ProductObject input);
    }
}