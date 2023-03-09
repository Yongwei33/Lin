<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="purchaseDialog.aspx.cs" Inherits="purchaseDialog" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>

    <div style="padding-top:3px">
        <asp:Label ID="Label1" runat="server" Text="搜尋日期" Width="75px"></asp:Label>
        <telerik:RadDatePicker runat="server" ID="start_date" Width="150px"></telerik:RadDatePicker>
        <asp:Label ID="Label15" runat="server" Text="~"></asp:Label>
        <telerik:RadDatePicker runat="server" ID="end_date" Width="150px"></telerik:RadDatePicker>
        <br /><br /><asp:Label ID="Label3" runat="server" Text="請購單位" Width="75px"></asp:Label>
        <asp:RadioButtonList runat="server" ID="buy_unit" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Text="總務課" Value="總務課"></asp:ListItem>
            <asp:ListItem Text="資材課" Value="資材課"></asp:ListItem>
        </asp:RadioButtonList>
        <br /><br /><asp:Label ID="Label2" runat="server" Text="關鍵字搜尋" Width="75px"></asp:Label>
        <asp:TextBox ID="txtKey" runat="server" Width="50%" placeholder="請輸入關鍵字"></asp:TextBox>
        <asp:Button ID="btnKey" runat="server" Text="搜尋" OnClick="btnKey_Click"/>
        <asp:CustomValidator ID="CV_Form" runat="server" Display="Dynamic"></asp:CustomValidator>
        <br/>
        <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>        
        <Ede:Grid ID="gvMain" runat="server" 
            AutoGenerateCheckBoxColumn="false"
            AutoGenerateSelectButton ="true"
            AllowSorting="false" AllowPaging="true" AutoGenerateColumns="false"
            DataKeyOnClientWithCheckBox="True" DefaultSortDirection="Ascending"
            DataKeyNames="item_no" ShowHeaderWhenEmpty="true"
            EmptyDataText="" EnhancePager="True"
            KeepSelectedRows="False" PageSize="20"    
            style="word-break:break-all; word-wrap:normal; width:100%;" 
            OnPageIndexChanging="gvMain_PageIndexChanging">
            <EnhancePagerSettings ShowHeaderPager="True"></EnhancePagerSettings>
            <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
            <SelectedRowStyle BackColor="#1f4378" ForeColor="White" />
            <Columns>    
                <asp:TemplateField HeaderText="請購單單號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="form_id" runat="server" Text='<%#Bind("form_id") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="請購單位" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="buy_unit" runat="server" Text='<%#Bind("buy_unit") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="項目編號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="item_no" runat="server" Text='<%#Bind("item_no") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="料號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_no" runat="server" Text='<%#Bind("part_no") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>              

                <asp:TemplateField HeaderText="品名規格" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_name" runat="server" Text='<%#Bind("part_name") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="產品型號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_type" runat="server" Text='<%#Bind("part_type") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>      
                
                <asp:TemplateField HeaderText="未採買量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="unpurchased_qty" runat="server" Text='<%#Bind("unpurchased_qty") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="需求日期" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="require_date" runat="server" Text='<%#Bind("require_date") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="備註" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <asp:Label ID="remark" runat="server" Text='<%#Bind("remark") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </Ede:Grid>
    </div>


        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

