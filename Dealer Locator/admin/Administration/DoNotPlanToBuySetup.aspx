<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="DoNotPlanToBuySetup.aspx.cs" Inherits="Dealer_Locator.admin.DoNotPlanToBuySetup"
    Title="Dealer Locator Admin - No Buy Plan Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<br />
<font size="4" style="font-weight:bold;" color="#3D51AA">DISTRIBUTION SETUP</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                                                <a href="/admin/Administration/Vendor.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Distribution Vendor </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Administration/RegionManagement.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Regions </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/Administration/DoNotPlanToBuySetup.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Do Not Plan To Buy </font></a>
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
            <img alt="Updating..." src="../../admin/Images/ajax-loader.gif" />
            Updating...
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Label ID="lblResult" runat="server"></asp:Label><br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="3" cellspacing="0" class="contentContainer">
                <tr align="center">
                    <td style="width: 796px">
                        <table cellpadding="3">
                            <tr>
                                <td class="dlHeader" style="width: 788px">
                                    Do Not Plan To Buy Email Address</td>
                            </tr>
                            <tr>
                                <td style="width: 788px" align="center">
                                    <br />
                                    Email Address: <asp:TextBox ID="txtPlanToBuyEmail" runat="server" Height="15px" Width="350px"></asp:TextBox></td>
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
</asp:Content>
