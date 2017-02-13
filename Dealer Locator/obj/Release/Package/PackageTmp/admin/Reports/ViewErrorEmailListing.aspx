<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ViewErrorEmailListing.aspx.cs" Inherits="Dealer_Locator.admin.Reports.ViewErrorEmailListing"
    Title="Dealer Locator Admin - Error Email Listing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <font size="4" style="font-weight: bold;" color="#3D51AA">ERROR REPORTS</font>
    <br />
    <br />
    <asp:Table ID="tblErrorEmailListing" runat="server" Width="500px" BorderStyle="Solid"
        BorderWidth="1px" GridLines="Both">
    </asp:Table>
</asp:Content>
