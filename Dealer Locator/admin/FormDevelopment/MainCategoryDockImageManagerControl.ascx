<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainCategoryDockImageManagerControl.ascx.cs"
    Inherits="Dealer_Locator.admin.FormDevelopment.MainCategoryDockImageManagerControl" %>
<table class="navHeader contentHeader" cellpadding="3">
    <tr>
        <td>
            Model Dock Menu Image + Title Manager
        </td>
    </tr>
</table>
<br />
<br />
<div>
    <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
        ForeColor="Red" />
</div>
<br />
<br />
<table class="contentContainer" cellpadding="3" cellspacing="0">
    <tr>
        <td>
            <table style="border: 1px solid black;" cellpadding="3" cellspacing="0">
                <tr valign="top" style="background-color: #E1E1E1;">
                    <td style="height: 30px; width: 472px;">
                        MAIN CATEGORY
                        <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" Width="351px"
                            OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;
                    </td>
                </tr>
                <tr valign="top" style="background-color: #E1E1E1;">
                    <td style="height: 30px; width: 472px;">
                    <div>
                            Menu Title: <asp:TextBox ID="DockMenuTitle" runat="server" />
                            <br />
                        </div>
                        <div>
                            Current Large Image Path:
                            <asp:Label ID="CurrentImagePathLarge" runat="server" />
                            <br />
                            <asp:Image ID="CurrentImagePreviewLarge" runat="server" />
                        </div>
                        <div>
                            Current Small Image Path:
                            <asp:Label ID="CurrentImagePathSmall" runat="server" />
                            <br />
                            <asp:Image ID="CurrentImagePreviewSmall" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr valign="top" style="background-color: #E1E1E1;">
                    <td style="height: 30px; width: 472px;">
                        New Image (large):
                        <asp:FileUpload ID="DockMenuImageUrlLargeFileUpload" runat="server" />
                    </td>
                </tr>
                <tr valign="top" style="background-color: #E1E1E1;">
                    <td style="height: 30px; width: 472px;">
                        New Image (small):
                        <asp:FileUpload ID="DockMenuImageUrlSmallFileUpload" runat="server" />
                    </td>
                </tr>
                <tr valign="top" style="background-color: #E1E1E1;">
                    <td style="height: 30px; width: 472px;">
                        <asp:Button ID="UpdateDockMenuImageUrl" runat="server" OnClick="UpdateDockMenuImageUrl_Click"
                            Text="Update Image Urls + Title" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
