<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModifyLeadSearch.ascx.cs" Inherits="Dealer_Locator.admin.DesktopLead.ModifyLeadSearch1" %>

<%@ Register Src="~/admin/Reports/LeadList.ascx" TagName="LeadList" TagPrefix="DL" %>

<div id="editStep1" runat="server">
        <br />
        <asp:Label ID="lblError" runat="server" Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label><br />
        <table cellpadding="3" cellspacing="0" class="contentContainer">
            <tr style="background-color: #e1e1e1" valign="top">
                <td>
                    Last Name:
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>
    <div id="editStep2" runat="server">
        <DL:LeadList ID="LeadList1" runat="server" />
    </div>