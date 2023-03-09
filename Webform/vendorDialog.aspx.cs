using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using LinHong.Lib.Service;
using System;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class vendorDialog : BasePage
{
    protected RequisitionsService db = new RequisitionsService();
    public string GroupName
    {
        get
        {
            return ViewState["GroupName"] as string;
        }
        set
        {
            ViewState["GroupName"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1OnClick += OKButton1OnClick;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button2Text = "";

        if (!IsPostBack)
        {
            this.GroupName = Request["GroupName"];
            BindControl();
        }
    }

    void DisplayMessage(string msg)
    {
        CV_Form.IsValid = false;
        CV_Form.ErrorMessage = msg;
    }

    private void OKButton1OnClick()
    {        
        if (gvMain.SelectedIndex < 0)
        {
            DisplayMessage("尚未選取資料");
            return;
        }

        var row = gvMain.Rows[gvMain.SelectedIndex];
        if (row == null) return;
        var xe = new XElement("V_SW_VENDOR");
        if (row != null)
        {
            Label vendor_no = row.FindControl("vendor_no") as Label; 
            Label vendor_name = row.FindControl("vendor_name") as Label;
            Label tel_no = row.FindControl("tel_no") as Label;
            Label fax_no = row.FindControl("fax_no") as Label;
            Label contact_person = row.FindControl("contact_person") as Label;
            Label shipping_method = row.FindControl("shipping_method") as Label;
            Label payment_term = row.FindControl("payment_term") as Label;
            Label address = row.FindControl("address") as Label;
            var xe1 = new XElement("Item",
                    new XAttribute("vendor_no", vendor_no.Text),
                    new XAttribute("vendor_name", vendor_name.Text),
                    new XAttribute("tel_no", tel_no.Text),
                    new XAttribute("fax_no", fax_no.Text),
                    new XAttribute("contact_person", contact_person.Text),
                    new XAttribute("shipping_method", shipping_method.Text),
                    new XAttribute("payment_term", payment_term.Text),
                    new XAttribute("address", address.Text)
            );
            xe.Add(xe1);
            Dialog.SetReturnValue2(xe.ToString());
        }
        Dialog.Close(this);
    }

    protected void btnKey_Click(object sender, EventArgs e)
    {        
        BindControl();
    }

    private void BindControl()
    {
        var list = db.getVendor(txtKey.Text, this.GroupName);
        gvMain.DataSource = list;
        gvMain.DataBind();
    }

    protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMain.PageIndex = e.NewPageIndex;
        BindControl();
    }
}