<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportGrouping.ascx.cs" Inherits="Dealer_Locator.admin.DataExport.ExportGrouping" %>
<%@ Register Src="~/admin/DataExport/ExportUnsentLeads.ascx" TagName="ExportUnsentLeads"
    TagPrefix="DL" %>
<%@ Register Src="~/admin/DataExport/ExcelDownload.ascx" TagName="ExcelDownload"
    TagPrefix="DL" %>

<div id="tabs3">
    <ul>
        <li><a href="#ExportLeads"><span>Leads</span></a></li>

        <li><a href="#ExportUnsentLeads"><span>Unsent Leads</span></a></li>
    </ul>
    
    <div id="ExportLeads">
        <DL:ExcelDownload ID="ExcelDownloadUserControl1" runat="server" />
    </div>

        <div id="ExportUnsentLeads">
        <DL:ExportUnsentLeads ID="ExportUnsentLeadsUserControl1"
            runat="server" />
    </div>
</div>
