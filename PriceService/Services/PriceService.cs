using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;

namespace Master.Services
{
    public class PriceService : IPriceService
    {
        #region Constructor
        //public ProductService(IServiceProvider serviceProvider, ILogger<ProductService> logger) { }

        private readonly IPriceRepository _repository;

        public PriceService(IPriceRepository repository)
        {
            _repository = repository;
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
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

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
            }
            return output;
        }
       
        #endregion

    }
}
