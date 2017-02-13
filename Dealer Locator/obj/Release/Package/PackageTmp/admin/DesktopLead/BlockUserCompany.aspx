<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="BlockUserCompany.aspx.cs" Inherits="Dealer_Locator.admin.DesktopLead.BlockUserCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div>
        <div id="searchBlock" style="margin-bottom:10px;">
            <label for="txtLastName">
                Enter a partial or full Last Name:
            </label>
            <asp:TextBox ID="txtLastName" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>
        <div id="searchResults" style="margin-left: auto; margin-right: auto;">

                    <div id="errorSection">
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red" />
                    </div>
                    <span style="float:left; margin-left:25px;">
                    <div>User List</div>
                        <asp:GridView AllowPaging="true" PageSize="20" runat="server" ID="gdvSearchResults"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="LastName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FirstName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zip">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnBlock" runat="server" Text="Block" CommandName="BlockUser" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridviewClass_Header" />
                            <RowStyle CssClass="gridviewClass_TableRow" />
                            <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                            <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                        </asp:GridView>
                    </span><span style="float:right; margin-right:25px;"">
                    <div>Blocked Users</div>
                        <asp:GridView AllowPaging="true" PageSize="20" runat="server" ID="gvLeadBlackList"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="LastName">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnLeadBlackListId" runat="server" Value='<%# Bind("leadBlackListId") %>' />
                                        <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Phone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zip">
                                    <ItemTemplate>
                                        <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("emailAddress") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Blocked Submissions">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubmissionCount" runat="server" Text='<%# Bind("submissionCount") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnUnBlock" runat="server" Text="Un-Block" CommandName="UnBlockUser" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridviewClass_Header" />
                            <RowStyle CssClass="gridviewClass_TableRow" />
                            <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                            <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                        </asp:GridView>
                    </span>
        </div>
    </div>
</asp:Content>
