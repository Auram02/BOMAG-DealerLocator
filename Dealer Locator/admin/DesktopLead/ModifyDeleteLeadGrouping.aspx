<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="ModifyDeleteLeadGrouping.aspx.cs" Inherits="Dealer_Locator.admin.DesktopLead.ModifyDeleteLeadGrouping" %>

<%@ Register Src="ModifyLeadSearch.ascx" TagName="ModifyLeadSearch" TagPrefix="DL" %>
<%@ Register Src="DeleteLead.ascx" TagName="DeleteLead" TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="tabs">
        <ul>
            <li><a href="#ModifyLeadTab"><span>Modify Lead</span></a></li>
            <li><a href="#DeleteLeadTab"><span>Delete Lead</span></a></li>
            
        </ul>
        <div id="ModifyLeadTab">
            <DL:ModifyLeadSearch ID="ModifyLeadSearchUserControl1" runat="server" />
        </div>
        <div id="DeleteLeadTab">
            <DL:DeleteLead ID="DeleteLeadUserControl1" runat="server" />
        </div>
    </div>
</asp:Content>
