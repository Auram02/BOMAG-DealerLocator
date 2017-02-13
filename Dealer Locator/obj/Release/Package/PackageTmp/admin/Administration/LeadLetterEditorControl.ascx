<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeadLetterEditorControl.ascx.cs"
    Inherits="Dealer_Locator.admin.Admin.LeadLetterEditorControl" %>
<br />
<font size="4" style="font-weight: bold;" color="#3D51AA">TEMPLATE DEVELOPMENT</font>
<br />
<br />
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
    DisplayAfter="0">
    <ProgressTemplate>
        <img src="../../admin/Images/ajax-loader.gif" />
        Updating...
        <br />
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:Label ID="lblResult" runat="server"></asp:Label><br />
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="contentContainer" cellpadding="3" cellspacing="0">
            <tr align="center">
                <td style="width: 796px">
                    <table cellpadding="3">
                        <tr>
                            <td class="dlHeader" style="width: 788px">
                                Sales Lead Form Vendor Email Template
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 788px">
                                <asp:TextBox ID="txtVendorEmail" runat="server" Height="439px" TextMode="MultiLine"
                                    Width="778px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;<asp:Button ID="btnSave" runat="server" Height="41px" OnClick="btnSave_Click"
                        Text="Save" Width="128px" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSave" />
    </Triggers>
</asp:UpdatePanel>
<br />
