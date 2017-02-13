<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapFieldsControl.ascx.cs" Inherits="Dealer_Locator.admin.DesktopLead.MapFieldsControl" %>
<br />
    <table>
        <tr valign="top">
            <td style="width: 427px" colspan="2" align="center">
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Mapping Name</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                                OnRowDataBound="GridView1_OnRowDataBound" OnRowCommand="GridView1_OnRowCommand"
                                OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="Show Mapped Fields">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton CommandName="selectFields" CommandArgument="<%# Container.DataItemIndex %>"
                                                    ID="selectFields" runat="server">Select</asp:LinkButton></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="pk_mapID" HeaderText="pk_mapID" SortExpression="pk_mapID" />
                                    <asp:BoundField DataField="mapName" HeaderText="Map Name" SortExpression="mapName" />
                                    <asp:BoundField DataField="fk_mapReadMethodID" HeaderText="fk_mapReadMethodID" SortExpression="fk_mapReadMethodID" />
                                    <asp:CheckBoxField DataField="active" HeaderText="Active Map" SortExpression="active" />
                                    <asp:TemplateField HeaderText="Set Active">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton CommandName="setActive" CommandArgument="<%# Container.DataItemIndex %>"
                                                    ID="setActive" runat="server">Set</asp:LinkButton></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete Map">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton CommandName="deleteMap" CommandArgument="<%# Container.DataItemIndex %>"
                                                    ID="deleteMap" runat="server">Delete</asp:LinkButton></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridviewClass_Header" />
                                <RowStyle CssClass="gridviewClass_TableRow" />
                                <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.MapTDSTableAdapters.DL_MapTableAdapter">
                                <InsertParameters>
                                    <asp:Parameter Name="pk_mapID" Type="Int32" />
                                    <asp:Parameter Name="mapName" Type="String" />
                                    <asp:Parameter Name="fk_mapReadMethodID" Type="Int32" />
                                    <asp:Parameter Name="active" Type="Boolean" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                            <asp:Label ID="lblSetActiveFailure" runat="server" ForeColor="Red"></asp:Label>
                    </tr>
                    
                </table>
            </td>
            <td>                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Create New Mapping Name</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:TextBox ID="txtAddMapping" runat="server"></asp:TextBox>&nbsp;
                            <asp:Button ID="btnAddMapping" runat="server" Text="Add Mapping" OnClick="btnAddMapping_Click" />
                            <br />
                            <asp:Label ID="lblCreateNewMapError" runat="server" ForeColor="Red"></asp:Label>
                    </tr>
                    
                </table></td>
        </tr>
        
        </table>
    <br />
        
        <table runat="server" id="tblMapFieldSource">
        <tr valign="top">
            <td>
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Sales Lead Form</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:DropDownList ID="cboSalesLeadForm" runat="server" AutoPostBack="True" Width="200px"
                                OnSelectedIndexChanged="cboSalesLeadForm_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Scan Card</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:TextBox ID="txtScan" runat="server" Rows="4" TextMode="MultiLine" OnTextChanged="txtScan_TextChanged"
                                Width="301px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:DropDownList ID="cboReadMethod" runat="server" Width="162px">
                            </asp:DropDownList><br />
                            <br />
                            <asp:Button ID="btnSaveAndProcess" runat="server" Text="Save and Process" OnClick="btnSaveAndProcess_Click" /><br />
                            <asp:Label ID="lblSaveAndProcessError" runat="server" ForeColor="Red"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        
        </table>
    <br />
        
        <table runat="server" id="tblMapFields">
        
        <tr valign="top">
            <td>
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Sales Lead Form Fields</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:DropDownList ID="cboSalesLeadFields" runat="server" AutoPostBack="True" Width="200px"
                                >
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
            <td>
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Card Field Sample Data</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:DropDownList ID="cboCardFields" runat="server" AutoPostBack="True" Width="200px"
                                >
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr valign="top">
            <td colspan="2" align="center">
                <asp:Button ID="btnAddMap" runat="server" Text="Add Mapping" OnClick="btnAddMap_Click" /><br />
                <asp:Label ID="lblAddMapError" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        </table>
    <br />
        
        <table runat="server" id="tblMappedFields">
        <tr valign="top">
            <td colspan="2">
                <table cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 398px">
                            Current Mapped Fields</td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 398px; background-color: #d4d7ea">
                            <asp:GridView ID="gvCurrentMappings" runat="server" DataSourceID="objCurrentMappings"
                                AutoGenerateColumns="False" OnRowDataBound="gvCurrentMappings_OnRowDataBound" OnRowCommand="gvCurrentMappings_OnRowCommand">
                                <Columns>
                                    <asp:BoundField DataField="pk_mapFieldID" HeaderText="pk_mapFieldID" SortExpression="pk_mapFieldID" />
                                    <asp:BoundField DataField="fk_mapID" HeaderText="fk_mapID" SortExpression="fk_mapID" />
                                    <asp:BoundField DataField="FormField" HeaderText="Form Field Name" SortExpression="FormField" />
                                    <asp:BoundField DataField="CardPosition" HeaderText="Card Position" SortExpression="CardPosition" />
                                                                        <asp:TemplateField HeaderText="Remove Mapping">
                                        <ItemTemplate>
                                            <center>
                                                <asp:LinkButton CommandName="removeMapping" CommandArgument="<%# Container.DataItemIndex %>"
                                                    ID="deleteGroup" runat="server">Remove</asp:LinkButton></center>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridviewClass_Header" />
                                <RowStyle CssClass="gridviewClass_TableRow" />
                                <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                <EditRowStyle CssClass="gridviewClass_TableRowEdit" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="objCurrentMappings" runat="server" InsertMethod="Insert"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByMapID" TypeName="Dealer_Locator.DA.MapTDSTableAdapters.DL_MapFieldTableAdapter">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="-1" Name="fk_mapID" SessionField="WorkingMapID"
                                        Type="Int32" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="pk_mapFieldID" Type="Int32" />
                                    <asp:Parameter Name="fk_mapID" Type="Int32" />
                                    <asp:Parameter Name="FormField" Type="String" />
                                    <asp:Parameter Name="CardPosition" Type="Int32" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
            </td>
        </tr>
    </table>