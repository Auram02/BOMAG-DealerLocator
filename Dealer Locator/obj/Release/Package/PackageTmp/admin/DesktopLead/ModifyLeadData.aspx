<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ModifyLeadData.aspx.cs" Inherits="Dealer_Locator.admin.DesktopLead.ModifyLeadData"
    Title="Dealer Locator Admin - Modify Lead Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="successFailureMessage">
    <br />
    <asp:Label ID="lblResult" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label><br />
    &nbsp;</div>

    <table border="1">
        <tr>
            <td>
                First Name:
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Last Name:
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Company/Agency:
            </td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Country/Region:
            </td>
            <td>
                <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Address Line 1:
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Address Line 2:
            </td>
            <td>
                <asp:TextBox ID="txtAddressLine2" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                City:
            </td>
            <td>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                State:
            </td>
            <td>
                <asp:TextBox ID="txtState" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Zip Code:
            </td>
            <td>
                <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Email:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Phone:
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Fax:
            </td>
            <td>
                <asp:TextBox ID="txtFax" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="javascript:history.back();return false;" />
            </td>
        </tr>
    </table>

</asp:Content>
