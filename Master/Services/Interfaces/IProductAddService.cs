using Master.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductObject>> GetAllProduct(SessionInfo sessionInfo, ProductObject input);
        Task<OperationStatus> AddProduct(SessionInfo sessionInfo, ProductObject input);
    }
}
