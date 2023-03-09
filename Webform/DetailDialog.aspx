<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master" AutoEventWireup="true" CodeFile="DetailDialog.aspx.cs" Inherits="DetailDialog" %>
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
            DataKeyNames="Part_Seq" ShowHeaderWhenEmpty="true"
            EmptyDataText="" EnhancePager="True"
            KeepSelectedRows="False" PageSize="20"    
            Width="100%" 
            OnPageIndexChanging="gvMain_PageIndexChanging">
            <EnhancePagerSettings ShowHeaderPager="True"></EnhancePagerSettings>
            <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
            <SelectedRowStyle BackColor="#1f4378" ForeColor="White" />
            <Columns>     
                <asp:TemplateField HeaderText="識別碼" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Part_Seq" runat="server" Text='<%#Bind("Part_Seq") %>'></asp:Label> 
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="料號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Part_No" runat="server" Text='<%#Bind("Part_No") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>              

                <asp:TemplateField HeaderText="品名規格" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_name" runat="server" Text='<%#Bind("part_name") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="預設供應商" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_vendor" runat="server" Text='<%#Bind("part_vendor") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="產品型號" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_type" runat="server" Text='<%#Bind("part_type") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>      
                
                <asp:TemplateField HeaderText="單價" HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="price" runat="server" Text='<%#Bind("price") %>'></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="單位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                                                
                        <asp:Label ID="unit" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="數量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="safe" runat="server" Text='<%#Bind("safe") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </Ede:Grid>
    </div>


        </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>

