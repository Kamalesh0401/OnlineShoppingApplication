using Master.Models;
using Master.Services.Data;
using Master.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
using System.Text;

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

        #region Private Methods

        private static string GenerateJwtToken(string userId, string userName, string userRole)
        {
            var data = Encoding.UTF8.GetBytes("JsonWebToken@123");
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(data);

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.Name] = userName,
                [ClaimTypes.GroupSid] = userRole,
                [ClaimTypes.Sid] = userId
            };
            var descriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = "MyIssuer",
                Audience = "MyAudience",
                Claims = claims,
                IssuedAt = null,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler();
            handler.SetDefaultTimesOnTokenCreation = false;
            var tokenString = handler.CreateToken(descriptor);
            return tokenString;
        }
        private static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        public static string Base64Encode(string? plainText, bool EncodeRequired)
        {
            if (EncodeRequired)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return plainText;
            }
        }
        #endregion

        #region Public Methods


        public async Task<OperationStatus> RegisterUser(SessionInfo sessionInfo, UserObject input)
        {
            var output = new OperationStatus();
            try
            {
                var password = Base64Encode(input.pass_word,true);
                input.pass_word = password;
                output = await _repository.RegisterUserAsync(input);
            }
            catch (Exception ex)
            {
                //this.Logger.LogError(ex, $"Session Info: {sessionInfo.ToJsonText()}, Input : {input.ToJsonText()}");

            }
            return output;
        }

        public async Task<OperationStatus> LoginUser(SessionInfo sessionInfo, string userID, string userPassword)
        {
            var output = new OperationStatus();
            try
            {
                OperationStatus result = await _repository.LoginUserAsync(userID, userPassword);

                if (result.Data == null)
                {
                    output.IsSuccess = false;
                    output.Message = "Invalid username or password";
                }
                else
                {
                    UserObject user = (UserObject)result.Data;
                    if (!VerifyPassword(userPassword, user.pass_word))
                    {
                        output.IsSuccess = false;
                        output.Message = "Invalid username or password";
                    }
                    else
                    {
                        // Generate JWT
                        var token = GenerateJwtToken(user.usr_id, user.usr_name, user.usr_role);
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
