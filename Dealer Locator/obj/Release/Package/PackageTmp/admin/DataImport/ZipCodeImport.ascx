<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZipCodeImport.ascx.cs" Inherits="Dealer_Locator.admin.DataImport.ZipCodeImport" %>
<font size="4" style="font-weight:bold;" color="#3D51AA">DATA IMPORT</font>
<br /><br />
    
    <div id="updateDiv" style="display:none;"><img src="../../admin/Images/ajax-loader.gif" id="IMG1" runat="server" />
    Updating...&nbsp;</div>

    <table class="contentContainer" style="width: 382px" cellpadding="3">
        <tr>
            <td class="dlHeader" align="center">
                Upload Zip Code Database</td>
        </tr>
        <tr>
            <td align="center">
                <br />
                Locate File:
                <asp:FileUpload ID="FileUpload1" runat="server" OnLoad="upload_load" /><br />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                <div id="fake" onclick="document.getElementById('updateDiv').style['display'] = 'block';"><asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload File" /></div>
                <br />
                <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
                    ForeColor="Red"></asp:Label></td>
        </tr>
    </table>