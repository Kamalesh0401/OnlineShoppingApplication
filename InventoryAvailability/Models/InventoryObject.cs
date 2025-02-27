namespace Master.Models
{

    public class InventoryInputObject
    {
        public int? prod_id { get; set; }
        public string? price { get; set; }
        public int? stock_aval { get; set; }

    }

    public class InventoryObject
    {
        public int prod_id { get; set; }
        public string prod_name { get; set; }
        public string brand { get; set; }
        public string prod_desc { get; set; }
        public string price { get; set; }
        public string currency { get; set; }
        public int ctgry_id { get; set; }
        public int stock_aval { get; set; }
        public string ctgry_name { get; set; }
        public string mod_by_usr_cd { get; set; }
        public string mod_dttm { get; set; }

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
