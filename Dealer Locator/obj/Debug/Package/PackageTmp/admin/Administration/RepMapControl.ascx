<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RepMapControl.ascx.cs" Inherits="Dealer_Locator.admin.Admin.RepMapControl" %>
<table cellpadding="3" cellspacing="2" class="contentContainer" style="width: 351px">
        <tr>
            <td class="dlHeader" colspan="2" style="height: 19px">
                Representative Mapping</td>
        </tr>
        <tr style="background-color: #f0f0f0">
            <td style="width: 177px">
                User Login</td>
            <td style="width: 243px">
                Representative</td>
        </tr>
        <tr style="background-color: #dde1e2">
            <td style="width: 177px">
                <asp:DropDownList ID="cboUser" runat="server" Width="146px" OnSelectedIndexChanged="cboUser_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
            <td style="width: 243px">
                <asp:DropDownList ID="cboRep" runat="server" 
                    Width="208px">
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td align="center" colspan="2">
                <br />
                <asp:Button ID="btnUpdateMapping" runat="server" Height="28px" Text="Update Mapping" Width="115px" OnClick="btnUpdateMapping_Click"  /><br />
                <br />
                <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    </center>