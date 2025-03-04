using Master.Models;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IUserRepository
    {
        Task<OperationStatus> RegisterUserAsync(string usr_id, string usr_name, string email, string pass_word, string usr_role);
        Task<OperationStatus<UserObject>> LoginUserAsync(string userID, string userPassword);
    }
}