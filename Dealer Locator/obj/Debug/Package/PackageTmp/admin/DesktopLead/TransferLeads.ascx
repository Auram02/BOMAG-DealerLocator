<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferLeads.ascx.cs"
    Inherits="Dealer_Locator.admin.DesktopLead.TransferLeads1" %>
<table id="tblUploadContainer" runat="server" cellpadding="3" class="contentContainer"
    width="400">
    <tr>
        <td align="center">
            <div id="updateDiv" style="display: none;">
                <img src="../../admin/Images/ajax-loader.gif" id="IMG1" runat="server" />
                Transferring...&nbsp;</div>
        </td>
    </tr>
    <tr>
        <td class="dlHeader">
            Upload Bulk Sales Lead&nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            Please verify the below information is correct before uploading the new leads.&nbsp;
            If it is not, please contact the developer.<br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="SOURCE Database Server: "></asp:Label>&nbsp;
            <asp:Literal ID="litSourceDatabase" runat="server"></asp:Literal><br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="DESTINATION Database Server: "></asp:Label>
            <asp:Literal ID="litDestinationDatabase" runat="server"></asp:Literal><br />
            &nbsp;<br />
            <asp:Label ID="Label5" runat="server" Text="Records to Transfer: "></asp:Label>
            <asp:Label ID="lblRecordsToTransfer" runat="server" Font-Bold="True"></asp:Label><br />
            <br />
            <asp:Label ID="lblRecordsTransferedLabel" runat="server" Text="TOTAL Records Transferred: "
                ForeColor="Red"></asp:Label>
            <asp:Label ID="lblRecordsTransfered" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label><br />
            <br />
            <asp:Literal ID="litResult" runat="server"></asp:Literal><br />
            <br />
            <asp:Literal ID="litReloadLink" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            &nbsp;<div id="fake" onclick="document.getElementById('updateDiv').style['display'] = 'block';">
                <asp:Button ID="btnTransferLeads" runat="server" Text="TRANSFER LEADS" OnClick="btnTransferLeads_Click" /><br />
                <br />
                &nbsp;</div>
        </td>
    </tr>
</table>
<table id="tblProcessLeads" runat="server" cellpadding="3" class="contentContainer"
    width="400">
    <tr>
        <td align="center">
            <div id="processingDiv" style="display: none;">
                <img src="../../admin/Images/ajax-loader.gif" id="IMG2" runat="server" />
                Processing...&nbsp;</div>
        </td>
    </tr>
    <tr>
        <td class="dlHeader">
            Process Uploaded XML File
        </td>
    </tr>
    <tr>
        <td>
            The following file is ready for processing. If you feel that this file has already
            been processed or if an error occurred above during the upload, please contact the developer with the file's name and any error messages received.
        </td>
    </tr>
    <tr>
        <td>
            Filename:
            <asp:Label ID="lblFileNameToProcess" runat="server" /><br />
            <asp:Literal ID="litResultProcessing" runat="server"></asp:Literal><br />
            <br />
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            &nbsp;<div id="Div2" onclick="document.getElementById('processingDiv').style['display'] = 'block';">
                <asp:Button ID="btnProcessLeads" runat="server" Text="Process Leads" OnClick="btnProcessLeads_Click" /><br />
                <br />
                &nbsp;</div>
        </td>
    </tr>
</table>
