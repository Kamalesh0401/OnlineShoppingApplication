using Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace Master.Core
{
    public class BaseController : ControllerBase
    {
        #region Protected Methods

        protected SessionInfo GetSessionInfo()
        {
            return new SessionInfo
            {
                MachineID = this.GetIPAddress(),
                BrowserInfo = this.GetBrowserInfo(),
                //UserID = this.HttpContext.GetUserID()
            };
        }

        #endregion


        private string GetBrowserInfo()
        {
            string result = "";
            try
            {
                StringValues value = default(StringValues);
                if (this.HttpContext != null && this.HttpContext.Request?.Headers?.TryGetValue("User-Agent", out value) == true)
                {
                    return value[0];
                }
            }
            catch
            {
            }

            return result;
        }
        private string GetIPAddress()
        {
            string result = "127.0.0.1";
            try
            {
                IPAddress iPAddress = this.HttpContext.Connection?.RemoteIpAddress;
                if (iPAddress == null)
                {
                    return result;
                }

                if (iPAddress.Equals(IPAddress.IPv6Loopback))
                {
                    return IPAddress.Loopback.ToString();
                }

                return iPAddress.MapToIPv4().ToString();
            }
            catch
            {
            }

            return result;
        }
    }
}
