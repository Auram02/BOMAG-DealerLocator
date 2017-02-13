<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormTemplateControl.ascx.cs"
    Inherits="Dealer_Locator.admin.FormDevelopment.FormTemplateControl" %>
<font size="4" style="font-weight: bold;" color="#3D51AA">FORM DEVELOPMENT</font>
<br />
<br />
<table style="border: 1px black solid;" cellspacing="0" cellpadding="7">
    <tr>
        <td>
            <center>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="objFormTemplate"
                    OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_OnRowCommand">
                    <Columns>
                        <asp:BoundField DataField="FormName" HeaderText="Form Name" SortExpression="FORM NAME" />
                        <asp:BoundField DataField="thanksUrl" HeaderText="Thanks Url" SortExpression="thanksUrl" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                        <asp:CheckBoxField DataField="ZipLocator" HeaderText="ZipLocator" SortExpression="ZipLocator" />
                        <asp:BoundField DataField="pk_SLFormID" HeaderText="pk_SLFormID" SortExpression="pk_SLFormID" />
                        <asp:BoundField DataField="fk_groupID" HeaderText="fk_groupID" SortExpression="fk_groupID" />
                        <asp:BoundField DataField="fk_headerID" HeaderText="fk_headerID" SortExpression="fk_headerID" />
                        <asp:BoundField DataField="fk_footerID" HeaderText="fk_footerID" SortExpression="fk_footerID" />
                        <asp:CommandField ShowEditButton="True" HeaderText="Action" />
                        <asp:TemplateField HeaderText="Delete Form">
                            <ItemTemplate>
                                <center>
                                    <asp:LinkButton CommandName="deleteForm" CommandArgument="<%# Container.DataItemIndex %>"
                                        ID="deleteForm" runat="server">Delete</asp:LinkButton></center>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit Data">
                            <ItemTemplate>
                                <center>
                                    <asp:LinkButton CommandName="editData" CommandArgument="<%# Container.DataItemIndex %>"
                                        ID="editData" runat="server">Edit</asp:LinkButton></center>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="gridviewClass_Header " />
                    <RowStyle CssClass="gridviewClass_TableRow" />
                    <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                </asp:GridView>
                <div style="text-align: right;">
                    <asp:LinkButton ID="lnkAddNew" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>&nbsp;&nbsp;</div>
            </center>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="objFormTemplate" runat="server" DeleteMethod="DeleteQuery"
    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByGroupID"
    TypeName="Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter"
    UpdateMethod="UpdateData">
    <DeleteParameters>
        <asp:Parameter Name="pk_SLFormID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="pk_SLFormID" Type="Int32" />
        <asp:Parameter Name="fk_groupID" Type="Int32" />
        <asp:Parameter Name="fk_headerID" Type="Int32" />
        <asp:Parameter Name="fk_footerID" Type="Int32" />
        <asp:Parameter Name="thanksURL" Type="String" />
        <asp:Parameter Name="FormName" Type="String" />
        <asp:Parameter Name="ZipLocator" Type="Boolean" />
        <asp:Parameter Name="CreatedBy" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="pk_SLFormID" Type="Int32" />
        <asp:Parameter Name="fk_groupID" Type="Int32" />
        <asp:Parameter Name="fk_headerID" Type="Int32" />
        <asp:Parameter Name="fk_footerID" Type="Int32" />
        <asp:Parameter Name="thanksUrl" Type="String" />
        <asp:Parameter Name="FormName" Type="String" />
        <asp:Parameter Name="ZipLocator" Type="Boolean" />
    </InsertParameters>
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="GroupID" SessionField="GroupID" Type="Int32" />
        <asp:Parameter Name="IsZipLocator" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:TextBox ID="txtFormID" runat="server" Visible="False" Width="73px"></asp:TextBox><br />
<asp:ObjectDataSource ID="SalesLeadDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataByGroupID" TypeName="Dealer_Locator.DA.SalesLeadFormTDSTableAdapters.DL_SalesLeadFormTableAdapter"
    InsertMethod="Insert">
    <SelectParameters>
        <asp:SessionParameter DefaultValue="0" Name="GroupID" SessionField="GroupID" Type="Int32" />
        <asp:SessionParameter DefaultValue="false" Name="IsZipLocator" SessionField="IsZipLocator"
            Type="Boolean" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="pk_SLFormID" Type="Int32" />
        <asp:Parameter Name="fk_groupID" Type="Int32" />
        <asp:Parameter Name="fk_headerID" Type="Int32" />
        <asp:Parameter Name="fk_footerID" Type="Int32" />
        <asp:Parameter Name="thanksUrl" Type="String" />
        <asp:Parameter Name="FormName" Type="String" />
        <asp:Parameter Name="ZipLocator" Type="Boolean" />
    </InsertParameters>
</asp:ObjectDataSource>
