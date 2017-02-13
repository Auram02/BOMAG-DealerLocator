<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ReorderMainCategories.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderMainCategories"
    Title="Dealer Locator Admin - Reorder Main Categories" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        </asp:ScriptManagerProxy>
    <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                Reorder Main Categories
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
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderMainCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Main Categories </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
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
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderProductModels.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Reorder Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
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

        <table   cellpadding="3" cellspacing="0" class="contentContainer">
            <tr>
                <td style="width: 400px; border-right: none;" valign="top" align="center">
                    <center>
                        &nbsp;<br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <div class="reorderListDemo">
                                        <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="ObjectDataSource1"
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
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        &nbsp;

                    </center>
                </td>
                <td valign="middle" style="border-left: none;">
                    <br />
                    <br />
                </td>
            </tr>
        </table>
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter"
                                            UpdateMethod="UpdatePosition">
                                            <UpdateParameters>
                                                <asp:Parameter Name="position" Type="Int32" />
                                                <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                            </UpdateParameters>
                                            <InsertParameters>
                                                <asp:Parameter Name="pk_mainCatID" Type="Int32" />
                                                <asp:Parameter Name="categoryName" Type="String" />
                                                <asp:Parameter Name="disable" Type="Boolean" />
                                                <asp:Parameter Name="position" Type="Int32" />
                                            </InsertParameters>
                                        </asp:ObjectDataSource>

</asp:Content>
