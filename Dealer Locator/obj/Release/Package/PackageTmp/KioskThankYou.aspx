<%@ Page Language="C#" MasterPageFile="~/DealerLocator.Master" AutoEventWireup="true" CodeBehind="KioskThankYou.aspx.cs" Inherits="Dealer_Locator.KioskThankYou" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
    <div style="font-size:20pt; font-weight:bold;">Thank You!</div><br />
    <br />
    The lead was saved successfully.<br />
    <br />
    You will be automatically redirected to the the kiosk page in 5 seconds.&nbsp; If
    you are not redirected, please <a href="Kiosk.aspx">click here to go there</a></center>
    
    <script type="text/javascript" language="javascript">
    <!--
    
        window.setTimeout('window.location="Kiosk.aspx"; ', 5000);
    
    // -->
    </script>
</asp:Content>
