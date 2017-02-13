<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="SalesLeadVendorEmailEditor.aspx.cs" Inherits="Dealer_Locator.admin.SalesLeadVendorEmailEditor"
    Title="Dealer Locator Admin - Distribution Vendor Email" %>

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
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                       <a href="/admin/FormDevelopment/GenerateFormUrls.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            URL Generator </font></a> 
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                         <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                       <a href="/admin/Administration/SalesLeadVendorEmailEditor.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Lead Letter Editor </font></a> 
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <img src="../../admin/Images/ajax-loader.gif" />
            Updating...
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Label ID="lblResult" runat="server"></asp:Label><br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <table class="contentContainer"  cellpadding="3" cellspacing="0">
        <tr align="center">
            <td style="width: 796px">
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 788px">
                            Sales Lead Form Vendor Email Template</td>
                    </tr>
                    <tr>
                        <td style="width: 788px">
                            <asp:TextBox ID="txtVendorEmail" runat="server" Height="439px" TextMode="MultiLine" Width="778px"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                &nbsp;<asp:Button ID="btnSave" runat="server" Height="41px" OnClick="btnSave_Click"
                    Text="Save" Width="128px" /></td>
        </tr>
    </table>
                </ContentTemplate>
                                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
        </asp:UpdatePanel>
    <br />
</asp:Content>
