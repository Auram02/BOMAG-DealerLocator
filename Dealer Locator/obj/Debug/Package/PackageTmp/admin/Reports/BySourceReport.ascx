<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BySourceReport.ascx.cs"
    Inherits="Dealer_Locator.admin.Reports.BySourceReport" %>
<table cellpadding="3" cellspacing="0" class="contentContainer">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label><br />
    <br />
    <tr style="background-color: #e1e1e1" valign="top">
        <td style="width: 478px">
            Select Year
        </td>
    </tr>
    <tr>
        <td style="width: 478px">
            <br />
            Year:
            <asp:DropDownList ID="cboYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboYear_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            &nbsp;
        </td>
    </tr>
</table>
<br />
<asp:Table ID="tblLeadsBySource" runat="server" BorderColor="Black" BorderStyle="Solid"
    BorderWidth="1px" CellPadding="4">
</asp:Table>
