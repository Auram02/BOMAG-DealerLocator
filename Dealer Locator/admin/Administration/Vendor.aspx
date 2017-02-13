<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="Vendor.aspx.cs" Inherits="Dealer_Locator.admin.Admin.Vendor" Title="Dealer Locator Admin - Distribution Vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
        <!-- /* <table class="navHeader contentHeader" cellpadding="3">
            <tr>
                <td>
                    Vendor Information
                 </td>
            </tr>
        </table>*/ -->
<br />
<font size="4" style="font-weight:bold;" color="#3D51AA">DISTRIBUTION SETUP</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                                                <a href="/admin/Administration/Vendor.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Distribution Vendor </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
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
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/Administration/DoNotPlanToBuySetup.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Do Not Plan To Buy </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                        <br /><br />
           <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="200">
        <ProgressTemplate>
            <img src="../../admin/Images/ajax-loader.gif" />
            Updating...
            <br />
        </ProgressTemplate>
        </asp:UpdateProgress>
    
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table cellspacing="2px"  cellpadding="3" class="contentContainer" style="width: 351px">
            <tr>
                <td colspan="2" class="dlHeader" style="height: 19px">
                    Vendor Information
                </td>
            </tr>
            <tr style="background-color:#F0F0F0;">
                <td align="right" style="width: 96px" >
                    Company Name&nbsp;
                </td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtCompanyName" runat="server" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#DDE1E2;">
                <td align="right" style="width: 96px">
                    Address&nbsp;
                </td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtAddress" runat="server" Height="82px"
                        TextMode="MultiLine" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#F0F0F0;">
                <td align="right" style="width: 96px">
                    City&nbsp;
                </td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtCity" runat="server" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#DDE1E2;">
                <td align="right" style="width: 96px">
                    State&nbsp;
                </td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtState" runat="server" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#F0F0F0;">
                <td align="right" style="width: 96px">
                    Zip &nbsp;</td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtZip" runat="server" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#DDE1E2;">
                <td align="right" style="width: 96px">
                    Email &nbsp;
                </td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtEmail" runat="server" Width="228px"></asp:TextBox></td>
            </tr>
            <tr style="background-color:#F0F0F0;">
                <td align="right" style="width: 96px">
                    Website &nbsp;</td>
                <td style="width: 243px">
                    <asp:TextBox ID="txtWebsite" runat="server"
                        Width="228px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <br />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Update" Width="126px" /><br />
                
                </td>
            </tr>
        </table>
        
                    </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
        </asp:UpdatePanel>


        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
