using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using System.Data;

namespace Master.Services
{
    public class UserService : IUserService
    {
        #region Constructor
        //public ProductService(IServiceProvider serviceProvider, ILogger<ProductService> logger) { }

        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods


        public async Task<OperationStatus> RegisterUser(SessionInfo sessionInfo, UserObject input)
        {
            var output = new OperationStatus();
            try
            {
                output = await _repository.RegisterUserAsync(input);
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }
     
        public async Task<OperationStatus> UpdateAvailabilityByProductId(SessionInfo sessionInfo, UserObject input)
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
