<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ViewLead.aspx.cs" Inherits="Dealer_Locator.admin.Reports.ViewLead"
    Title="Dealer Locator Admin - Lead Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table width="100%"><tr><td align="left">
            <table cellpadding="2" cellspacing="2" class="contentContainer" width="550px">
                <tr style="background-color: #e1e1e1" valign="top">
                    <td align="left">
                        <asp:Label ID="lblBreadcrumb" runat="server"></asp:Label></td>
                </tr>
            </table>
            <asp:Table ID="Table1" runat="server" Width="550px">
                <asp:TableRow runat="server">
                    <asp:TableCell runat="server">
                        <center>
                            <asp:PlaceHolder ID="phEmailTemplates" runat="server"></asp:PlaceHolder>
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table></td></tr></table>
</asp:Content>
