<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="TerritoryManagerReport.aspx.cs" Inherits="Dealer_Locator.admin.Reports.TerritoryManagerReport" 
Title="Dealer Locator Admin - Territory Manager Lead Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <br />
    <table cellpadding="2" cellspacing="2" class="contentContainer" width="600px">
        <tr style="background-color: #e1e1e1" valign="top">
            <td align="left" style="width: 478px">
                <asp:Label ID="lblBreadcrumb" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <asp:Table ID="tblTM" runat="server" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Width="300px">
    </asp:Table>
</asp:Content>
