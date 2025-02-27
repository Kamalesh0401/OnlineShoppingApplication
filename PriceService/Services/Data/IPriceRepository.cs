// IProductRepository.cs
using Master.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IPriceRepository
    {
        Task<List<PriceObject>> GetPriceByProductIdAsync(PriceObjectInputObject input);
        Task<OperationStatus> UpdatePriceByProductIdAsync(PriceUpdateObject input);
    }
}