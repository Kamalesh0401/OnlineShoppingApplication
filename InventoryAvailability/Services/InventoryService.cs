using Master.Models;
using Common.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;
using System.Text.Json;
using Common.Logging;

namespace Master.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly ILogger<InventoryService> _logger;
        #region Constructor
        public InventoryService(IInventoryRepository repository, ILogger<InventoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #endregion

        #region Public Methods


        public async Task<List<InventoryObject>> GetAvailabilityByProductId(SessionInfo sessionInfo, InventoryInputObject input)
        {
            var output = new List<InventoryObject>();
            try
            {
                output = await _repository.GetAvailabilityByProductIdAsync(input);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(_logger, ex, $"Session Info: {sessionInfo?.UserID}, Input :{JsonSerializer.Serialize(input)}");
            }
            return output;
        }

        public async Task<OperationStatus> UpdateAvailabilityByProductId(SessionInfo sessionInfo, InventoryObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.UpdateAvailabilityByProductIdAsync(input);
            }
            catch (Exception ex)
            {
                output.IsSuccess = false;
                output.Message = "Something went wrong";
                LogHelper.LogError(_logger, ex, $"Session Info: {sessionInfo?.UserID}, Input :{JsonSerializer.Serialize(input)}");
            }
            return output;
        }

        #endregion

    }
}
