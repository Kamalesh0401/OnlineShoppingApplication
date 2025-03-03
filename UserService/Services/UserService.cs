using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;


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
                var password = Common.Authentication.HashPassword(input.pass_word);
                input.pass_word = password;
                output = await _repository.RegisterUserAsync(input);
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }

        public async Task<OperationStatus<string>> LoginUser(SessionInfo sessionInfo, string userID, string userPassword)
        {
            var output = new OperationStatus<string>();
            try
            {
                OperationStatus<UserObject> result = await _repository.LoginUserAsync(userID, userPassword);

                if (result.Data == null)
                {
                    output.IsSuccess = false;
                    output.Message = "Invalid username or password";
                }
                else
                {
                    UserObject user = result.Data;
                    if (!Common.Authentication.VerifyPassword(userPassword, user.pass_word))
                    {
                        output.IsSuccess = false;
                        output.Message = "Invalid username or password";
                    }
                    else
                    {
                        // Generate JWT
                        var token = Common.Authentication.GenerateJwtToken(user.usr_id, user.usr_name, user.usr_role);
                        output.IsSuccess = true;
                        output.Message = "Login successful";
                        output.Data = token;
                    }
                }
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
