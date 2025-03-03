using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Master.Core;

namespace Master.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        #region Private Elements

        private readonly IUserService _service;

        #endregion

        #region Constructor

        public UserController(IUserService service)
        {
            this._service = service;
        }
        #endregion

        
        [HttpGet("RegisterUserAsync")]
        public async Task<OperationStatus> RegisterUser([FromQuery] UserObject input)
        {
            return await this._service.RegisterUser(this.GetSessionInfo(), input).ConfigureAwait(false);
        }
        
        [HttpPost("LoginUser")]
        public async Task<OperationStatus<string>> LoginUser(string userID, string userPassword)
        {
            return await this._service.LoginUser(this.GetSessionInfo(), userID, userPassword).ConfigureAwait(false);
        }
        
    }
}
