using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LinHong.Lib.Model
{
    public class PR_GRIDITEM
    {
        [XmlAttribute]
        public int id { get; set; }
        [XmlAttribute]
        public string form_id { get; set; }
        [XmlAttribute]
        public int item_no { get; set; }
        [XmlAttribute]
        public string status { get; set; }
        [XmlAttribute]
        public string part_no { get; set; }
        [XmlAttribute]
        public string part_name { get; set; }
        [XmlAttribute]
        public string part_type { get; set; }
        [XmlAttribute]
        public string require_date { get; set; }
        [XmlAttribute]
        public string require_qty { get; set; }
        [XmlAttribute]
        public string purchased_qty { get; set; }
        [XmlAttribute]
        public string unpurchased_qty { get; set; }
        [XmlAttribute]
        public string require_user { get; set; }
        [XmlAttribute]
        public string create_time { get; set; }
        [XmlAttribute]
        public string update_time { get; set; }
        [XmlAttribute]
        public string remark { get; set; }
        [Computed]
        [XmlAttribute]
        public string purchased_qty2 { get; set; }
        [Computed]
        [XmlAttribute]
        public string price { get; set; }
        [Computed]
        [XmlAttribute]
        public string unit { get; set; }
        [Computed]
        [XmlAttribute]
        public string currency { get; set; }
    }
}
