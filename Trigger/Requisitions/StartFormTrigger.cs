using Ede.Uof.Utility.Log;
using Ede.Uof.WKF.ExternalUtility;
using LinHong.Lib.Model;
using LinHong.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinHong.Lib.Trigger.Requisitions
{
    public class StartFormTrigger : ICallbackTriggerPlugin
    {
        public string GetFormResult(ApplyTask applyTask)
        {
            RequisitionsService service = new RequisitionsService();
            int item_no = 1;
            var formDoc = applyTask.Task.CurrentDocument;
            string purchaseNum = formDoc.Fields["purchaseNum"].FieldValue;
            string needDay = formDoc.Fields["needDay"].FieldValue;
            string buy_unit = formDoc.Fields["buy_unit"].FieldValue;
            //外卦欄位
            var detail = formDoc.Fields["purchaseDetail"];
            string detailFieldXml = detail.FieldValue;
            XElement detailForm = XElement.Parse(detailFieldXml);
            //Logger.Write("SW_InfoLog", string.Format("xml:{0}", detailForm));

            foreach (XElement row in detailForm.Elements())
            {
                var form = new SW_PR();
                form.form_id = purchaseNum;
                form.item_no = item_no;
                form.status = "0";
                form.require_user = applyTask.Task.Applicant.UserGUID;
                if (DateTimeOffset.TryParse(needDay, out DateTimeOffset require_date))
                {
                    form.require_date = require_date.ToString("yyyyMMdd");
                }
                form.create_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                form.update_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                form.part_no = GetAttrValue(row, "Part_No");
                form.part_name = GetAttrValue(row, "part_name");
                form.part_type = GetAttrValue(row, "part_type");
                form.require_qty = Convert.ToDecimal(GetAttrValue(row, "require_qty"));
                form.purchased_qty = 0;
                form.unpurchased_qty = Convert.ToDecimal(GetAttrValue(row, "require_qty"));
                form.remark = GetAttrValue(row, "remark");
                form.buy_unit = buy_unit;
                item_no++;

                service.SaveRequisitionData(form);
            }
            
            Logger.Write("SW_InfoLog", string.Format("{0}申請起單:{1}", applyTask.Task.FormName, applyTask.FormNumber));

            return "";
        }

        string GetAttrValue(XElement section, string attrName)
        {
            if (section == null) return "";
            var attr = section.Attribute(attrName);
            return attr != null ? attr.Value : "";
        }

        public void OnError(Exception errorException)
        {
            Logger.Write("SW_ErrorLog", errorException.ToString());
        }
        public void Finally()
        {
           
        }
    }
}
