using Ede.Uof.EIP.Organization.Util;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.Utility.Log;
using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using LinHong.Lib.Service;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class EntryDialog : BasePage
{
    protected RequisitionsService db = new RequisitionsService();

    void SetButtonLink()
    {
        ExpandoObject param1 = new
        {
        }.ToExpando();
        Dialog.Open2(btnDetail, "~/CDS/LinHong/DetailDialog.aspx", "請購項目", 850, 700, Dialog.PostBackType.AfterReturn, param1);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1OnClick += OKButton1OnClick;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button2Text = "";

        if (!IsPostBack)
        {
            SetButtonLink();
        }
    }

    private void OKButton1OnClick()
    {
        try
        {
            var xe = new XElement("SW_PRODUCT");
            var xe1 = new XElement("Item",
                    new XAttribute("Part_No", part_no.Text),
                    new XAttribute("part_name", part_name.Text),
                    new XAttribute("part_type", part_type.Text),
                    new XAttribute("part_vendor", part_vendor.Text),
                    new XAttribute("price", price.Text),
                    new XAttribute("unit", unit.Text),
                    new XAttribute("safe", safe.Text)
            );
            xe.Add(xe1);
            Dialog.SetReturnValue2(xe.ToString());
            Dialog.Close(this);
        }
        catch (Exception ex) {
            DisplayMessage(ex.Message);
            Logger.Write("", ex.ToString());
        }        
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        string dialogRtnVal = Dialog.GetReturnValue();
        if (!dialogRtnVal.StartsWith("<SW_PRODUCT")) return;
        XElement el = XElement.Parse(dialogRtnVal);
        var gridItem = el.Element("Item");
        if (gridItem != null)
        {
            var detail = db.getOneProduct(GetAttrValue(gridItem, "Part_No"));
            if (detail != null)
            {
                part_no.Text = detail.Part_No;
                part_name.Text = detail.part_name;
                part_type.Text = detail.part_type;
                part_vendor.Text = detail.part_vendor;
                price.Text = detail.price;
                unit.Text = detail.unit;
                safe.Text = detail.safe;
            }
        }
    }

    void DisplayMessage(string msg)
    {
        CV_Form.IsValid = false;
        CV_Form.ErrorMessage = msg;        
    }

    string GetAttrValue(XElement section, string attrName)
    {
        if (section == null) return "";
        var attr = section.Attribute(attrName);
        return attr != null ? attr.Value : "";
    }
}