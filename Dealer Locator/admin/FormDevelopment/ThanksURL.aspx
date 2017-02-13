<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ThanksURL.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ThanksURL"
    Title="Dealer Locator Admin - Thanks URL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <table class="navHeader contentHeader" cellpadding="3">
            <tr>
                <td>
                    FORM DASHBOARD: <asp:Literal ID="litFormName" runat="server" />
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
                        <asp:LinkButton ID="lnkUrl" runat="server" OnClick="lnkUrl_Click" Font-Names="arial; sans serif" ForeColor="black" Font-Size="10pt" Font-Underline="False">Link to Form</asp:LinkButton>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ThanksUrl.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Thank You URL </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/FormElements.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderFormElements.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderElementValues.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Element Values </font></a>
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
    <table border="1"  cellpadding="3" cellspacing="0">
        <tr >
            <td class="dlHeader" style="text-align:center;">
                THANK YOU URL
            </td>
            <td class="dlHeader" style="text-align:center;">
            FORM NAME
            </td>
            <td class="dlHeader" style="width: 67px; text-align:center">
            ACTION
            </td>
        </tr>
        <tr style="background-color:#D4D7EA;">
            <td align="center">
                &nbsp;<asp:TextBox ID="txtURL" runat="server" Width="400px"></asp:TextBox>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblFormName" runat="server"></asp:Label></td>
            <td align="center" style="width: 67px">
                &nbsp;<asp:LinkButton ID="lnkUpdate" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Update</asp:LinkButton>&nbsp;
            </td>
        </tr>
    </table>
    
    <asp:Label ID="lblResult" runat="server" Width="174px"></asp:Label>
    
</asp:Content>
