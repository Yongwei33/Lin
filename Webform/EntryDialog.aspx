<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="EntryDialog.aspx.cs" Inherits="EntryDialog" %>

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
                <span class="reqField">*</span>
                <asp:Label ID="Label8" runat="server" Text="料號"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="part_no" runat="server" Width="70%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF_part_no" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="part_no" Display="Dynamic"></asp:RequiredFieldValidator>
                <telerik:RadButton ID="btnDetail" runat="server" Text="查詢" CausesValidation="false" UseSubmitBehavior="false" Width="60px" OnClick="btnDetail_Click">
                    <Icon PrimaryIconUrl="~/Common/Images/Icon/icon_m39.png" />
                </telerik:RadButton>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <span class="reqField">*</span>
                <asp:Label ID="Label1" runat="server" Text="品名規格"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="part_name" runat="server" Width="70%"></asp:TextBox>  
                <asp:RequiredFieldValidator ID="RF_part_name" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="part_name" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <span class="reqField">*</span>
                <asp:Label ID="Label6" runat="server" Text="產品型號"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="part_type" runat="server" Width="70%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF_part_type" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="part_type" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label2" runat="server" Text="預設供應商"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="part_vendor" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label3" runat="server" Text="單價"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="price" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label4" runat="server" Text="單位"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="unit" runat="server" Width="70%"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="PopTableLeftTD">
                <asp:Label ID="Label5" runat="server" Text="安全數量"></asp:Label>
            </td>
            <td class="PopTableRightTD">                
                <asp:TextBox ID="safe" runat="server" Width="70%"></asp:TextBox>
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

