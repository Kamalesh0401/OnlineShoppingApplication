using Master.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IUserService
    {
        Task<OperationStatus> RegisterUser(SessionInfo sessionInfo, UserObject input);
        Task<OperationStatus<string>> LoginUser(SessionInfo sessionInfo, string userID, string userPassword);
    }
}
