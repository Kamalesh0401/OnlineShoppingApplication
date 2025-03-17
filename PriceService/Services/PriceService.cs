using Common.Logging;
using Common.Models;
using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;
using System.Text.Json;

namespace Master.Services
{
    public class PriceService : IPriceService
    {
        #region Constructor

        private readonly IPriceRepository _repository;
        private readonly ILogger<PriceService> _logger;
        public PriceService(IPriceRepository repository, ILogger<PriceService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        #endregion

        #region Public Methods


        public async Task<List<PriceObject>> GetPriceByProductId(SessionInfo sessionInfo, PriceObjectInputObject input)
        {
            var output = new List<PriceObject>();
            try
            {
                output = await _repository.GetPriceByProductIdAsync(input);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(_logger, ex, $"Session Info: {sessionInfo?.UserID}, Input :{JsonSerializer.Serialize(input)}");

            }
            return output;
        }

        public async Task<OperationStatus> UpdatePriceByProductId(SessionInfo sessionInfo, PriceUpdateObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.UpdatePriceByProductIdAsync(input);
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
