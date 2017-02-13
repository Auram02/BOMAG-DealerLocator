<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupsControl.ascx.cs" Inherits="Dealer_Locator.admin.Admin.GroupsControl" %>
<table class="contentContainer" cellpadding="3">
            <tr align="left">
                <td>
                    <asp:GridView ID="GridView1" runat="server"  AllowPaging="False" AllowSorting="False"
                        AutoGenerateColumns="False" DataSourceID="objGroup" OnRowDataBound="GridView1_OnRowDataBound"    OnRowCommand="GridView1_OnRowCommand" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="pk_groupID" HeaderText="pk_groupID" SortExpression="pk_groupID" />
                            <asp:BoundField DataField="GroupName" HeaderText="GroupName" SortExpression="GroupName" />
                            <asp:CheckBoxField DataField="disable" HeaderText="Disable" SortExpression="disable" />
                            <asp:CommandField ShowEditButton="True" />
                                                                                                <asp:TemplateField HeaderText="Delete Group" >
                            <ItemTemplate><center><asp:LinkButton CommandName="deleteGroup" CommandArgument="<%# Container.DataItemIndex %>" ID="deleteGroup" runat="server" >Delete</asp:LinkButton></center></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        
                                <HeaderStyle CssClass="gridviewClass_Header" />
                                <RowStyle CssClass="gridviewClass_TableRow" />
                                <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                    </asp:GridView>
                <div style="text-align: right;">
                    <asp:LinkButton ID="lnkAddNew" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>&nbsp;&nbsp;</div>
                </td>
            </tr>
        </table>
    </center>
    <center>
        &nbsp;</center>
    <center>
        <table class="contentContainer" runat="server" id="DeleteGroupTable"><tr><td style="width: 265px" cellpadding="4">
            <table cellpadding="3">
                <tr>
                    <td class="dlHeader" style="width: 398px">
                        Delete Group and ReAssign All Users to New Group</td>
                </tr>
                <tr>
                    <td align="center" style="width: 398px; background-color: #d4d7ea">
                        <asp:DropDownList ID="cboGroups" runat="server" AutoPostBack="True"
                            Width="200px">
                        </asp:DropDownList><br />
                        <br />
                        <asp:Button ID="btnReAssign" runat="server" OnClick="btnReAssign_Click" Text="ReAssign To Group"
                            Width="154px" /></td>
                </tr>
            </table>
            <br />
            <table cellpadding="3">
                <tr>
                    <td class="dlHeader" style="width: 398px">
                        Delete Group and All Users</td>
                </tr>
                <tr>
                    <td align="center" style="width: 398px; background-color: #d4d7ea">
                        <asp:Button ID="btnDeleteAllUsers" runat="server" OnClick="btnDeleteAllUsers_Click"
                            Text="Delete All Users" Width="154px" /></td>
                </tr>
            </table>
            <asp:Label ID="lblGroupID" runat="server" Visible="False"></asp:Label></td></tr></table></center>
    <center>
        <asp:ObjectDataSource ID="objGroup" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetData" TypeName="Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter"
            UpdateMethod="UpdateQuery">
            <UpdateParameters>
                <asp:Parameter Name="pk_groupID" Type="Int32" />
                <asp:Parameter Name="GroupName" Type="String" />
            </UpdateParameters>
        </asp:ObjectDataSource>
    </center>