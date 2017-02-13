<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ProductModels.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ProductModels"
    Title="Dealer Locator Admin - Product Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                Category Dashboard
            </td>
        </tr>
    </table>
        <br />
        <div>
        <table border="0"  cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td background="../../../admin/Images/Tabs/ui_tab_middle.gif">
                        <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderMainCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Main Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/SubCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Sub Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderSubCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Sub Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ProductModels.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Product Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderProductModels.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Default.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Exit Dashboard </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <table class="contentContainer"  cellpadding="3" cellspacing="0">
        <tr>
            <td>
                <table style="border: 1px solid black;"  cellpadding="3" cellspacing="0">
                    <tr valign="top" style="background-color: #E1E1E1;">
                        <td style="height: 30px; width: 472px;">
                            MAIN CATEGORY
                            <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" Width="351px" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr valign="top" style="background-color: #E1E1E1;">
                        <td style="height: 30px; width: 472px;">
                            SUB CATEGORY
                            <asp:DropDownList ID="cboSubCategory" runat="server" AutoPostBack="True" Width="351px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr valign="top">
                        <td align="center" style="width: 472px; height: 154px">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="False"
                                AutoGenerateColumns="False" DataSourceID="ModelDS" OnRowDataBound="GridView1_RowDataBound"
                                Width="483px">
                                <Columns>
                                    <asp:BoundField DataField="pk_modelID" HeaderText="pk_modelID" SortExpression="pk_modelID" />
                                    <asp:BoundField DataField="fk_subCatID" HeaderText="fk_subCatID" SortExpression="fk_subCatID" />
                                    <asp:BoundField DataField="modelName" HeaderText="Product Model Name" SortExpression="modelName" />
                                    <asp:CheckBoxField DataField="disable" HeaderText="disable" SortExpression="disable" />
                                    <asp:BoundField DataField="position" HeaderText="position" SortExpression="position" />
                                    <asp:BoundField DataField="modelUrl" HeaderText="Product URL" SortExpression="modelUrl" />
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:BoundField DataField="fk_mainCatID" HeaderText="fk_mainCatID" SortExpression="fk_mainCatID" />
                                </Columns>
                                <HeaderStyle CssClass="gridviewClass_Header " />
                                <RowStyle CssClass="gridviewClass_TableRow" />
                                <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
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
    <asp:ObjectDataSource ID="ModelDS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID_SubCatID" TypeName="Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter"
        UpdateMethod="UpdateQuery" InsertMethod="Insert">
        <UpdateParameters>
            <asp:Parameter Name="fk_subCatID" Type="Int32" />
            <asp:Parameter Name="pk_modelID" Type="Int32" />
            <asp:Parameter Name="modelName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
            <asp:Parameter Name="modelUrl" Type="String" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboSubCategory" DefaultValue="-1" Name="subCatID"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="-1" Name="mainCatID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_modelID" Type="Int32" />
            <asp:Parameter Name="fk_subCatID" Type="Int32" />
            <asp:Parameter Name="modelName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
            <asp:Parameter Name="modelUrl" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
