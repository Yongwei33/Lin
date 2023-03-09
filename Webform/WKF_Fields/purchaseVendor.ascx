<%@ Control Language="C#" AutoEventWireup="true" CodeFile="purchaseVendor.ascx.cs" Inherits="WKF_OptionalFields_purchaseVendor" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

    <style>
        .PopTable > tbody > tr > td[class='left'] {
            text-align: left;
        }
        .PopTable > tbody > tr > td[class='pcenter'] {
            text-align: center;
            background-color:whitesmoke;
        }
        .PopTableLeftTD {            
            white-space:nowrap;
        }

        .reqField {
            color :red;
        }

        .hideMe {
            display:none;
        }

        html body .RadInput_Metro .riDisabled, html body .RadInput_Disabled_Metro ,select:disabled, input:disabled, textarea:disabled{
            opacity: 1;
            color: #000000;        
            cursor:default;
        }     
        .searchButton {
            float: right;
            display:inline;
        }

    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <table class="PopTable">
                <tr>
                    <td class="PopTableLeftTD">
                        <asp:Label ID="lblVendor" runat="server" Text="選擇廠商"></asp:Label>
                    </td>
                    <td class="PopTableRightTD" colspan="5">
                        <telerik:RadButton ID="btnVendor" runat="server" Text="查詢" CausesValidation="false" UseSubmitBehavior="false" Width="60px" OnClick="btnVendor_Click">
                            <Icon PrimaryIconUrl="~/Common/Images/Icon/icon_m39.png" />
                        </telerik:RadButton>
                    </td>
                    </tr>

                <tr>
                    <td class="PopTableLeftTD" runat="server" id="tdNo1">
                        <asp:Label ID="lblVendorNo" runat="server" Text="廠商編號"></asp:Label>
                    </td>
                    <td class="PopTableRightTD" runat="server" id="tdNo2">
                        <asp:TextBox ID="vendor_no" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF_vendor_no" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="vendor_no"></asp:RequiredFieldValidator>
                    </td>

                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label5" runat="server" Text="廠商名稱"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="vendor_name" runat="server"></asp:TextBox>
                        <br /><asp:RequiredFieldValidator ID="RF_vendor_name" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="vendor_name"></asp:RequiredFieldValidator>
                    </td>
                
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label1" runat="server" Text="電話"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="tel_no" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_tel_no" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="tel_no"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label2" runat="server" Text="傳真"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="fax_no" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_fax_no" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="fax_no"></asp:RequiredFieldValidator>
                    </td>
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label3" runat="server" Text="聯絡人員"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="contact_person" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_contact_person" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="contact_person"></asp:RequiredFieldValidator>
                    </td>
                
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label4" runat="server" Text="運輸方式"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="shipping_method" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_shipping_method" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="shipping_method"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label6" runat="server" Text="付款條件"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="payment_term" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_payment_term" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="payment_term"></asp:RequiredFieldValidator>
                     </td>
                    <td class="PopTableLeftTD">
                        <asp:Label ID="Label7" runat="server" Text="地址"></asp:Label>
                    </td>
                    <td class="PopTableRightTD">
                        <asp:TextBox ID="address" runat="server"></asp:TextBox>
                         <br /><asp:RequiredFieldValidator ID="RF_address" runat="server" ErrorMessage="此為必填欄位" Display="Dynamic" ControlToValidate="address"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <asp:CustomValidator ID="CV_Message" runat="server" Display="Dynamic"></asp:CustomValidator>
        </ContentTemplate>
    </asp:UpdatePanel>
<asp:LinkButton ID="lnk_Edit" runat="server" onclick="lnk_Edit_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_EditResource1">[修改]</asp:LinkButton>
<asp:LinkButton ID="lnk_Cannel" runat="server" onclick="lnk_Cannel_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_CannelResource1">[取消]</asp:LinkButton>
<asp:LinkButton ID="lnk_Submit" runat="server" onclick="lnk_Submit_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_SubmitResource1">[確定]</asp:LinkButton>
<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>