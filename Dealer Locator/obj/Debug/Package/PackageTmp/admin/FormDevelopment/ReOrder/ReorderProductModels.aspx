<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ReorderProductModels.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderProductModels"
    Title="Dealer Locator Admin - Reorder Product Models" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="navHeader contentHeader">
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
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderSubCategories.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Reorder Sub Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ProductModels.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Product Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderProductModels.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Reorder Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Default.aspx" style="text-decoration: none;"><font style="text-decoration: none;"
                                            color="#000000" face="arial,sans-serif" size="2">Exit Dashboard </font></a>
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
    <table  cellpadding="3" cellspacing="0" class="contentContainer">
        <tr>
            <td align="center" style="border-right: medium none; width: 400px" valign="top">
                <table style="border: 1px black solid;" cellspacing="0"  cellpadding="3">
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>
                                MAIN CATEGORY</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E0E0E0; width: 228px;">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged"
                                    Width="203px">
                                </asp:DropDownList><br />
                                <br />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>
                                SUB CATEGORY</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E0E0E0; width: 228px;">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboSubCategory" runat="server" AutoPostBack="True" Width="203px">
                                </asp:DropDownList><br />
                                <br />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>
                                REORDER ITEMS ACTION</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #D4D7EA;">
                            <center>
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <div class="reorderListDemo">
                                                <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="objModels"
                                                    PostBackOnReorder="False" SortOrderField="position" CallbackCssStyle="callbackStyle"
                                                    DragHandleAlignment="Left">
                                                    <ItemTemplate>
                                                        <div class="itemArea">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("modelName") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <DragHandleTemplate>
                                                        <div class="dragHandle">
                                                        </div>
                                                    </DragHandleTemplate>
                                                    <ReorderTemplate>
                                                        <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                                    </ReorderTemplate>
                                                </cc1:ReorderList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                            </center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="objModels" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID_SubCatID" TypeName="Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter"
        UpdateMethod="UpdateQuery">
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
            <asp:ControlParameter ControlID="cboSubCategory" Name="subCatID" PropertyName="SelectedValue"
                Type="Int32" />
            <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="-1" Name="mainCatID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
