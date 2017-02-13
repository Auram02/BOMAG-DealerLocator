<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExportUnsentLeads.ascx.cs" Inherits="Dealer_Locator.admin.DataExport.ExportUnsentLeads1" %>
    <table id="tblConfirm" runat="server" cellpadding="3" class="contentContainer">
        <tr>
            <td class="dlHeader" style="width: 520px">
                Export Unsent Leads</td>
        </tr>
        <tr>
            <td align="center" style="height: 94px">
                <br />
                &nbsp;&nbsp;<asp:Button ID="btnExportUnsentLeads" runat="server" OnClick="btnExportUnsentLeads_Click"
                    Text="Create Export File" />
                &nbsp; &nbsp; &nbsp;<br />
                &nbsp; &nbsp;
                <br />
                <asp:Literal ID="xmlDocUrl" runat="server" />
                <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
                    ForeColor="Red"></asp:Label></td>
        </tr>
    </table>