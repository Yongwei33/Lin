using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LinHong.Lib.Model
{
    public class PRODUCT_GRIDITEM
    {
        [XmlAttribute]
        public int Part_Seq { get; set; }
        [XmlAttribute]
        public string Part_No { get; set; }
        [XmlAttribute]
        public string part_name { get; set; }
        [XmlAttribute]
        public string part_vendor { get; set; }
        [XmlAttribute]
        public string part_type { get; set; }
        [XmlAttribute]
        public string price { get; set; }
        [XmlAttribute]
        public string safe { get; set; }
        [XmlAttribute]
        public string unit { get; set; }
        [XmlAttribute]
        public string update_time { get; set; }
        [XmlAttribute]
        public string remark { get; set; }
        [Computed]
        [XmlAttribute]
        public string require_qty { get; set; }
    }
}
