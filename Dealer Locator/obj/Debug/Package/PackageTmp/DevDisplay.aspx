<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DevDisplay.aspx.cs" Inherits="Dealer_Locator.DevDisplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="75%" style="border:1px solid black;">
                <tr>
                    <td style="background-color:#C5D5FC;">
                    User Email - User Selected "Electronic"</td>
                </tr>
                <tr>
                    <td style="background-color:#FBFBF9;">
                        <asp:Literal ID="litUserEmail" runat="server"></asp:Literal></td>
                </tr>
                                <tr>
                    <td style="background-color:#C5D5FC;">
                    Product Vendor Emails - User Selected "By Mail"</td>
                </tr>
                <tr>
                    <td style="background-color:#FBFBF9;">
                        <asp:Literal ID="litProductVendorEmails" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td style="background-color:#C5D5FC;">
                    Territory Manager Emails - All Products</td>
                </tr>
                <tr>
                    <td style="background-color:#FBFBF9;">
                        <asp:Literal ID="litTerritoryManagerEmails" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td style="background-color:#C5D5FC;">
                        FAXES: To distributors</td>
                </tr>
                <tr>
                    <td style="background-color:#FBFBF9;">
                        <asp:Literal ID="litDistributorFax" runat="server"></asp:Literal></td>
                </tr>
                
            </table>
        </div>
    </form>
</body>
</html>
