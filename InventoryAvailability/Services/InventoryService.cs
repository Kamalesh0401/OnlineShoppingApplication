using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;

namespace Master.Services
{
    public class InventoryService : IInventoryService
    {
        #region Constructor
        //public ProductService(IServiceProvider serviceProvider, ILogger<ProductService> logger) { }

        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
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
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

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
            }
            return output;
        }
       
        #endregion

    }
}
