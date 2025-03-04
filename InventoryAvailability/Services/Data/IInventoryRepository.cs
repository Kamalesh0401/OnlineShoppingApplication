using Master.Models;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IInventoryRepository
    {
        Task<List<InventoryObject>> GetAvailabilityByProductIdAsync(InventoryInputObject input);
        Task<OperationStatus> UpdateAvailabilityByProductIdAsync(InventoryObject input);
    }
}