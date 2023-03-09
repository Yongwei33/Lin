<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="purchaseEditDialog.aspx.cs" Inherits="purchaseEditDialog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  ChildrenAsTriggers="true">
    <ContentTemplate>        
    <table class="PopTable">
        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label3" runat="server" Text="請購單號"></asp:Label>
            </td>
            <td class="PopTableRightTD" style="width:250px">                
                <asp:Label ID="lblform_id" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>

            <td class="PopTableLeftTD">
                <asp:Label ID="Label6" runat="server" Text="項目編號"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:Label ID="lblitem_no" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label5" runat="server" Text="品名規格"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:Label ID="lblpart_name" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label9" runat="server" Text="未採購量"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:Label ID="lblunpurchased_qty" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <span class="reqField">*</span>
                <asp:Label ID="Label8" runat="server" Text="採購單價"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="3">                
                <asp:TextBox ID="price" runat="server" Width="70%"  TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF_price" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="price" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="幣別"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="3">                
                <asp:TextBox ID="currency" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <span class="reqField">*</span>
                <asp:Label ID="Label1" runat="server" Text="採購量"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="3">                
                <asp:TextBox ID="purchased_qty2" runat="server" Width="70%"  TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF_purchased_qty2" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="purchased_qty2" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label4" runat="server" Text="備註"></asp:Label>
            </td>
            <td class="PopTableRightTD" colspan="3">                
                <asp:TextBox ID="remark" runat="server" Width="70%" MaxLength="180" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </td>
        </tr>
    </table>  
    <asp:CustomValidator ID="CV_Form" runat="server" ErrorMessage="" Display="Dynamic"></asp:CustomValidator>
  </ContentTemplate>
</asp:UpdatePanel>      

    <script>
        $(document).ready(function () {
            $("input[type=text]").attr('autocomplete', 'off');
        });
    </script>
</asp:Content>

