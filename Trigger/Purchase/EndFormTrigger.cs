using Ede.Uof.Utility.Log;
using Ede.Uof.WKF.ExternalUtility;
using LHTech.Lib.Cust2StdGrd.Trigger;
using LinHong.Lib.Model;
using LinHong.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinHong.Lib.Trigger.Purchase
{
    public class EndFormTrigger : ICallbackTriggerPlugin
    {
        public string GetFormResult(ApplyTask applyTask)
        {
            RequisitionsService service = new RequisitionsService();
            var formDoc = applyTask.Task.CurrentDocument;
            //Logger.Write("SW_Purchase", applyTask.Task.CurrentDocXml);
            string purchaseNo = formDoc.Fields["purchaseNo"].FieldValue;
            //外卦欄位
            var detail = formDoc.Fields["requisitionDetail"];
            string detailFieldXml = detail.FieldValue;
            XElement detailForm = XElement.Parse(detailFieldXml);

            if (applyTask.FormResult == Ede.Uof.WKF.Engine.ApplyResult.Reject)
            {
                foreach (XElement row in detailForm.Elements())
                {
                    string form_id = GetAttrValue(row, "form_id");
                    string pr_item_no = GetAttrValue(row, "item_no");
                    Decimal? purchased_qty2 = Convert.ToDecimal(GetAttrValue(row, "purchased_qty2"));

                    var PR = service.geteRequisitionQty(form_id, pr_item_no);
                    Decimal? unpurchased_qty = PR.unpurchased_qty;
                    Decimal? purchased_qty = PR.purchased_qty;
                    unpurchased_qty += purchased_qty2;
                    purchased_qty -= purchased_qty2;

                    service.UpdatePRpurchased_qty(form_id, pr_item_no, purchased_qty, unpurchased_qty);

                    string updateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    service.UpdateRequisitionsTime(form_id, updateTime);
                }
                service.UpdatePOstatus(purchaseNo, "4");
            }
            else if(applyTask.FormResult == Ede.Uof.WKF.Engine.ApplyResult.Adopt)
                service.UpdatePOstatus(purchaseNo, "1");

            Logger.Write("SW_InfoLog", string.Format("{0}申請結案:{1}", applyTask.Task.FormName, applyTask.FormNumber));

            //表單標準明細代碼
            string stdFieldId = "std_requisitionDetail";
            //表單客製明細代碼
            string custFieldId = "requisitionDetail";
            //客製明細XML Cell Element
            string cellElement = "PR_GRIDITEM";
            //客志明細XML Cell Attribute
            List<string> lstCellAttr = new List<string>() { "id", "form_id", "item_no", "part_name", "require_date", "unpurchased_qty", "remark1", "purchased_qty2", "price", "unit", "currency" };

            LH_Event lhEvent = new LH_Event();
            lhEvent.EndFormEvent(applyTask, stdFieldId, custFieldId, cellElement, lstCellAttr);

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
