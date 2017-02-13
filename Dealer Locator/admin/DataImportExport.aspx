<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="DataImportExport.aspx.cs" Inherits="Dealer_Locator.admin.DataImportExport" %>
<%@ Register Src="~/admin/DataImport/ImportGrouping.ascx" TagName="ImportGrouping"
    TagPrefix="DL" %>
    <%@ Register Src="~/admin/DataExport/ExportGrouping.ascx" TagName="ExportGrouping"
    TagPrefix="DL" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <div id="tabs">
        <ul>
            <li><a href="#ImportTab"><span>Import</span></a></li>
            <li><a href="#ExportTab"><span>Export</span></a></li>
        </ul>
        <div id="ImportTab">
            <DL:ImportGrouping id="ImportGroupingControl" runat="server" />
        </div>
        <div id="ExportTab">        
            <DL:ExportGrouping id="ExportGroupingControl" runat="server" />
        </div>
    </div>
</asp:Content>
