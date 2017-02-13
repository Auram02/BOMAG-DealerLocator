<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="GenerateFormUrls.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.GenerateSalesLeadForm"
    Title="Dealer Locator Admin - Generate Form Urls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
<font size="4" style="font-weight:bold;" color="#3D51AA">TEMPLATE DEVELOPMENT</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                                                <a href="/admin/FormDevelopment/HeaderFooter.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Header / Footers </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                       <a href="/admin/FormDevelopment/GenerateFormUrls.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            URL Generator </font></a> 
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                         <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                       <a href="/admin/Administration/SalesLeadVendorEmailEditor.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Lead Letter Editor </font></a> 
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br />
    <table cellpadding="3">
        <tr>
            <td style="width: 398px" class="dlHeader">
                Header/Footer</td>
        </tr>
        <tr>
            <td style="width: 398px; background-color: #D4D7EA;" align="center">
                <asp:DropDownList ID="cboHeaderFooter" runat="server" Width="200px" OnSelectedIndexChanged="cboHeaderFooter_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="3">
        <tr>
            <td style="width: 398px" class="dlHeader">
                Sales Lead Form</td>
        </tr>
        <tr>
            <td style="width: 398px; background-color: #D4D7EA;" align="center">
                <asp:DropDownList ID="cboSalesLeadForm" runat="server" Width="200px" OnSelectedIndexChanged="cboSalesLeadForm_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="3">
        <tr>
            <td style="width: 398px" class="dlHeader">
                Url for Locator Form</td>
        </tr>
        <tr>
            <td style="width: 398px; background-color: #D4D7EA;" align="center">
                <asp:TextBox ID="txtLocatorFormUrl" runat="server" Width="389px"></asp:TextBox></td>
        </tr>
    </table>
        <br />
    <br />
    <table cellpadding="3">
        <tr>
            <td style="width: 398px" class="dlHeader">
                Url for Sales Lead Form</td>
        </tr>
        <tr>
            <td style="width: 398px; background-color: #D4D7EA;" align="center">
                <asp:TextBox ID="txtSalesLeadFormUrl" runat="server" Width="389px"></asp:TextBox></td>
        </tr>
    </table>
</asp:Content>
