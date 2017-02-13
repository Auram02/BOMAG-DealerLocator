<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="RepMap.aspx.cs" Inherits="Dealer_Locator.admin.RepMap" Title="Dealer Locator Admin - Representative Map" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <center>        
    <!-- /* <table class="navHeader contentHeader" cellpadding="3">
            <tr>
                <td>
                    Representative Map
                    </td>
            </tr>
        </table> */ -->
<br />
<font size="4" style="font-weight:bold;" color="#3D51AA">ACCOUNT SETUP</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>

                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Administration/Users.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            User Account </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                        <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Administration/Groups.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Group Accounts </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <!--Button -->
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/Administration/RepMap.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Representative Map </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br /><table cellpadding="3" cellspacing="2" class="contentContainer" style="width: 351px">
        <tr>
            <td class="dlHeader" colspan="2" style="height: 19px">
                Representative Mapping</td>
        </tr>
        <tr style="background-color: #f0f0f0">
            <td style="width: 177px">
                User Login</td>
            <td style="width: 243px">
                Representative</td>
        </tr>
        <tr style="background-color: #dde1e2">
            <td style="width: 177px">
                <asp:DropDownList ID="cboUser" runat="server" Width="146px" OnSelectedIndexChanged="cboUser_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 243px">
                <asp:DropDownList ID="cboRep" runat="server" 
                    Width="208px">
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td align="center" colspan="2">
                <br />
                <asp:Button ID="btnUpdateMapping" runat="server" Height="28px" Text="Update Mapping" Width="115px" OnClick="btnUpdateMapping_Click"  /><br />
                <br />
                <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    </center>
</asp:Content>
