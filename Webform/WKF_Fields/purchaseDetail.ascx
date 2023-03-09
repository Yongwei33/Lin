<%@ Control Language="C#" AutoEventWireup="true" CodeFile="purchaseDetail.ascx.cs" Inherits="WKF_OptionalFields_purchaseDetail" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>

<asp:Label ID="lblDetail" runat="server" Text="未採購項目:"></asp:Label>
<telerik:RadButton ID="btnDetail" runat="server" Text="查詢" CausesValidation="false" UseSubmitBehavior="false" Width="60px" OnClick="btnDetail_Click">
    <Icon PrimaryIconUrl="~/Common/Images/Icon/icon_m39.png" />
</telerik:RadButton>
<br /><asp:CustomValidator ID="CV_Message" runat="server" Display="Dynamic"></asp:CustomValidator><br />

<Ede:Grid ID="gvMain" runat="server" AutoGenerateCheckBoxColumn="False" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
           EmptyDataText="尚未新增細項" PageSize="15" OnRowDataBound="gvMain_RowDataBound" style="word-break:break-all; word-wrap:normal; width:100%;">
    <EnhancePagerSettings ShowHeaderPager="True"></EnhancePagerSettings>
            <Columns>
                <asp:TemplateField HeaderText="請購單號" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="form_id" runat="server" Text='<%#Bind("form_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="項目編號" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="item_no" runat="server" Text='<%#Bind("item_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="品名規格" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_name" runat="server" Text='<%#Bind("part_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="單位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="unit" runat="server" Text='<%#Bind("unit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="採購單價" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                                                
                        <asp:Label ID="price" runat="server" Text='<%#Bind("price")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="幣別" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="currency" runat="server" Text='<%#Bind("currency")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="需求日期" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="require_date" runat="server" Text='<%#Bind("require_date") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="未採購量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="unpurchased_qty" runat="server" Text='<%#Bind("unpurchased_qty") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="採購量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="purchased_qty2" runat="server" Text='<%#Bind("purchased_qty2")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="備註" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="300px">
                    <ItemTemplate>
                        <asp:Label ID="remark" runat="server" Text='<%#Bind("remark")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click" CausesValidation="false">
                            <img id="Img1" src="~/Common/Images/Icon/icon_m02.png" runat="server" style="margin-left: 4px" border="0" />
                            <asp:Label ID="Label1" runat="server" Text="編輯"></asp:Label>
                        </asp:LinkButton>

                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Eval("form_id")+","+Eval("item_no") %>' OnCommand="lbtnDelete_Command" CausesValidation="false">
                            <img id="Img3" src="~/Common/Images/Icon/icon_m03.png" runat="server" style="margin-left: 4px" border="0" />
                            <asp:Label ID="lblDelete" runat="server" Text="刪除"></asp:Label>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </Ede:Grid>
    </ContentTemplate>
</asp:UpdatePanel>

<script>
    function deleteConfirm(msg) {
        return confirm('確定刪除' + msg + '?');
    }
</script>
<asp:LinkButton ID="lnk_Edit" runat="server" onclick="lnk_Edit_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_EditResource1">[修改]</asp:LinkButton>
<asp:LinkButton ID="lnk_Cannel" runat="server" onclick="lnk_Cannel_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_CannelResource1">[取消]</asp:LinkButton>
<asp:LinkButton ID="lnk_Submit" runat="server" onclick="lnk_Submit_Click" Visible="False" CausesValidation="False" meta:resourcekey="lnk_SubmitResource1">[確定]</asp:LinkButton>
<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>