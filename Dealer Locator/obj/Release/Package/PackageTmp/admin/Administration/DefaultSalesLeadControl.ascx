<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DefaultSalesLeadControl.ascx.cs"
    Inherits="Dealer_Locator.admin.Admin.DefaultSalesLeadControl" %>
<br />
<font size="4" style="font-weight: bold;" color="#3D51AA">FORM DEVELOPMENT</font>
<br />
<br />
<table id="DeleteGroupTable" runat="server" class="contentContainer">
    <tr>
        <td style="width: 265px">
            <table cellpadding="3">
                <tr>
                    <td class="dlHeader" style="width: 398px">
                        Current Default Sales Lead Form
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 398px; background-color: #d4d7ea">
                        <asp:Label ID="lblCurrentDefaultSalesLeadForm" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table cellpadding="3">
                <tr>
                    <td class="dlHeader" style="width: 398px">
                        New Default Sales Lead Form
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 398px; background-color: #d4d7ea">
                        <asp:DropDownList ID="cboNewDefaultSLF" runat="server" AutoPostBack="True" Width="200px">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Button ID="btnSetAsDefault" runat="server" OnClick="btnReAssign_Click" Text="Set As Default"
                            Width="154px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
