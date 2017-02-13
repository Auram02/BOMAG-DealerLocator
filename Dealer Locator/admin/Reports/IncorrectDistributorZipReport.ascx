<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncorrectDistributorZipReport.ascx.cs" Inherits="Dealer_Locator.admin.Reports.IncorrectDistributorZipReport" %>
<table border="0" cellpadding="5">
        <tr>
            <td align="left">
                This report will display any distributors that<br />
                1) Have a Shipping Zip Code that does not appear in this system's ZipCode database.&nbsp;
                This means that the distributor has a valid Shipping City Name, but not Zip Code.<br />
                <br />
                To alleviate this problem:
                <br />
                1) Correct the Shipping Zip Code for the distributor so it matches the Zip Code
                for their Shipping City<br />
                2) If no suggestions appear, please re-run the Distributor City Report (Shipping).&nbsp;
                They should appear there with instructions on how to fix them<br />
                3) Re-Import the DDA Database
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:GridView ID="gvDistributorZipReport" runat="server" Font-Names="Arial" Font-Size="Small"
        HeaderStyle-BackColor="#bec8d1">
    </asp:GridView>
    <asp:Label ID="lblNoErrors" runat="server" Font-Bold="true" Font-Size="20pt" Text="No Errors Found"
        Visible="False"></asp:Label>
    <br />