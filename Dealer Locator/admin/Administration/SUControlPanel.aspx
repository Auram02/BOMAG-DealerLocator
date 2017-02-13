<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="SUControlPanel.aspx.cs" Inherits="Dealer_Locator.admin.SUControlPanel"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div style="background-color: #224179; color: #ffffff;">
        <asp:Literal ID="litResult" runat="server"></asp:Literal></div>
    <br />
    <br />
    <br />
    &nbsp;<asp:Button ID="btnSendPendingLeads" runat="server" OnClick="btnSendPendingLeads_Click"
        Text="Send Pending Leads" />
    <br />
    <br />
    <br />
    <br />
    Send XML File<br />
    <asp:FileUpload ID="FileUpload1" runat="server" /><br />
    <asp:Button ID="btnUploadXMLFile" runat="server"
        Text="Upload and Send Leads" OnClick="btnUploadXMLFile_Click" />
</asp:Content>
