<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ReorderSubCategories.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderSubCategories"
    Title="Dealer Locator Admin - Reorder Sub Categories" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderSubCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Sub Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#eeeeee">
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
    <table  cellpadding="3" cellspacing="0" class="contentContainer">
        <tr>
            <td align="center" style="border-right: medium none; width: 400px" valign="top">
                <center>
                    <table style="border: 1px black solid;" cellspacing="0" cellpadding="0">
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
                                    <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" Width="203px" />
                                    <br />
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="reorderListDemo">
                                                <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="objSubCat"
                                                    PostBackOnReorder="False" SortOrderField="position" CallbackCssStyle="callbackStyle"
                                                    DragHandleAlignment="Left">
                                                    <ItemTemplate>
                                                        <div class="itemArea">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("categoryName") %>'></asp:Label>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </center>
                            </td>
                        </tr>
                    </table>
                    &nbsp;&nbsp;</center>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="objSubCat" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
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
            <asp:ControlParameter ControlID="cboMainCategory" Name="MainCatID" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_subCatID" Type="Int32" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
            <asp:Parameter Name="categoryName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
