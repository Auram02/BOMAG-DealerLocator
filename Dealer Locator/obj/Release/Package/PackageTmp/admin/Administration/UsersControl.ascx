<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersControl.ascx.cs" Inherits="Dealer_Locator.admin.Admin.UsersControl" %>
<br />
    <font size="4" style="font-weight: bold;" color="#3D51AA">ACCOUNT SETUP</font>
    <br />
    <br />
    <table class="contentContainer" cellpadding="3">
        <tr align="center">
            <td>
                <table cellpadding="3">
                    <tr style="background-color: #E1E1E1;">
                        <td width="70%" align="right">
                            <strong>Sort By Group:</strong>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboGroupName" runat="server" AutoPostBack="True" DataSourceID="groupDataSource"
                                DataTextField="GroupName" DataValueField="pk_groupID" Width="191px" OnSelectedIndexChanged="cboGroupName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                DataKeyNames="pk_userID" DataSourceID="ObjectDataSource1" Width="714px" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
                                    <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
                                    <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                                    <asp:CheckBoxField DataField="admin" HeaderText="admin" SortExpression="admin" />
                                    <asp:BoundField DataField="pk_userID" HeaderText="pk_userID" ReadOnly="True" SortExpression="pk_userID" />
                                    <asp:BoundField DataField="fk_groupID" HeaderText="fk_groupID" SortExpression="fk_groupID" />
                                    <asp:CheckBoxField DataField="territoryManager" HeaderText="territory manager" SortExpression="territoryManager" />
                                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                </Columns>
                                <HeaderStyle CssClass="gridviewClass_Header" />
                                <RowStyle CssClass="gridviewClass_TableRow" />
                                <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="DeleteQuery"
                                InsertMethod="Insert" OldValuesParameterFormatString="{0}" SelectMethod="GetDataByGroupID"
                                TypeName="Dealer_Locator.DA.UserTDSTableAdapters.DL_UserTableAdapter" UpdateMethod="UpdateQuery1">
                                <DeleteParameters>
                                    <asp:Parameter Name="pk_userID" Type="Int32" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="username" Type="String" />
                                    <asp:Parameter Name="password" Type="String" />
                                    <asp:Parameter Name="email" Type="String" />
                                    <asp:Parameter Name="admin" Type="Boolean" />
                                    <asp:Parameter Name="fk_groupID" Type="Int32" />
                                    <asp:Parameter Name="pk_userID" Type="Int32" />
                                    <asp:Parameter Name="territoryManager" Type="Boolean" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cboGroupName" DefaultValue="0" Name="GroupID" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="username" Type="String" />
                                    <asp:Parameter Name="password" Type="String" />
                                    <asp:Parameter Name="email" Type="String" />
                                    <asp:Parameter Name="admin" Type="Boolean" />
                                    <asp:Parameter Name="pk_userID" Type="Int32" />
                                    <asp:Parameter Name="fk_groupID" Type="Int32" />
                                    <asp:Parameter Name="territoryManager" Type="Boolean" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="text-align: right;">
                                <asp:LinkButton ID="lnkAddNew" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>
                                &nbsp;&nbsp;
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <asp:ObjectDataSource ID="groupDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData_nonDisabled" TypeName="Dealer_Locator.DA.GroupdTDSTableAdapters.DL_GroupTableAdapter">
    </asp:ObjectDataSource>