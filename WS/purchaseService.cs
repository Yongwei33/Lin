using LinHong.Lib.Model;
using LinHong.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

namespace LinHong.Lib.WS
{
    /// <summary>
    /// purchaseService 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
    // [System.Web.Script.Services.ScriptService]
    public class purchaseService : System.Web.Services.WebService
    {
        protected RequisitionsService db = new RequisitionsService();
        public purchaseService()
        {

            //如果使用設計的元件，請取消註解下列一行
            //InitializeComponent(); 
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string getVendors()
        {
            XElement rootEl = new XElement("item");
            /*var data = db.getVendor();
            foreach (var row in data)
            {
                var fieldValue = new XAttribute("fieldValue", row.vendor_seq);
                var fieldText = new XAttribute("fieldText", row.vendor_name);
                rootEl.Add(fieldValue);
                rootEl.Add(fieldText);
            }*/
            return rootEl.ToString();
        }

    }
}