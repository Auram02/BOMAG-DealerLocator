<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="RegionManagement.aspx.cs" Inherits="Dealer_Locator.admin.RegionManagement" Title="Dealer Locator Admin - Region Management" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<br />
<font size="4" style="font-weight:bold;" color="#3D51AA">DISTRIBUTION SETUP</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                                                <a href="/admin/Administration/Vendor.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Distribution Vendor </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/Administration/RegionManagement.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Regions </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Administration/DoNotPlanToBuySetup.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Do Not Plan To Buy </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br />
    <table class="contentContainer" cellpadding="3">
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                    OnRowCommand="GridView1_OnRowCommand" OnRowDataBound="GridView1_OnRowDataBound"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="pk_regionID" HeaderText="pk_regionID" SortExpression="pk_regionID" />
                        <asp:BoundField DataField="RegionName" HeaderText="RegionName" SortExpression="RegionName" />
                        <asp:BoundField DataField="RegionContactName" HeaderText="RegionContactName" SortExpression="RegionContactName" />
                        <asp:BoundField DataField="RegionEmail" HeaderText="RegionEmail" SortExpression="RegionEmail" />
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    </Columns>
                    <RowStyle CssClass="gridviewClass_TableRow" />
                    <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                    <HeaderStyle CssClass="gridviewClass_Header" />
                    <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                </asp:GridView>
                &nbsp;
                <div style="text-align: right">
                    <asp:LinkButton ID="lnkAddNew" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
                    &nbsp;</div>
            </td>
        </tr>
    </table>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeleteQuery"
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                    TypeName="Dealer_Locator.DA.DLTableAdapters.DL_RegionTableAdapter" UpdateMethod="UpdateQueryByRegionID">
                    <DeleteParameters>
                        <asp:Parameter Name="pk_regionID" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="pk_regionID" Type="Int32" />
                        <asp:Parameter Name="RegionName" Type="String" />
                        <asp:Parameter Name="RegionContactName" Type="String" />
                        <asp:Parameter Name="RegionEmail" Type="String" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="pk_regionID" Type="Int32" />
                        <asp:Parameter Name="RegionName" Type="String" />
                        <asp:Parameter Name="RegionContactName" Type="String" />
                        <asp:Parameter Name="RegionEmail" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>

</asp:Content>
