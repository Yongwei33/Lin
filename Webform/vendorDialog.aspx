<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="vendorDialog.aspx.cs" Inherits="vendorDialog" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>

    <div style="padding-top:3px">
        <asp:Label ID="Label2" runat="server" Text="關鍵字搜尋"></asp:Label>
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
            DataKeyNames="vendor_no" ShowHeaderWhenEmpty="true"
            EmptyDataText="" EnhancePager="True"
            KeepSelectedRows="False" PageSize="20"    
            Width="100%" 
            OnPageIndexChanging="gvMain_PageIndexChanging">
            <EnhancePagerSettings ShowHeaderPager="True"></EnhancePagerSettings>
            <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
            <SelectedRowStyle BackColor="#1f4378" ForeColor="White" />
            <Columns>     
                <asp:TemplateField HeaderText="廠商編號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="vendor_no" runat="server" Text='<%#Bind("vendor_no") %>'></asp:Label> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="廠商名稱" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="vendor_name" runat="server" Text='<%#Bind("vendor_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>              

                <asp:TemplateField HeaderText="電話" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="tel_no" runat="server" Text='<%#Bind("tel_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="傳真" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="fax_no" runat="server" Text='<%#Bind("fax_no") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="聯絡人員" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="contact_person" runat="server" Text='<%#Bind("contact_person") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>      
                
                <asp:TemplateField HeaderText="運輸方式" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="shipping_method" runat="server" Text='<%#Bind("shipping_method") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="付款條件" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                                                
                        <asp:Label ID="payment_term" runat="server" Text='<%#Bind("payment_term")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="地址" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="address" runat="server" Text='<%#Bind("address") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </Ede:Grid>
    </div>


        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

