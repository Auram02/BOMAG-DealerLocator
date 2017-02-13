<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationsControl.ascx.cs"
    Inherits="Dealer_Locator.admin.Admin.LocationsControl" %>
<br />
<font size="4" style="font-weight: bold;" color="#3D51AA">DISTRIBUTION SETUP</font>
<br />
<br />
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
