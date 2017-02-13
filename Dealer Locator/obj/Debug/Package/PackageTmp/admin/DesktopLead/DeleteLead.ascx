<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteLead.ascx.cs" Inherits="Dealer_Locator.admin.DesktopLead.DeleteLead" %>
<br />
    <asp:Label ID="lblResult" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label><br />
    <br />
    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr align="center">
            <td>
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            Search By Last Name</td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:TextBox ID="txtLastName" runat="server" Width="512px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnSearch" runat="server" OnClick="btnUpdateEmail_Click" Text="Search"
                    Width="103px" />
                <br />
                <br />
                <asp:Label ID="lblEmailUpdate" runat="server"></asp:Label></td>
        </tr>
    </table>
    <br />
    <table cellpadding="3" class="contentContainer" id="LeadsTable" runat="server">
        <tr align="left">
            <td>
                &nbsp;
                &nbsp;&nbsp;
                <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_OnRowCommand"
                    OnRowDataBound="GridView1_OnRowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">                    <RowStyle CssClass="gridviewClass_TableRow" />
                    <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                    <HeaderStyle CssClass="gridviewClass_Header" />
                    <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />                        <Columns><asp:TemplateField HeaderText="RemoveLead">
                            <ItemTemplate>
                                <center>
                                    <asp:LinkButton ID="RemoveLead" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="RemoveLead">Remove</asp:LinkButton></center>
                            </ItemTemplate>
                        </asp:TemplateField>	</Columns>
                </asp:GridView>
               
            </td>
        </tr>
    </table>
    <br />
    <table cellpadding="3" class="contentContainer" id="tblConfirm" runat="server">
                        <tr>
                        <td class="dlHeader" style="width: 520px">
                            Confirm Removal</td>
                    </tr>
        <tr>
            <td style="height: 94px" align ="center">
                <br />
                <asp:Label ID="lblConfirm" runat="server"></asp:Label><br />
                <br />
                &nbsp;&nbsp;<asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_Click" />
                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                <br />
                <asp:Label ID="lblLeadID" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <br />