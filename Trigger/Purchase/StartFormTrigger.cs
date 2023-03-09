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

namespace LinHong.Lib.Trigger.Purchase
{
    public class StartFormTrigger : ICallbackTriggerPlugin
    {
        public string GetFormResult(ApplyTask applyTask)
        {
            RequisitionsService service = new RequisitionsService();
            var formDoc = applyTask.Task.CurrentDocument;
            //外卦欄位
            var vendor = formDoc.Fields["purchaseVendor"];
            string vendorFieldXml = vendor.FieldValue;
            XElement vendorForm = XElement.Parse(vendorFieldXml);

            var detail = formDoc.Fields["requisitionDetail"];
            string detailFieldXml = detail.FieldValue;
            XElement detailForm = XElement.Parse(detailFieldXml);
            int item_noPO = 1;

            foreach (XElement row in detailForm.Elements())
            {
                string form_id = GetAttrValue(row, "form_id");
                string item_no = GetAttrValue(row, "item_no");
                Decimal? purchased_qty2 = Convert.ToDecimal(GetAttrValue(row, "purchased_qty2"));

                var PR = service.geteRequisitionQty(form_id, item_no);
                Decimal? unpurchased_qty = PR.unpurchased_qty;
                Decimal? purchased_qty = PR.purchased_qty;
                unpurchased_qty -= purchased_qty2;
                purchased_qty += purchased_qty2;

                service.UpdatePRpurchased_qty(form_id, item_no, purchased_qty, unpurchased_qty);

                string updateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                service.UpdateRequisitionsTime(form_id, updateTime);

                var form = new SW_PO();
                string purchaseNo = formDoc.Fields["purchaseNo"].FieldValue;
                string orderDate = formDoc.Fields["orderDate"].FieldValue;
                form.form_id = purchaseNo;
                form.item_no = item_noPO;
                form.status = "0";
                form.require_user = applyTask.Task.Applicant.UserGUID;
                if (DateTimeOffset.TryParse(orderDate, out DateTimeOffset po_date))
                {
                    form.po_date = po_date.ToString("yyyyMMdd");
                }
                form.create_time = DateTime.Now.ToString("yyyyMMddHHmmss");
                form.vendor_no = GetAttrValue(vendorForm, "vendor_no");
                form.vendor_name = GetAttrValue(vendorForm, "vendor_name");
                form.tel_no = GetAttrValue(vendorForm, "tel_no");
                form.fax_no = GetAttrValue(vendorForm, "fax_no");
                form.address = GetAttrValue(vendorForm, "address");
                form.contact_person = GetAttrValue(vendorForm, "contact_person");
                form.shipping_method = GetAttrValue(vendorForm, "shipping_method");
                form.payment_term = GetAttrValue(vendorForm, "payment_term");
                form.pr_form_id = GetAttrValue(row, "form_id");
                form.pr_item_no = Convert.ToInt32(GetAttrValue(row, "item_no"));
                if(!string.IsNullOrEmpty(GetAttrValue(row, "price")))
                {
                    form.price = Convert.ToDecimal(GetAttrValue(row, "price"));
                }
                form.currency = GetAttrValue(row, "currency");
                form.purchased_qty = Convert.ToDecimal(GetAttrValue(row, "purchased_qty2"));
                item_noPO++;

                service.SavePurchaseData(form);
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
