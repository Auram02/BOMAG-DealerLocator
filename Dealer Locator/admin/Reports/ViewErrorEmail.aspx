<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="ViewErrorEmail.aspx.cs" Inherits="Dealer_Locator.admin.Reports.ViewErrorEmails" Title="Dealer Locator Admin - View Email" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<br />

    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label><br />
    <br />
<asp:Table ID="tblEmail" runat="server" Width="100%" BorderColor="Black" BorderStyle="solid"
    BorderWidth="1px" CellPadding="5">
    <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell ID="TableCell1" ColumnSpan="2" runat="server">
            <asp:Literal ID="litType" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server">
        <asp:TableCell ID="TableCell2" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            TO:
        </asp:TableCell>
        <asp:TableCell ID="TableCell3" Style="background-color: #e6e6e6;" runat="server">
            <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow3" runat="server">
        <asp:TableCell ID="TableCell4" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            CC:
        </asp:TableCell>
        <asp:TableCell ID="TableCell5" Style="background-color: #e6e6e6;" runat="server">
            <asp:TextBox ID="txtCC" runat="server"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow4" runat="server">
        <asp:TableCell ID="TableCell6" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            BCC:
        </asp:TableCell>
        <asp:TableCell ID="TableCell7" Style="background-color: #e6e6e6;" runat="server">
            <asp:TextBox ID="txtBCC" runat="server"></asp:TextBox>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow5" runat="server">
        <asp:TableCell ID="TableCell8" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            FROM:
        </asp:TableCell>
        <asp:TableCell ID="TableCell9" Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litFrom" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow6" runat="server">
        <asp:TableCell ID="TableCell10" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            SUBJECT:
        </asp:TableCell>
        <asp:TableCell ID="TableCell11" Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litSubject" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow7" runat="server">
        <asp:TableCell ID="TableCell12" Style="width: 82px; background-color: #e6e6e6;" runat="server">
            BODY:
        </asp:TableCell>
        <asp:TableCell ID="TableCell13" Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litBody" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
    <br />
    <asp:Button ID="btnResendLead" runat="server" OnClick="btnResendLead_Click" Text="Resend Lead" /><br />
</asp:Content>
