<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CopyFormControl.ascx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.CopyFormControl" %>
<font size="4" style="font-weight:bold;" color="#3D51AA">FORM DEVELOPMENT</font>
<br /><br />
<asp:Literal ID="litResult" runat="server"></asp:Literal><br />
    <table cellpadding="3">
        <tr>
            <td class="dlHeader" style="width: 398px">
                Base Sales Lead Form</td>
        </tr>
        <tr>
            <td align="center" style="width: 398px; background-color: #d4d7ea">
                <asp:DropDownList ID="cboSalesLeadForm" runat="server" AutoPostBack="True"
                    Width="200px">
                </asp:DropDownList></td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="3">
        <tr>
            <td class="dlHeader" style="width: 398px">
                New Sales Lead Form Name</td>
        </tr>
        <tr>
            <td align="center" style="width: 398px; background-color: #d4d7ea">
                <asp:TextBox ID="txtNewFormName" runat="server" Width="389px"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnCopyForm" runat="server" Height="33px" OnClick="btnCopyForm_Click"
                    Text="Copy Form" Width="113px" /><br />
            </td>
        </tr>
    </table>
    <br />
    <br />