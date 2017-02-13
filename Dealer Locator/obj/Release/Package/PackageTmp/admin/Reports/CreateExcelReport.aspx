<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="CreateExcelReport.aspx.cs" Inherits="Dealer_Locator.admin.Reports.CreateExcelReport"
    Title="Dealer Locator Admin - Download Excel Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table cellpadding="2" cellspacing="2" class="contentContainer" width="600">
        <tr style="background-color: #e1e1e1" valign="top">
            <td align="left" style="width: 478px">
                <asp:Label ID="lblBreadcrumb" runat="server"></asp:Label></td>
        </tr>
    </table>
<br />
    <table id="tblLeadsBySource" runat="server"  style="border:1px solid black;" cellpadding="4">
        <tr>
            <td>
                <asp:Literal ID="litExcelReportUrl" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
