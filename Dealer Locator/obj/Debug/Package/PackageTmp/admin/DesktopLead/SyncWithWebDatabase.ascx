<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SyncWithWebDatabase.ascx.cs" Inherits="Dealer_Locator.admin.DesktopLead.SyncWithWebDatabase" %>
<br />
    <table id="tblUploadContainer" runat="server" cellpadding="3" class="contentContainer"
        width="400">
        <tr>
            <td align="center">
                <div id="updateDiv" style="display: none">
                    <img id="IMG1" runat="server" src="../../admin/Images/ajax-loader.gif" />
                    Sync'ing...&nbsp;</div>
            </td>
        </tr>
        <tr>
            <td class="dlHeader">
                Data Sync&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <br />
                Please verify the below information is correct before sync'ing the tables.&nbsp;
                If it is not, please contact the developer.<br />
                <br />
                <asp:Label ID="Label1" runat="server" Text="SOURCE Database Server: "></asp:Label>&nbsp;
                <asp:Literal ID="litSourceDatabase" runat="server"></asp:Literal><br />
                <br />
                <asp:Label ID="Label3" runat="server" Text="DESTINATION Database Server: "></asp:Label>
                <asp:Literal ID="litDestinationDatabase" runat="server"></asp:Literal><br />
                &nbsp;<br />
                <asp:Label ID="lblResult" runat="server" Font-Bold="True" Font-Size="Larger" Font-Strikeout="False"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                &nbsp;<div id="fake" onclick="document.getElementById('updateDiv').style['display'] = 'block';">
                    <asp:Button ID="btnSyncData" runat="server"
                        Text="SYNC DATA" OnClick="btnSyncData_Click" /><br />
                    <br />
                    &nbsp;</div>
            </td>
        </tr>
    </table>