using Microsoft.AspNetCore.Mvc;
using Master.Services.Interfaces;
using Master.Models;
using Common.Models;
using Common.Core;
using System.ComponentModel.DataAnnotations;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


        [HttpPost("RegisterUser")]
        public async Task<OperationStatus> RegisterUser([Required] string usr_id, [Required] string usr_name, [Required] string email, [Required] string pass_word, [Required] string usr_role)
        {
            return await this._service.RegisterUser(this.GetSessionInfo(), usr_id, usr_name, email, pass_word, usr_role).ConfigureAwait(false);
        }

        [HttpGet("LoginUser")]
        public async Task<OperationStatus<string>> LoginUser(string userID, string userPassword)
        {
            return await this._service.LoginUser(this.GetSessionInfo(), userID, userPassword).ConfigureAwait(false);
        }

    }
}
