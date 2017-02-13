<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="DefaultSalesLeadForm.aspx.cs" Inherits="Dealer_Locator.admin.DefaultSalesLeadForm" Title="Dealer Locator Admin - Default Sales Lead Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<br />
<font size="4" style="font-weight:bold;" color="#3D51AA">FORM DEVELOPMENT</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                                                <a href="/admin/Administration/DefaultSalesLeadForm.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Set Default </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/FormTemplate.aspx?edit=SL" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Form Editor </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/CopyForm.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Form Duplicator </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderMainCategories.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Manage Models </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br /><table id="DeleteGroupTable" runat="server" class="contentContainer">
        <tr>
            <td style="width: 265px">
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Current Default Sales Lead Form</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
    <asp:Label ID="lblCurrentDefaultSalesLeadForm" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <br />
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            New Default Sales Lead Form</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:DropDownList ID="cboNewDefaultSLF" runat="server" AutoPostBack="True" Width="200px">
                            </asp:DropDownList><br />
                            <br />
                            <asp:Button ID="btnSetAsDefault" runat="server" OnClick="btnReAssign_Click" Text="Set As Default"
                                Width="154px" /></td>
                    </tr>
                </table>
                </td>
        </tr>
    </table>
</asp:Content>
