<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="UsersGrouping.aspx.cs" Inherits="Dealer_Locator.admin.Admin.UsersGrouping" %>

<%@ Register Src="UsersControl.ascx" TagName="UsersControl" TagPrefix="uc1" %>
<%@ Register Src="GroupsControl.ascx" TagName="GroupsControl" TagPrefix="uc2" %>
<%@ Register Src="RepMapControl.ascx" TagName="RepMapControl" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <div id="tabs">
        <ul>
            <li><a href="#UsersTab"><span>User Accounts</span></a></li>
            <li><a href="#GroupsTab"><span>Group Accounts</span></a></li>
            <li><a href="#RepMapTab"><span>Representative Map</span></a> </li>
        </ul>
        <div id="UsersTab">
            <uc1:UsersControl ID="UsersControl1" runat="server" />
        </div>
        <div id="GroupsTab">
            <uc2:GroupsControl ID="GroupsControl1" runat="server" />
        </div>
        <div id="RepMapTab">
            <uc3:RepMapControl ID="RepMapControl1" runat="server" />
        </div>
    </div>
</asp:Content>
