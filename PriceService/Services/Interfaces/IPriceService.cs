using Master.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IPriceService
    {
        Task<List<PriceObject>> GetPriceByProductId(SessionInfo sessionInfo, PriceObjectInputObject input);
        Task<OperationStatus> UpdatePriceByProductId(SessionInfo sessionInfo, PriceUpdateObject input);
    }
}
