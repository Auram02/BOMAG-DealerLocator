<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncorrectDistributorCityReport.ascx.cs"
    Inherits="Dealer_Locator.admin.Reports.IncorrectDistributorCityReport" %>
<table border="0" cellpadding="5">
    <tr>
        <td align="left">
            This report will display any distributors that<br />
            1) Have a Shipping City Name that is not in this system's ZipCode database<br />
            2) Have a correctly spelled Shipping City Name that is present somewhere in the
            United States, but is not located within the Shipping Zip Code specified.&nbsp;
            This may occur if more than one state has the same City Name, such as Springfield,
            MO and Springfield, IL.<br />
            <br />
            To alleviate this problem:
            <br />
            1) Correct the spelling of the Distributor's Shipping City Name<br />
            2) Confirm that the correct Shipping State was specified for each Shipping City
            Name / Shipping Zip Code specified<br />
            3) Re-Import the DDA Database
        </td>
    </tr>
</table>
<br />
<asp:GridView ID="gvDistributorCityReport" runat="server" Font-Names="Arial" Font-Size="Small"
    HeaderStyle-BackColor="#bec8d1">
</asp:GridView>
<asp:Label ID="lblNoErrors" runat="server" Text="No Errors Found" Visible="False"
    Font-Size="20pt" Font-Bold="true"></asp:Label><br />
