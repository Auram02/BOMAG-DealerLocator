<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="ProcessLeads.aspx.cs" Inherits="Dealer_Locator.admin.ProcessLeads" %>

<%@ Register Src="~/admin/DesktopLead/TransferLeads.ascx" TagName="TransferLeads"
    TagPrefix="DL" %>
<%@ Register Src="~/admin/DesktopLead/PushLeadsManually.ascx" TagName="PushLeadsManually"
    TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#TransferLeadsTab"><span>Transfer Leads</span></a></li>
            <li><a href="#PushLeadsManuallyTab"><span>Push Leads Manually</span></a></li>
        </ul>
        <div id="TransferLeadsTab">
            <DL:TransferLeads ID="TransferLeadsUserControl1" runat="server" />
        </div>
        <div id="PushLeadsManuallyTab">
            <DL:PushLeadsManually ID="PushLeadsManuallyUserControl1" runat="server" />
        </div>
    </div>
</asp:Content>
