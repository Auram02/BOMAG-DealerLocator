<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RentalAdministratorEmailControl.ascx.cs" Inherits="Dealer_Locator.admin.Admin.RentalAdministratorEmailControl" %>
<font size="4" style="font-weight:bold;" color="#3D51AA">RENTAL ADMINISTRATOR E-MAIL SETUP</font>
<br /><br />


    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="0">
        <ProgressTemplate>
            <img alt="Updating..." src="../../admin/Images/ajax-loader.gif" />
            Updating...
            <br />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Label ID="lblResult" runat="server"></asp:Label><br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="3" cellspacing="0" class="contentContainer">
                <tr align="center">
                    <td style="width: 796px">
                        <table cellpadding="3">
                            <tr>
                                <td class="dlHeader" style="width: 788px">
                                    Rental Administrator Email Address</td>
                            </tr>
                            <tr>
                                <td style="width: 788px" align="center">
                                    <br />
                                    Email Address: <asp:TextBox ID="txtHeavyDistributorEmail" runat="server" Height="15px" Width="350px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        &nbsp;<asp:Button ID="btnSave" runat="server" Height="41px" OnClick="btnSave_Click"
                            Text="Save" Width="128px" /></td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>