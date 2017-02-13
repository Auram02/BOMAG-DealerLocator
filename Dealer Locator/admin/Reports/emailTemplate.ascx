<%@ Control Language="C#" AutoEventWireup="true" Codebehind="emailTemplate.ascx.cs"
    Inherits="Dealer_Locator.admin.Reports.emailTemplate" %>
<asp:Table ID="tblEmail" runat="server" Width="100%" BorderColor="Black" BorderStyle="solid"
    BorderWidth="1px" CellPadding="5">
    <asp:TableRow runat="server">
        <asp:TableCell ColumnSpan="2" runat="server">
            <asp:Literal ID="litType" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            TO:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litTo" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            CC:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litCC" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            BCC:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litBCC" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            FROM:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litFrom" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            SUBJECT:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litSubject" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server">
        <asp:TableCell Style="width: 82px; background-color: #e6e6e6;" runat="server">
            BODY:
        </asp:TableCell>
        <asp:TableCell Style="background-color: #e6e6e6;" runat="server">
            <asp:Literal ID="litBody" runat="server"></asp:Literal>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<br />