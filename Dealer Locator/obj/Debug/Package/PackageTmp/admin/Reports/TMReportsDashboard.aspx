<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="TMReportsDashboard.aspx.cs" Inherits="Dealer_Locator.admin.Reports.TMReportsDashboard" 
Title="Dealer Locator Admin - Territory Manager Leads Report Dashboard" %>

<%@ Register Src="SelectYear.ascx" TagName="SelectYear" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <uc1:SelectYear id="SelectYear1" runat="server">
    </uc1:SelectYear>
    <br />
    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr style="background-color: #e1e1e1" valign="top">
            <td style="width: 478px">
                Leads By Year - Dealer Breakdown</td>
        </tr>
        <tr>
            <td style="width: 478px">
                <br />
                <asp:Button ID="btnLeadsByDealer" runat="server" Text="View Leads By Dealer" OnClick="btnLeadsByDealer_Click" /><br />
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr style="background-color: #e1e1e1" valign="top">
            <td style="width: 478px">
                Leads By Year - Monthly Breakdown</td>
        </tr>
        <tr>
            <td style="width: 478px">
                <br /><asp:Button ID="btnLeadsByYear" runat="server" Text="View Leads By Year" OnClick="btnLeadsByYear_Click" /><br />
                &nbsp;</td>
        </tr>
    </table>

</asp:Content>
