<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="TerritoryMapReport.aspx.cs" Inherits="Dealer_Locator.admin.TerritoryMapReport" %>
<%@ Register src="TerritoryMapReportControl.ascx" tagname="TerritoryMapReportControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <link rel="Stylesheet" type="text/css" href="/admin/Styles/TerritoryMapReport.css" />
    <script type="text/javascript" src="/admin/js/TerritoryMapReport.js"></script>

    <uc1:TerritoryMapReportControl ID="TerritoryMapReportControl1" runat="server" />

</asp:Content>
