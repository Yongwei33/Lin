using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LinHong.Lib.Model
{
    [Table("SW_PRODUCT")]
    public class SW_PRODUCT
    {
        [ExplicitKey]
        public int Part_Seq { get; set; }
        public string Part_No { get; set; }
        public string part_name { get; set; }
        public string part_vendor { get; set; }
        public string part_type { get; set; }
        public decimal? price { get; set; }
        public decimal? safe { get; set; }
        public string unit { get; set; }
        public string update_time { get; set; }
        [Computed]
        public decimal? require_qty { get; set; }
    }
}
