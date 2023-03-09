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
using LinHong.Lib.Service;
using System.Dynamic;
using Ede.Uof.Utility.Page.Common;
using System.Xml.Linq;
using LinHong.Lib.Model;
using Ede.Uof.EIP.SystemInfo;
using Ede.Uof.EIP.Organization;

public partial class WKF_OptionalFields_purchaseVendor : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
{
    protected RequisitionsService db = new RequisitionsService();


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
        SetButtonLink();
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
        btnVendor.Enabled = Enabled;
        //tdNo1.Visible = false;
        //tdNo2.Visible = false;
        vendor_no.Enabled = false;
        vendor_name.Enabled = false;
        tel_no.Enabled = false;
        fax_no.Enabled = false;
        contact_person.Enabled = false;
        shipping_method.Enabled = false;
        payment_term.Enabled = false;
        address.Enabled = false;
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
                    break;
                default:
                    //觀看和列印都需作沒有權限的處理
                    EnabledControl(false);
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
        GroupUCO groupUCO = new GroupUCO(GroupType.Department);

        if(this.ApplicantGroupId != null)
        {
            ExpandoObject param1 = new
            {
                GroupName = groupUCO.QueryDepartmentName(this.ApplicantGroupId)
            }.ToExpando();
            Dialog.Open2(btnVendor, "~/CDS/LinHong/vendorDialog.aspx", "廠商", 1000, 700, Dialog.PostBackType.AfterReturn, param1);
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
        /*else
        {
            //申請者切換部門或代申請下拉bar時會觸發postback
            if (this.hfAppGroupGuid.Value != this.ApplicantGroupId || this.hfAppUserGuid.Value != this.ApplicantGuid)
            {
                UC_AppUserBaseInfo.InitFormData(this);
                CheckAppRole();
                //SetRequireField(this.AppRole);
            }
        }*/
    }

    public XElement GetFormValue()
    {
        XElement rootEl = new XElement("purchaseVendor"
            , new XAttribute("vendor_no", vendor_no.Text)
            , new XAttribute("vendor_name", vendor_name.Text)
            , new XAttribute("tel_no", tel_no.Text)
            , new XAttribute("fax_no", fax_no.Text)
            , new XAttribute("contact_person", contact_person.Text)
            , new XAttribute("shipping_method", shipping_method.Text)
            , new XAttribute("payment_term", payment_term.Text)
            , new XAttribute("address", address.Text)
        );
        return rootEl;
    }

    private void SetFormValue(string fieldValueXml)
    {
        if (!string.IsNullOrEmpty(fieldValueXml))
        {
            XElement xe = XElement.Parse(fieldValueXml);
            var data = Xml2Object(xe);
            if (data != null)
            {
                Session["purchaseVendor"] = data;
                vendor_no.Text = data.vendor_no;
                vendor_name.Text = data.vendor_name;
                tel_no.Text = data.tel_no;
                fax_no.Text = data.fax_no;
                contact_person.Text = data.contact_person;
                shipping_method.Text = data.shipping_method;
                payment_term.Text = data.payment_term;
                address.Text = data.address;
            }
        }
    }

    SW_VENDOR1 Xml2Object(XElement row)
    {
        SW_VENDOR1 obj = new SW_VENDOR1()
        {
            vendor_no = row.Attribute("vendor_no").Value,
            vendor_name = row.Attribute("vendor_name").Value,
            tel_no = row.Attribute("tel_no").Value,
            fax_no = row.Attribute("fax_no").Value,
            contact_person = row.Attribute("contact_person").Value,
            shipping_method = row.Attribute("shipping_method").Value,
            payment_term = row.Attribute("payment_term").Value,
            address = row.Attribute("address").Value,
        };
        return obj;
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
    protected void btnVendor_Click(object sender, EventArgs e)
    {
        string dialogRtnVal = Dialog.GetReturnValue();
        if (!dialogRtnVal.StartsWith("<V_SW_VENDOR")) return;
        XElement el = XElement.Parse(dialogRtnVal);
        var gridItem = el.Element("Item");
        if (gridItem != null)
        {
            vendor_no.Text = GetAttrValue(gridItem, "vendor_no");
            vendor_name.Text = GetAttrValue(gridItem, "vendor_name");
            tel_no.Text = GetAttrValue(gridItem, "tel_no");
            fax_no.Text = GetAttrValue(gridItem, "fax_no");
            contact_person.Text = GetAttrValue(gridItem, "contact_person");
            shipping_method.Text = GetAttrValue(gridItem, "shipping_method");
            payment_term.Text = GetAttrValue(gridItem, "payment_term");
            address.Text = GetAttrValue(gridItem, "address");
        }
    }

    string GetAttrValue(XElement section, string attrName)
    {
        if (section == null) return "";
        var attr = section.Attribute(attrName);
        return attr != null ? attr.Value : "";
    }
}