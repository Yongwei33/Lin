using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ede.Uof.WKF.Design;
using System.Collections.Generic;
using Ede.Uof.WKF.Utility;
using Ede.Uof.EIP.Organization.Util;
using Ede.Uof.WKF.Design.Data;
using Ede.Uof.WKF.VersionFields;
using System.Xml;
using System.Linq;
using LinHong.Lib.Model;
using LinHong.Lib.Service;
using System.Dynamic;
using Ede.Uof.Utility.Page.Common;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;

public partial class WKF_OptionalFields_purchaseDetail : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
{
    protected RequisitionsService db = new RequisitionsService();
    bool gridEnabled;

    #region ==============公開方法及屬性==============
    //表單設計時
    //如果為False時,表示是在表單設計時
    private bool m_ShowGetValueButton = true;
    public bool ShowGetValueButton
    {
        get { return this.m_ShowGetValueButton; }
        set { this.m_ShowGetValueButton = value; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
		//這裡不用修改
		//欄位的初始化資料都到SetField Method去做
        SetField(m_versionField);
    }    

    /// <summary>
    /// 外掛欄位的條件值
    /// </summary>
    public override string ConditionValue
    {
        get
        {
			//回傳字串
			//此字串的內容將會被表單拿來當做條件判斷的值
			return String.Empty;
        }
    }

    /// <summary>
    /// 是否被修改
    /// </summary>
    public override bool IsModified
    {
        get
        {
			//請自行判斷欄位內容是否有被修改
			//有修改回傳True
			//沒有修改回傳False
            //若實作產品標準的控制修改權限必需實作
            //一般是用 m_versionField.FieldValue (表單開啟前的值)
            //      和this.FieldValue (當前的值) 作比對
			return false;
        }
    }

    /// <summary>
    /// 查詢顯示的標題
    /// </summary>
    public override string DisplayTitle
    {
        get
        {
			//表單查詢或WebPart顯示的標題
			//回傳字串
            return String.Empty;
        }
    }

    /// <summary>
    /// 訊息通知的內容
    /// </summary>
    public override string Message
    {
        get
        {
			//表單訊息通知顯示的內容
			//回傳字串
            return String.Empty;
        }
    }


    /// <summary>
    /// 真實的值
    /// </summary>
    public override string RealValue
    {
        get
        {
            //回傳字串
			//取得表單欄位簽核者的UsetSet字串
            //內容必須符合EB UserSet的格式
			return String.Empty;
        }
        set
        {
			//這個屬性不用修改
            base.m_fieldValue = value;
        }
    }


    /// <summary>
    /// 欄位的內容
    /// </summary>
    public override string FieldValue
    {
        get
        {
            //回傳字串
            //取得表單欄位填寫的內容
            XElement xe = GetFormValue();
            return xe.ToString();
        }
        set
        {
			//這個屬性不用修改
            base.m_fieldValue = value;
        }
    }

    /// <summary>
    /// 是否為第一次填寫
    /// </summary>
    public override bool IsFirstTimeWrite
    {
        get
        {
            //這裡請自行判斷是否為第一次填寫
            //若實作產品標準的控制修改權限必需實作
            //實作此屬性填寫者可修改也才會生效
            //一般是用 m_versionField.Filler == null(沒有記錄填寫者代表沒填過)
            //      和this.FieldValue (當前的值是否為預設的空白) 作比對
            return false;
        }
        set
        {
            //這個屬性不用修改
            base.IsFirstTimeWrite = value;
        }
    }

    /// <summary>
    /// 設定元件狀態
    /// </summary>
    /// <param name="Enabled">是否啟用輸入元件</param>
    public void EnabledControl(bool Enabled)
    {
        btnDetail.Enabled = Enabled;
        gvMain.Columns[10].Visible = Enabled;
    }

    /// <summary>
    /// 顯示時欄位初始值
    /// </summary>
    /// <param name="versionField">欄位集合</param>
    public override void SetField(Ede.Uof.WKF.Design.VersionField versionField)
    {
        FieldOptional fieldOptional = versionField as FieldOptional;

        if (fieldOptional != null)
        {

            //若有擴充屬性，可以用該屬性存取
            // fieldOptional.ExtensionSetting

            
            //草稿
            if(!fieldOptional.IsAudit)
            {
                if(fieldOptional.HasAuthority)
                {
                    //有填寫權限的處理
                    EnabledControl(true);
                }
                else
                {
                    //沒填寫權限的處理
                    EnabledControl(false);
                }
            }
            else
            {
                //己送出

                //有填過
                if(fieldOptional.Filler != null)
                {
                    //判斷填寫的站點和當前是否相同
                    if(base.taskObj != null && base.taskObj.CurrentSite != null &&
                        base.taskObj.CurrentSite.SiteId == fieldOptional.FillSiteId && fieldOptional.Filler.UserGUID == Ede.Uof.EIP.SystemInfo.Current.UserGUID)
                    {
                        //判斷填寫權限
                        if (fieldOptional.HasAuthority)
                        {
                            //有填寫權限的處理
                            EnabledControl(true);
                        }
                        else
                        {
                            //沒填寫權限的處理
                            EnabledControl(false);
                        }
                    }
                    else
                    {
                        //判斷修改權限
                        if (fieldOptional.AllowModify)
                        {
                            //有修改權限的處理
                            EnabledControl(true);
                        }
                        else
                        {
                            //沒修改權限的處理
                            EnabledControl(false);
                        }

                    }
                }
                else
                {
                    //判斷填寫權限
                    if (fieldOptional.HasAuthority)
                    {
                        //有填寫權限的處理
                        EnabledControl(true);
                    }
                    else
                    {
                        //沒填寫權限的處理
                        EnabledControl(false);
                    }

                }
            }



            switch(fieldOptional.FieldMode)
            {
                case FieldMode.Applicant:
                case FieldMode.ReturnApplicant:
                    EnabledControl(true);
                    gridEnabled = true;
                    SetFormValue();
                    break;
                default:
                    //觀看和列印都需作沒有權限的處理
                    EnabledControl(false);
                    gridEnabled = false;
                    break;

            }
            
            #region ==============屬性說明==============『』
			//fieldOptional.IsRequiredField『是否為必填欄位,如果是必填(True),如果不是必填(False)』
			//fieldOptional.DisplayOnly『是否為純顯示,如果是(True),如果不是(False),一般在觀看表單及列印表單時,屬性為True』
			//fieldOptional.HasAuthority『是否有填寫權限,如果有填寫權限(True),如果沒有填寫權限(False)』
			//fieldOptional.FieldValue『如果已有人填寫過欄位,則此屬性為記錄其內容』
			//fieldOptional.FieldDefault『如果欄位有預設值,則此屬性為記錄其內容』
			//fieldOptional.FieldModify『是否允許修改,如果允許(fieldOptional.FieldModify=FieldModifyType.yes),如果不允許(fieldOptional.FieldModify=FieldModifyType.no)』
			//fieldOptional.Modifier『如果欄位有被修改過,則Modifier的內容為EBUser,如果沒有被修改過,則會等於Null』
            #endregion

            //#region ==============如果沒有填寫權限時,就要顯示有填寫權限人員的清單,只要把以下註解拿掉即可==============
            //if (!fieldOptional.HasAuthority『是否有填寫權限)
            //{
            //    string strItemName = String.Empty;
            //    Ede.Uof.EIP.Organization.Util.UserSet userSet = ((FieldOptional)versionField).FieldControlData;

            //    for (int i = 0; i < userSet.Items.Count; i++)
            //    {
            //        if (i == userSet.Items.Count - 1)
            //        {
            //            strItemName += userSet.Items[i].Name;
            //        }
            //        else
            //        {
            //            strItemName += userSet.Items[i].Name + "、";
            //        }
            //    }

            //    lblHasNoAuthority.ToolTip = lblAuthorityMsg.Text + "：" + strItemName;
            //}
            //#endregion

            #region ==============如果有修改，要顯示修改者資訊==============
            if (fieldOptional.Modifier != null)
            {
                lblModifier.Visible = true;
                lblModifier.ForeColor = System.Drawing.Color.FromArgb(0x52, 0x52, 0x52);
                lblModifier.Text = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(fieldOptional.Modifier.Name, true);
            }
            #endregion
            BindData(fieldOptional);
        }
    }

    void SetButtonLink()
    {
        ExpandoObject param1 = new
        {
        }.ToExpando();
        Dialog.Open2(btnDetail, "~/CDS/LinHong/purchaseDialog.aspx", "請購單項目", 1300, 900, Dialog.PostBackType.AfterReturn, param1);
    }

    private void SetFormValue()
    {
        if (!IsPostBack)
        {
            var data = new List<PR_GRIDITEM>();
            BindGrid(data);
            SetButtonLink();
        }
    }

    private void BindData(FieldOptional fieldOptional)
    {
        if (!IsPostBack)
        {
            string fieldValueXml = fieldOptional.FieldValue;// 目前DB存的值(第一次為空)

            if (!String.IsNullOrEmpty(fieldValueXml))
            {
                //從草稿開單時..
                this.SetFormValue(fieldValueXml);
            }
        }
    }

    private void SetFormValue(string fieldValueXml)
    {
        if (!string.IsNullOrEmpty(fieldValueXml))
        {
            var data = new List<PR_GRIDITEM>();
            XElement xe = XElement.Parse(fieldValueXml);
            foreach (XElement row in xe.Elements())
            {
                data.Add(this.Xml2Object(row));
            }
            BindGrid(data);
        }
    }

    PR_GRIDITEM Xml2Object(XElement row)
    {
        PR_GRIDITEM obj = new PR_GRIDITEM()
        {
            form_id = row.Attribute("form_id").Value,
            item_no = Convert.ToInt32(row.Attribute("item_no").Value),
            part_name = row.Attribute("part_name").Value,
            unit = row.Attribute("unit").Value,
            price = row.Attribute("price").Value,
            currency = row.Attribute("currency").Value,
            require_date = row.Attribute("require_date").Value,
            unpurchased_qty = row.Attribute("unpurchased_qty").Value,
            purchased_qty2 = row.Attribute("purchased_qty2").Value,
        };
        return obj;
    }

    public XElement GetFormValue()
    {
        XElement rootEl = new XElement("requisitionDetail");
        foreach (GridViewRow row in gvMain.Rows)
        {
            var item = Row2Object(row);
            var el = ToXElement<PR_GRIDITEM>(item);
            rootEl.Add(el);
        }
        return rootEl;
    }

    PR_GRIDITEM Row2Object(GridViewRow row)
    {
        Label form_id = row.FindControl("form_id") as Label;
        Label item_no = row.FindControl("item_no") as Label;
        Label part_name = row.FindControl("part_name") as Label;
        Label unit = row.FindControl("unit") as Label;
        Label price = row.FindControl("price") as Label;
        Label currency = row.FindControl("currency") as Label;
        Label require_date = row.FindControl("require_date") as Label;
        Label unpurchased_qty = row.FindControl("unpurchased_qty") as Label;
        Label purchased_qty2 = row.FindControl("purchased_qty2") as Label;
        Label remark = row.FindControl("remark") as Label;
        PR_GRIDITEM obj = new PR_GRIDITEM()
        {
            form_id = form_id.Text,
            item_no = Convert.ToInt32(item_no.Text),
            part_name = part_name.Text,
            unit = unit.Text,
            price = price.Text,
            currency = currency.Text,
            require_date = require_date.Text,
            unpurchased_qty = unpurchased_qty.Text,
            purchased_qty2 = purchased_qty2.Text,
            remark = remark.Text
        };
        return obj;
    }

    private void BindGrid(List<PR_GRIDITEM> data)
    {
        //資料填入SESSION
        Session["PR_GRIDITEM"] = data;

        gvMain.DataSource = data;
        gvMain.DataBind();
    }

    public List<PR_GRIDITEM> GetRowData()
    {
        var gvData = new List<PR_GRIDITEM>();
        foreach (GridViewRow row in gvMain.Rows)
        {
            gvData.Add(this.Row2Object(row));
        }
        return gvData;
    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*if (!gridEnabled)
        {
            //gvMain.Columns[10].Visible = false;
            //e.Row.Cells[8].Style["display"] = "none";
        }*/
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // edit            
            var row = e.Row;
            LinkButton lbtnDelete = (LinkButton)row.FindControl("lbtnDelete");
            LinkButton lbtnEdit = (LinkButton)row.FindControl("lbtnEdit");
            Label purchased_qty2 = row.FindControl("purchased_qty2") as Label;
            Label currency = row.FindControl("currency") as Label;
            Label price = row.FindControl("price") as Label;
            Label remark = row.FindControl("remark") as Label;

            PR_GRIDITEM dataItem = e.Row.DataItem as PR_GRIDITEM;
            lbtnDelete.Attributes["onclick"] = string.Format("return deleteConfirm('{0}');", dataItem.form_id + " " + dataItem.item_no);

            ExpandoObject param2 = new
            {
                form_id = dataItem.form_id,
                item_no = dataItem.item_no,
                part_name = dataItem.part_name,
                price = dataItem.price,
                currency = dataItem.currency,
                unpurchased_qty = dataItem.unpurchased_qty,
                purchased_qty2 = dataItem.purchased_qty2,
                remark = dataItem.remark
            }.ToExpando();
            Dialog.Open2(lbtnEdit, "~/CDS/LinHong/purchaseEditDialog.aspx", "修改", 850, 700, Dialog.PostBackType.AfterReturn, param2);
        }
    }

    protected void purchased_qty2_TextChanged(object sender, EventArgs e)
    {
        var totalRow = this.GetRowData();
        foreach (GridViewRow row in gvMain.Rows)
        {
            Label form_id = row.FindControl("form_id") as Label;
            Label item_no = row.FindControl("item_no") as Label;
            Label unpurchased_qty = row.FindControl("unpurchased_qty") as Label;
            TextBox purchased_qty2 = row.FindControl("purchased_qty2") as TextBox;
            if (!string.IsNullOrEmpty(purchased_qty2.Text))
            {
                if (Convert.ToDecimal(purchased_qty2.Text) > Convert.ToDecimal(unpurchased_qty.Text))
                    DisplayMessage("請購單 " + form_id.Text + " 項目 " + item_no.Text + " 採購量大於未採購量");
                else
                {
                    foreach (var o in totalRow)
                    {
                        if (o.form_id == form_id.Text && o.item_no == Convert.ToInt32(item_no.Text))
                        {
                            o.purchased_qty2 = purchased_qty2.Text;
                            break;
                        }
                    }
                }
            }
        }
    }

    protected void currency_TextChanged(object sender, EventArgs e)
    {
        var totalRow = this.GetRowData();
        foreach (GridViewRow row in gvMain.Rows)
        {
            Label form_id = row.FindControl("form_id") as Label;
            Label item_no = row.FindControl("item_no") as Label;
            TextBox currency = row.FindControl("currency") as TextBox;
            if (!string.IsNullOrEmpty(currency.Text))
            {
                foreach (var o in totalRow)
                {
                    if (o.form_id == form_id.Text && o.item_no == Convert.ToInt32(item_no.Text))
                    {
                        o.currency = currency.Text;
                        break;
                    }
                }
            }
        }
    }

    #region ==============修改權限LinkButton的事件==============
    protected void lnk_Edit_Click(object sender, EventArgs e)
    {
		//這裡還要加入控制項的隱藏或顯示

        lnk_Cannel.Visible = true;
        lnk_Edit.Visible = false;
        lnk_Submit.Visible = true;
    }
    protected void lnk_Cannel_Click(object sender, EventArgs e)
    {
		//這裡還要加入控制項的隱藏或顯示

        SetField(m_versionField);

        lnk_Cannel.Visible = false;
        lnk_Edit.Visible = true;
        lnk_Submit.Visible = false;
    }
    protected void lnk_Submit_Click(object sender, EventArgs e)
    {
		//這裡還要加入控制項的隱藏或顯示

        lnk_Cannel.Visible = false;
        lnk_Edit.Visible = true;
        lnk_Submit.Visible = false;
		
		//儲存表單資料
		if (base._IFieldOOServer.Count == 0) return;
        ((IFieldCompetenceServer)base._IFieldOOServer[0]).SaveForm();
    }
    #endregion
    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        if (!String.IsNullOrEmpty(e.CommandArgument as string))
        {
            string form = e.CommandArgument.ToString().Split(',')[0].Trim();
            string item = e.CommandArgument.ToString().Split(',')[1].Trim();
            if (!string.IsNullOrEmpty(form) && !string.IsNullOrEmpty(item))
            {
                var data = this.GetRowData();
                foreach (var row in data)
                {
                    if (row.form_id == form && row.item_no == Convert.ToInt32(item))
                    {
                        data.Remove(row);
                        break;
                    }
                }
                BindGrid(data);
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        string dialogRtnVal = Dialog.GetReturnValue();
        if (!dialogRtnVal.StartsWith("<SW_PR")) return;
        var totalRow = this.GetRowData();
        XElement el = XElement.Parse(dialogRtnVal);
        var gridItem = el.Element("Item");
        if (gridItem != null)
        {
            foreach (GridViewRow row in gvMain.Rows)
            {
                string form_id = GetAttrValue(gridItem, "form_id");
                string item_no = GetAttrValue(gridItem, "item_no");
                foreach (var o in totalRow)
                {
                    if (o.form_id == form_id && o.item_no == Convert.ToInt32(item_no))
                    {
                        o.price = GetAttrValue(gridItem, "price");
                        o.currency = GetAttrValue(gridItem, "currency");
                        o.purchased_qty2 = GetAttrValue(gridItem, "purchased_qty2");
                        o.remark = GetAttrValue(gridItem, "remark");
                        break;
                    }
                }
            }
            BindGrid(totalRow);
        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        string dialogRtnVal = Dialog.GetReturnValue();
        if (!dialogRtnVal.StartsWith("<SW_PR")) return;
        var totalRow = this.GetRowData();
        XElement el = XElement.Parse(dialogRtnVal);
        var gridItem = el.Element("Item");
        if (gridItem != null)
        {
            if (!totalRow.Exists(d => d.form_id == GetAttrValue(gridItem, "form_id") && d.item_no == Convert.ToInt32(GetAttrValue(gridItem, "item_no"))))
            {
                var detail = db.getOneRequisition(GetAttrValue(gridItem, "form_id"), GetAttrValue(gridItem, "item_no"));
                if (detail != null)
                {
                    totalRow.Add(detail);
                }
                BindGrid(totalRow);
            }
            else
            {
                DisplayMessage("該項目已選購");
            }
            SetButtonLink();
        }
    }

    string GetAttrValue(XElement section, string attrName)
    {
        if (section == null) return "";
        var attr = section.Attribute(attrName);
        return attr != null ? attr.Value : "";
    }

    protected XElement ToXElement<T>(object obj)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (TextWriter streamWriter = new StreamWriter(memoryStream))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, obj);
                return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
            }
        }
    }

    void DisplayMessage(string msg)
    {
        CV_Message.IsValid = false;
        CV_Message.ErrorMessage = msg;
    }
}