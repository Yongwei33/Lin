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

public partial class purchaseEditDialog : BasePage
{
    protected RequisitionsService db = new RequisitionsService();

    public string form_id
    {
        get
        {
            return ViewState["form_id"] as string;
        }
        set
        {
            ViewState["form_id"] = value;
        }
    }

    public string item_no
    {
        get
        {
            return ViewState["item_no"] as string;
        }
        set
        {
            ViewState["item_no"] = value;
        }
    }

    public string unpurchased_qty
    {
        get
        {
            return ViewState["unpurchased_qty"] as string;
        }
        set
        {
            ViewState["unpurchased_qty"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1OnClick += OKButton1OnClick;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button2Text = "";

        if (!IsPostBack)
        {
            this.form_id = Request["form_id"];
            this.item_no = Request["item_no"];
            this.unpurchased_qty = Request["unpurchased_qty"];
            lblform_id.Text = this.form_id;
            lblitem_no.Text = this.item_no;
            lblpart_name.Text = Request["part_name"];
            lblunpurchased_qty.Text = this.unpurchased_qty;
            price.Text = Request["price"];
            currency.Text = Request["currency"];
            purchased_qty2.Text = Request["purchased_qty2"];
            remark.Text = Request["remark"];
        }
    }

    private void OKButton1OnClick()
    {
        if (Convert.ToDecimal(purchased_qty2.Text) > Convert.ToDecimal(this.unpurchased_qty))
        {
            DisplayMessage("採購量大於未採購量");
        }
        else
        {
            try
            {
                var xe = new XElement("SW_PRODUCT");
                var xe1 = new XElement("Item",
                        new XAttribute("form_id", this.form_id),
                        new XAttribute("item_no", this.item_no),
                        new XAttribute("price", price.Text),
                        new XAttribute("currency", currency.Text),
                        new XAttribute("purchased_qty2", purchased_qty2.Text),
                        new XAttribute("remark", remark.Text)
                );
                xe.Add(xe1);
                Dialog.SetReturnValue2(xe.ToString());
                Dialog.Close(this);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                Logger.Write("", ex.ToString());
            }
        }   
    }

    void DisplayMessage(string msg)
    {
        CV_Form.IsValid = false;
        CV_Form.ErrorMessage = msg;        
    }
}