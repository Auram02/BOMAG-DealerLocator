<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="User.aspx.cs" Inherits="Dealer_Locator.admin.User" Title="Dealer Locator Admin - My Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- /*
    <table class="navHeader contentHeader">
        <tr>
            <td>
                My Account
            </td>
        </tr>
    </table> 
    */ -->
<br />

    <table class="contentContainer"  cellpadding="3" cellspacing="0">
        <tr align="center">
            <td>
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            Email Address</td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:TextBox ID="txtEmailAddress" runat="server" Width="512px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnUpdateEmail" runat="server" OnClick="btnUpdateEmail_Click" Text="Update"
                    Width="103px" />
                <br />
                <br />
                <asp:Label ID="lblEmailUpdate" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <br />
    <table class="contentContainer"  cellpadding="3" cellspacing="0">
        <tr align="center">
            <td>
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            Current Password</td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:TextBox ID="txtPassword" runat="server" Width="512px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            New Password</td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:TextBox ID="txtNewPassword1" runat="server" Width="512px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            Retype New Password</td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:TextBox ID="txtNewPassword2" runat="server" Width="512px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnUpdatePassword" runat="server" OnClick="btnUpdatePassword_Click"
                    Text="Update" Width="103px" /><br />
                <br />
                <asp:Label ID="lblPasswordUpdate" runat="server"></asp:Label></td>
        </tr>
    </table>
</asp:Content>
