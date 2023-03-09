<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptionFieldUC.ascx.cs" Inherits="WKF_OptionalFields_OptionFieldUC" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Ede.Uof.Utility.Component.Grid" Namespace="Ede.Uof.Utility.Component" TagPrefix="Ede" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Label ID="Label3" runat="server" Text="新增明細:"  Width="90"></asp:Label>
<telerik:RadButton ID="btnEntry" runat="server" Text="輸入" CausesValidation="false" UseSubmitBehavior="false" Width="60px" OnClick="btnEntry_Click">
    <Icon PrimaryIconUrl="~/Common/Images/Icon/icon_m02.png" />
</telerik:RadButton>

<br /><asp:CustomValidator ID="CV_Message" runat="server" Display="Dynamic"></asp:CustomValidator><br />

<Ede:Grid ID="gvMain" runat="server" AutoGenerateCheckBoxColumn="False" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
           EmptyDataText="尚未新增細項" PageSize="15" OnRowDataBound="gvMain_RowDataBound" Width="70%">
    <EnhancePagerSettings ShowHeaderPager="True"></EnhancePagerSettings>
            <Columns>
                <asp:TemplateField HeaderText="識別碼" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Part_Seq" runat="server" Text='<%#Bind("Part_Seq") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="料號" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Part_No" runat="server" Text='<%#Bind("Part_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="品名規格" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_name" runat="server" Text='<%#Bind("part_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="產品型號" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="part_type" runat="server" Text='<%#Bind("part_type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="單價" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                                                
                        <asp:Label ID="price" runat="server" Text='<%#Bind("price")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="單位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>                                                
                        <asp:Label ID="unit" runat="server" Text='<%#Bind("unit")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="需求數量" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:TextBox ID="require_qty" runat="server" TextMode="Number" Text='<%#Bind("require_qty")%>' AutoPostBack="true" OnTextChanged="require_qty_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF_require_qty" runat="server" ErrorMessage="此為必填欄位" ControlToValidate="require_qty" Display="Dynamic"></asp:RequiredFieldValidator>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="備註" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="400px">
                    <ItemTemplate>
                        <asp:TextBox ID="remark" runat="server" Text='<%#Bind("remark")%>' AutoPostBack="true" OnTextChanged="remark_TextChanged" TextMode="MultiLine" Rows="3" Width="70%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Eval("Part_No") %>' OnCommand="lbtnDelete_Command" CausesValidation="false">
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