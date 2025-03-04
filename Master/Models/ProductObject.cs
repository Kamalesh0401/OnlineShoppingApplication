namespace Master.Models
{
    public class ProductInputObject
    {
        public int? prod_id { get; set; }
        public string? prod_name { get; set; }
        public string? price { get; set; }
        public int? ctgry_id { get; set; }
        public bool? sort_by_price { get; set; }
        public bool? sort_by_invntry { get; set; }

    }
    public class ProductObject
    {
        public bool? act_inact_ind { get; set; }
        public string sort_ordr { get; set; }
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

}
