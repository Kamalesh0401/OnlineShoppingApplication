namespace Master.Models
{
    public class ProductObject
    {
        public bool? act_inact_ind { get; set; }
        public string prod_id { get; set; }
        public string prod_nam { get; set; }
        public string sort_ordr { get; set; }
        public string category { get; set; }
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
}
