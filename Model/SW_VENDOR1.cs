using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHong.Lib.Model
{
    [Table("SW_VENDOR1")]
    public class SW_VENDOR1
    {
        [Key]
        public int vendor_seq { get; set; }
        public string vendor_no { get; set; }
        public string vendor_name { get; set; }
        public string short_name { get; set; }
        public string tel_no { get; set; }
        public string fax_no { get; set; }
        public string tax_id_no { get; set; }
        public string address { get; set; }
        public string contact_person { get; set; }
        public string shipping_method { get; set; }
        public string payment_term { get; set; }
        public string owner_unit { get; set; }
        public string rem1 { get; set; }
        public string rem2 { get; set; }
        public string rem3 { get; set; }
        public string rem4 { get; set; }
        public string rem5 { get; set; }
        public string rem6 { get; set; }
        public string rem7 { get; set; }
        public string rem8 { get; set; }
    }
}
