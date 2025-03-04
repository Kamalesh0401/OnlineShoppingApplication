using Master.Models;
using Common.Models;
using System.Collections.Generic;

namespace Master.Services.Interfaces
{
    public interface IUserService
    {
        Task<OperationStatus> RegisterUser(SessionInfo sessionInfo, string usr_id, string usr_name, string email, string pass_word, string usr_role);
        Task<OperationStatus<string>> LoginUser(SessionInfo sessionInfo, string userID, string userPassword);
    }
}
