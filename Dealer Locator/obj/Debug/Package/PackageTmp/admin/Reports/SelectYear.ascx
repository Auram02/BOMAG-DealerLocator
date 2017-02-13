<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectYear.ascx.cs" Inherits="Dealer_Locator.admin.Reports.SelectYear" %>
<table cellpadding="3" cellspacing="0" class="contentContainer">
    <tr style="background-color: #e1e1e1" valign="top">
        <td style="width: 478px">
            Select Year</td>
    </tr>
    <tr>
        <td style="width: 478px">
            <br />
            Year:
            <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" >
            </asp:DropDownList><br />
            &nbsp;</td>
    </tr>
</table>
