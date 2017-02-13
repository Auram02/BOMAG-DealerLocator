<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="ManageDesktopSetup.aspx.cs" Inherits="Dealer_Locator.admin.ManageDesktopSetup" %>

<%@ Register Src="~/admin/DesktopLead/MapFieldsControl.ascx" TagName="MapFieldsControl" TagPrefix="DL" %>
<%@ Register Src="~/admin/DesktopLead/SyncWithWebDatabase.ascx" TagName="SyncWithWebDatabase" TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#MapFieldsTab"><span>Map Fields</span></a></li>
            <li><a href="#SyncWithWebTab1"><span>Sync With Web</span></a></li>
        </ul>
        <div id="MapFieldsTab">
            <DL:MapFieldsControl ID="ModifyLeadSearchUserControl1" runat="server" />
        </div>
        <div id="SyncWithWebTab1">
            <DL:SyncWithWebDatabase ID="SyncWithWebDatabaseUserControl1" runat="server" />
        </div>
    </div>
</asp:Content>
