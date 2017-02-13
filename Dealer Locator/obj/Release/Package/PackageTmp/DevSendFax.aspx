<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DevSendFax.aspx.cs" Inherits="Dealer_Locator.DevSendFax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtFaxNumber" runat="server">8006086479@mhsfax.com</asp:TextBox>
        <asp:Button ID="btnSendFax" runat="server" Text="Send Fax" OnClick="btnSendFax_Click" /></div>
    </form>
</body>
</html>
