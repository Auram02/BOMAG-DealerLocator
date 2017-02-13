<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralTMReport.ascx.cs"
    Inherits="Dealer_Locator.admin.Reports.GeneralTMReport" %>

<table cellpadding="3" cellspacing="0" class="contentContainer" width="600px">
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
<br />
<table cellpadding="3" cellspacing="0" class="contentContainer" width="600px">
    <tr style="background-color: #e1e1e1" valign="top">
        <td style="width: 478px">
            TM Year-to-Date Totals
        </td>
    </tr>
    <tr>
        <td style="width: 478px" align="center">
            &nbsp;
            <asp:Table ID="tblTM" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                GridLines="Both" Width="300px">
            </asp:Table>
        </td>
    </tr>
</table>
<br />
&nbsp;
<table cellpadding="3" cellspacing="0" class="contentContainer" width="600px">
    <tr style="background-color: #e1e1e1" valign="top">
        <td style="width: 478px">
            Monthly Breakdown
        </td>
    </tr>
    <tr>
        <td style="width: 478px;" align="center">
            <br />
            <asp:Table ID="tblMonthlyBreakdown" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" GridLines="Both" Width="300px">
            </asp:Table>
            &nbsp;
        </td>
    </tr>
</table>
<br />
