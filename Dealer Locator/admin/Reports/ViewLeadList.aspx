<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="ViewLeadList.aspx.cs" Inherits="Dealer_Locator.admin.Reports.ViewLeadList" 
Title="Dealer Locator Admin - Leads List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <table cellpadding="2" cellspacing="2" class="contentContainer" width="600">
        <tr style="background-color: #e1e1e1" valign="top">
            <td align="left" style="width: 478px">
                <asp:Label ID="lblBreadcrumb" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <table id="tblUploadContainer" runat="server" cellpadding="3" class="contentContainer"
        width="400">

        <tr>
            <td class="dlHeader">
                Generate Leads By Source Excel Report</td>
        </tr>
        <tr><td style="height: 22px">
            <asp:Label ID="lblExcelUrl" runat="server"></asp:Label></td></tr>
    </table>
    <br />
    <asp:Literal ID="litReportPageListing" runat="server"></asp:Literal><br />
    <br />
    <asp:Table ID="tblTM" runat="server" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Width="300px">
    </asp:Table>
    <br />


</asp:Content>
