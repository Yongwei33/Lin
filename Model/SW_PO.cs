using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHong.Lib.Model
{
    [Table("SW_PO")]
    public class SW_PO
    {
        [Key]
        public int id { get; set; }
        public string form_id { get; set; }
        public int item_no { get; set; }
        public string status { get; set; }
        public string po_date { get; set; }
        public decimal? purchased_qty { get; set; }
        public decimal? price { get; set; }
        public string require_user { get; set; }
        public string currency { get; set; }
        public string pr_form_id { get; set; }
        public int? pr_item_no { get; set; }
        public string vendor_no { get; set; }
        public string vendor_name { get; set; }
        public string tel_no { get; set; }
        public string fax_no { get; set; }
        public string address { get; set; }
        public string contact_person { get; set; }
        public string shipping_method { get; set; }
        public string payment_term { get; set; }
        public string create_time { get; set; }
    }
}
