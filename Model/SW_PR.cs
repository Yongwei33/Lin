using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinHong.Lib.Model
{
    [Table("SW_PR")]
    public class SW_PR
    {
        [Key]
        public int id { get; set; }
        public string form_id { get; set; }
        public int item_no { get; set; }
        public string status { get; set; }
        public string part_no { get; set; }
        public string part_name { get; set; }
        public string part_type { get; set; }
        public string require_date { get; set; }
        public decimal? require_qty { get; set; }
        public decimal? purchased_qty { get; set; }
        public decimal? unpurchased_qty { get; set; }
        public string require_user { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public string remark { get; set; }
        public string buy_unit { get; set; }
        [Computed]
        public decimal? purchased_qty2 { get; set; }
        [Computed]
        public decimal? price { get; set; }
        [Computed]
        public string unit { get; set; }
    }
}
