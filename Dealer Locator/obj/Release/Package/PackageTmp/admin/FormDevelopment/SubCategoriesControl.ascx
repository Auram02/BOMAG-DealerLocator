<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubCategoriesControl.ascx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.SubCategoriesControl" %>
    <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                Category Dashboard
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="0" cellspacing="0" class="contentContainer">
        <tr align="top">
            <td>
                <table cellpadding="0" cellspacing="0" style="border: 1px solid black;">
                    <tr valign="top" align="center">
                        <td style="height: 30px; background-color: #E1E1E1;">
                            <strong>MAIN CATEGORY:</strong>&nbsp;<asp:DropDownList ID="cboMainCategory" runat="server"
                                Width="228px" AutoPostBack="True" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                DataSourceID="MainCategoryDS" DataTextField="categoryName" DataValueField="pk_mainCatID">
                            </asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr valign="top">
                        <td style="width: 375px; height: 154px" align="left">
                            <asp:GridView ID="GridView1" runat="server" Width="390px" AllowSorting="False" AutoGenerateColumns="False"
                                OnRowDataBound="GridView1_RowDataBound" DataSourceID="SubCatTDS" AllowPaging="True"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowEditing="GridView1_RowEditing"
                                OnRowUpdating="GridView1_RowUpdating">
                                <Columns>
                                    <asp:BoundField DataField="pk_subCatID" HeaderText="pk_subCatID" SortExpression="pk_subCatID">
                                        <ControlStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fk_mainCatID" HeaderText="fk_mainCatID" SortExpression="fk_mainCatID">
                                        <ControlStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="categoryName" HeaderText="Sub Category Name" SortExpression="categoryName" />
                                    <asp:CheckBoxField DataField="disable" HeaderText="DISABLE" SortExpression="disable" />
                                    <asp:BoundField DataField="position" HeaderText="position" SortExpression="position">
                                        <ControlStyle Width="0px" />
                                        <ItemStyle Width="0px" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowEditButton="True" HeaderText="ACTION" />
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
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="MainCategoryDS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData_nonDisabled" TypeName="Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SubCatTDS" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID" TypeName="Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter"
        UpdateMethod="UpdateQuery">
        <UpdateParameters>
            <asp:Parameter Name="pk_subCatID" Type="Int32" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
            <asp:Parameter Name="categoryName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="0" Name="MainCatID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_subCatID" Type="Int32" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
            <asp:Parameter Name="categoryName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>