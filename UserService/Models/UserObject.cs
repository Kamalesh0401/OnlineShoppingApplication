namespace Master.Models
{

    public class UserObject
    {
        public string? usr_id { get; set; }
        public string? usr_name { get; set; }
        public string? email { get; set; }
        public string? pass_word { get; set; }
        public string? usr_role { get; set; }
        public string? created_at { get; set; }

    }

    public class SessionInfo
    {
        public string MachineID { get; set; }
        public string BrowserInfo { get; set; }
    }

    public class OperationStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class OperationStatus<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
