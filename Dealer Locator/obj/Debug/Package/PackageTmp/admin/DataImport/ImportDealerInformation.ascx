<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportDealerInformation.ascx.cs" Inherits="Dealer_Locator.admin.DataImport.ImportDealerInformation1" %>
<font size="4" style="font-weight:bold;" color="#3D51AA">DATA IMPORT</font>
<br /><br />
    <div id="updateDiv" style="display:none;"><img src="../../admin/Images/ajax-loader.gif" id="IMG1" runat="server" />
    Updating...&nbsp;</div>
    

    <table class="contentContainer" width="400px" cellpadding="3">
        <tr>
            <td class="dlHeader">
                Upload Distributor Database Application (DDA) Database
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
                &nbsp;<div id="fake" onclick="document.getElementById('updateDiv').style['display'] = 'block';"><asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload File" /></div>
                <br />
                <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
                    ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
