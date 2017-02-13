<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="HeaderFooter.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.HeaderFooter"
    Title="Dealer Locator Admin - Header / Footer" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                Header / Footer
            </td>
        </tr>
    </table> */ -->
    <br />
<font size="4" style="font-weight:bold;" color="#3D51AA">TEMPLATE DEVELOPMENT</font>
<br /><br />
    <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                                                <a href="/admin/FormDevelopment/HeaderFooter.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Header / Footers </font></a>            </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel1"
        DisplayAfter="200">
        <ProgressTemplate>
            <img src="../../admin/Images/ajax-loader.gif" />
            Updating...
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel1" runat="server">
        <ContentTemplate>
            <table class="contentContainer" cellpadding="3">
                <tr>
                    <td>
                        <table cellpadding="5px">
                            <tr>
                                <td>
                                    <table cellpadding="5px" style="background-color:#E2E1F1;">
                                        <tr>
                                            <td class="dlHeader" style="background-color:#E2E1F1; color:Black;">
                                                Select Header/Footer</td>
                                        </tr>
                                        <tr>
                                            <td align="left" style="width: 520px">
                                                <asp:DropDownList ID="cboHeaderFooter" runat="server" Width="520px" OnSelectedIndexChanged="cboHeaderFooter_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                </asp:DropDownList></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table  cellpadding="5px" style="background-color:#E2E1F1;" >
                                        <tr>
                                            <td class="dlHeader" style="width: 520px; background-color:#E2E1F1; color:Black;">
                                                Header/Footer Form Name</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 520px">
                                                <asp:TextBox ID="txtName" runat="server" Width="512px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table><br />
                                    <table style="background-color:#E2E1F1;"  cellpadding="5px">
                                        <tr>
                                            <td class="dlHeader" style="width: 520px; background-color:#E2E1F1; color:Black;">
                                                Header/Footer Thanks Url</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 520px">
                                                <asp:TextBox ID="txtThanksUrl" runat="server" Width="512px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <div style="font-weight: bold; font-size: 12pt; background-color:#DDE1E2; font-style:italic;">
                                        &lt;html&gt;<br />
                                        &lt;head&gt;<br />
                                    </div>
                                    <br />
                                    <table  cellpadding="5px"  style="width: 520px; background-color:#E2E1F1;" id="TABLE1">
                                        <tr>
                                            <td class="dlHeader" style="height: 18px;  color:Black;background-color:#E2E1F1;">
                                                Custom Scripts and StyleSheets</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtCustomCode" runat="server" Height="200px" TextMode="MultiLine"
                                                    Width="520px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="font-weight: bold; font-size: 12pt; background-color:#DDE1E2;font-style:italic;">
                                        &lt;/head&gt;<br />
                                        &lt;body&gt;<br />
                                    </div>
                                    &nbsp;<table  style="width: 520px; background-color:#E2E1F1;" cellpadding="5px">
                                        <tr>
                                            <td class="dlHeader" style="height: 18px; background-color:#E2E1F1; color:Black;">
                                                Header</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtHeader" runat="server" Height="200px" TextMode="MultiLine" Width="520px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="font-weight: bold; font-size: 12pt; background-color:#DDE1E2;font-style:italic;">
                                        [DYNAMIC FORM CONTENT GOES HERE]<br />
                                    </div>
                                    <br />
                                    <table  style="width: 520px;background-color:#E2E1F1;" cellpadding="5px">
                                        <tr>
                                            <td class="dlHeader" style="width: 520px; background-color:#E2E1F1; color:Black;">
                                                Footer
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 520px">
                                                <asp:TextBox ID="txtFooter" runat="server" Height="200px" TextMode="MultiLine" Width="520px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="font-weight: bold; font-size: 12pt; background-color:#DDE1E2;font-style:italic;">
                                        &lt;/body&gt;<br />
                                        &lt;/html&gt;<br />
                                    </div>
                                    <br />
                                    <br />
                                    <table   style="border:1px solid black;background-color:#E2E1F1;" cellpadding="5px">
                                        <tr>
                                            <td class="dlHeader" style="width: 520px; background-color:#3F52A1; color:white;">
                                                Preview Link</td>
                                        </tr>
                                        <tr align="left">
                                            <td style="width: 520px; background-color:#F0F0F0;">
                                                <asp:LinkButton ID="lnkUrl" runat="server" OnClick="lnkUrl_Click">Header/Footer Preview</asp:LinkButton></td>
                                        </tr>
                                    </table>
                                    <br />
                                                                        <table style="border:1px solid black;background-color:#E2E1F1;" cellpadding="5px">
                                        <tr>
                                            <td class="dlHeader" style="width: 520px; background-color:#3F52A1; color:white;">
                                                Save Header/Footer</td>
                                        </tr>
                                        <tr align="left">
                                            <td style="width: 520px; background-color:#F0F0F0;">
                                                                                    <asp:Button ID="btnSave" runat="server" Height="33px" OnClick="btnSave_Click" Text="Save"
                                        Width="118px" />&nbsp;
                                    <asp:Button ID="btnDelete" runat="server" Height="33px" Text="Delete" Width="88px"
                                        OnClick="btnDelete_Click" />
                                        </tr>
                                    </table>
<br />
                                    &nbsp;Default:
                                    <asp:CheckBox ID="chkDefault" runat="server" /><br />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="inputArea" runat="server">
        <br />
        <br />
        &nbsp; &nbsp;
    </div>
</asp:Content>
