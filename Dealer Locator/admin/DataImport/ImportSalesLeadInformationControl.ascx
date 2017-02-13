<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportSalesLeadInformationControl.ascx.cs"
    Inherits="Dealer_Locator.admin.DataImport.Data.ImportSalesLeadInformationControl" %>



<br />
<br />
<table cellpadding="3">
    <tr>
        <td class="dlHeader" style="width: 398px">
            Sales Lead Form
        </td>
    </tr>
    <tr>
        <td align="center" style="width: 398px; background-color: #d4d7ea">
            <asp:DropDownList ID="cboSalesLeadForm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboSalesLeadForm_SelectedIndexChanged"
                Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table runat="server" id="tblDownloadExcel" cellpadding="3">
    <tr>
        <td class="dlHeader" style="width: 398px">
            Download Excel Template
        </td>
    </tr>
    <tr>
        <td align="center" style="width: 398px; background-color: #d4d7ea">
            <asp:Button ID="btnDownloadExcel" runat="server" Text="Generate Template" OnClick="btnDownloadExcel_Click" /><br />
            <br />
            <asp:Literal ID="litDownloadURL" runat="server"></asp:Literal>
        </td>
    </tr>
</table>
<br />
<br />
<table class="contentContainer" id="tblUploadContainer" runat="server" width="400"
    cellpadding="3">
    <tr id="trImporting" runat="server">
        <td>
            <img id="IMG1" runat="server" src="../../admin/Images/ajax-loader.gif" />
            Importing...&nbsp;
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
            Locate File:
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            &nbsp;<div id="fake" onclick="document.getElementById('updateDiv').style['display'] = 'block';">
                <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload File" /><br />
                <br />
                Before uploading, please ensure that you seperated multiple model names with a semi-colon
                ( ; ).&nbsp;
                <br />
                <br />
                Do not enter any spaces before or after the semi-colon.&nbsp; If you do, the import
                may find a model name as "invalid".<br />
                <br />
                Example: model1; model2 &nbsp; &lt;-- (bad)<br />
                Example: model1;model2 &nbsp;&nbsp; &lt;-- (good)</div>
        </td>
    </tr>
</table>
<br />
<br />
<table class="contentContainer" id="tblImportErrors" runat="server" width="400" cellpadding="3">
    <tr>
        <td class="dlHeader">
            Import Errors
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
                ForeColor="Red"></asp:Label><br />
            <br />
            <asp:GridView ID="gvImportErrors" runat="server">
            </asp:GridView>
            <br />
        </td>
    </tr>
</table>
