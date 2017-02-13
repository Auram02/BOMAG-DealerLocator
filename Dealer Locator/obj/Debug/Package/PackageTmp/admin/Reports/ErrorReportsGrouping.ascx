<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorReportsGrouping.ascx.cs"
    Inherits="Dealer_Locator.admin.Reports.ErrorReportsGrouping" %>
<%@ Register Src="~/admin/Reports/IncorrectDistributorCityReport.ascx" TagName="IncorrectDistributorCityReport"
    TagPrefix="DL" %>
<%@ Register Src="~/admin/Reports/IncorrectDistributorZipReport.ascx" TagName="IncorrectDistributorZipReport"
    TagPrefix="DL" %>
<%@ Register Src="~/admin/Reports/LeadSubmissionErrorsReport.ascx" TagName="LeadSubmissionErrorsReport"
    TagPrefix="DL" %>
<div id="tabs2">
    <ul>
        <li><a href="#IncorrectDistributorCity"><span>Incorrect Distributor City</span></a></li>
        <li><a href="#IncorrectDistributorZip"><span>Incorrect Distributor Zip</span></a></li>
        <li><a href="#LeadSubmissionErrors"><span>Lead Submission Errors</span></a></li>
    </ul>
    <div id="IncorrectDistributorCity">
        <DL:IncorrectDistributorCityReport ID="IncorrectDistributorCityReportUserControl1"
            runat="server" />
    </div>
    <div id="IncorrectDistributorZip">
        <DL:IncorrectDistributorZipReport ID="IncorrectDistributorZipReportUserControl1"
            runat="server" />
    </div>
    <div id="LeadSubmissionErrors">
        <DL:LeadSubmissionErrorsReport ID="LeadSubmissionErrorsReportUserControl1" runat="server" />
    </div>
</div>
