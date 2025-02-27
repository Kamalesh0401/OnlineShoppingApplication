using Master.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IInventoryService
    {
        Task<List<InventoryObject>> GetAvailabilityByProductId(SessionInfo sessionInfo, InventoryInputObject input);
        Task<OperationStatus> UpdateAvailabilityByProductId(SessionInfo sessionInfo, InventoryObject input);
    }
}
