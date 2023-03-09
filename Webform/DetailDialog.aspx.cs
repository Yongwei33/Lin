using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using LinHong.Lib.Service;
using System;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class DetailDialog : BasePage
{
    protected RequisitionsService db = new RequisitionsService();

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1OnClick += OKButton1OnClick;
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button2Text = "";

        if (!IsPostBack)
        {
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
        var xe = new XElement("SW_PRODUCT");
        if (row != null)
        {
            Label Part_Seq = row.FindControl("Part_Seq") as Label; 
            Label Part_No = row.FindControl("Part_No") as Label;
            Label part_name = row.FindControl("part_name") as Label;
            Label part_type = row.FindControl("part_type") as Label;
            var xe1 = new XElement("Item",
                    new XAttribute("Part_Seq", Part_Seq.Text),
                    new XAttribute("Part_No", Part_No.Text),
                    new XAttribute("part_name", part_name.Text),
                    new XAttribute("part_type", part_type.Text)
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
        var list = db.getProduct(txtKey.Text);
        gvMain.DataSource = list;
        gvMain.DataBind();
    }

    protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMain.PageIndex = e.NewPageIndex;
        BindControl();
    }
}