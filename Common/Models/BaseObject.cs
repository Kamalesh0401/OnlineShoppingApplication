using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class OperationStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class OperationStatus<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class SessionInfo
    {
        public string MachineID { get; set; }
        public string BrowserInfo { get; set; }
        public string UserID { get; set; }
    }

}
