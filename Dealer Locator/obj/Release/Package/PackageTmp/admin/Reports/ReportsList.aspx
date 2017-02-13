<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="ReportsList.aspx.cs" Inherits="Dealer_Locator.admin.ReportsList" %>
    
<%@ Register Src="~/admin/Reports/PendingLeadsListReport.ascx" TagName="PendingLeadsListReport" TagPrefix="DL" %>
<%@ Register Src="~/admin/Reports/GeneralTMReport.ascx" TagName="GeneralTMReport" TagPrefix="DL" %>
<%@ Register Src="~/admin/Reports/BySourceReport.ascx" TagName="BySourceReport" TagPrefix="DL" %>
<%@ Register Src="~/admin/Reports/ErrorReportsGrouping.ascx" TagName="ErrorReportsGrouping" TagPrefix="DL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#PendingLeadsTab"><span>Pending Leads</span></a></li>
            <li><a href="#GeneralTab"><span>General</span></a></li>
            <li><a href="#BySourceTab"><span>By Source</span></a> </li>
            <li><a href="#ByStateTab"><span>By State</span></a> </li>
            <li><a href="#ErrorReportsTab"><span>Error Reports</span></a> </li>
        </ul>
        <div id="PendingLeadsTab">
            <DL:PendingLeadsListReport ID="PendingLeadsListReportUserControl1" runat="server" />
        </div>
        <div id="GeneralTab">
            <DL:GeneralTMReport ID="GeneralTMReportUserControl1" runat="server" />

        </div>
        <div id="BySourceTab">
            <DL:BySourceReport ID="BySourceReportUserControl1" runat="server" />
        </div>
        <div id="ByStateTab">
        </div>
        <div id="ErrorReportsTab">
            <DL:ErrorReportsGrouping ID="ErrorReportsGroupingUserControl1" runat="server" />
        </div>
    </div>
</asp:Content>
