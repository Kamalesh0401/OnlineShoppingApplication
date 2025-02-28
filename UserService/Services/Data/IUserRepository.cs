// IProductRepository.cs
using Master.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Master.Services.Data
{
    public interface IUserRepository
    {
        Task<OperationStatus> RegisterUserAsync(UserObject input);
        Task<OperationStatus> LoginUserAsync(string userID, string userPassword);
    }
}