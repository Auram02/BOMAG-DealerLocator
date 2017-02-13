<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="FormElements.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.FormElements"
    Title="Dealer Locator Admin - Form Elements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
             <table class="navHeader contentHeader" cellpadding="3">
            <tr>
                <td>
                    FORM DASHBOARD: <asp:Literal ID="litFormName" runat="server" />
                 </td>
            </tr>
        </table>
        <br />
        <table border="0"  cellpadding="0" cellspacing="0" width="100%">
            <tbody>
                <tr>
                    <td background="../../../admin/Images/Tabs/ui_tab_middle.gif">
                        <table border="0"  cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr><td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                        <asp:LinkButton ID="lnkUrl" runat="server" OnClick="lnkUrl_Click" Font-Names="arial; sans serif" ForeColor="black" Font-Size="10pt" Font-Underline="False">Link to Form</asp:LinkButton>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/FormElements.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderFormElements.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderElementValues.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Reorder Element Values </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/FormTemplate.aspx?edit=SL" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Exit Dashboard </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" alt="" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    <br />
    <table  cellpadding="3" cellspacing="0" class="contentContainer">
        <tr>
            <td valign="top">
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <div class="gridviewClass">
                                <asp:GridView ID="gvElements" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1"
                                    OnRowDataBound="gvElements_RowDataBound" AllowPaging="False" DataKeyNames="pk_formElementID"
                                    Width="600px" CssClass="gridviewClass" AllowSorting="false" OnRowCommand="GridView1_OnRowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                        <asp:BoundField DataField="label" HeaderText="Field Label" SortExpression="label" />
                                        <asp:BoundField DataField="Text" HeaderText="Default Text" SortExpression="Text" />
                                        <asp:BoundField DataField="name" HeaderText="Field Type" SortExpression="name" />
                                        <asp:BoundField DataField="fk_SLFormID" HeaderText="fk_SLFormID" SortExpression="fk_SLFormID" />
                                        <asp:BoundField DataField="CssClass" HeaderText="CssClass" SortExpression="CssClass" />
                                        <asp:BoundField DataField="pk_formElementID" HeaderText="pk_formElementID" SortExpression="pk_formElementID" />
                                        <asp:BoundField DataField="position" HeaderText="position" SortExpression="position" />
                                        <asp:CheckBoxField DataField="required" HeaderText="Required" SortExpression="required" />
                                        <asp:CommandField ShowSelectButton="True" />
                                                                    <asp:TemplateField HeaderText="Delete Form" >
                            <ItemTemplate><center><asp:LinkButton CommandName="deleteForm" CommandArgument="<%# Container.DataItemIndex %>" ID="deleteForm" runat="server" >Delete</asp:LinkButton></center></ItemTemplate>
                            </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="gridviewClass_Header" />
                                    <RowStyle CssClass="gridviewClass_TableRow" />
                                    <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div style="text-align: right;">
                                <asp:LinkButton ID="lnkAddNew" runat="server" Font-Size="10pt" OnClick="lnkAddNew_Click">Add New</asp:LinkButton>&nbsp;&nbsp;</div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <div runat="server" id="ElementEdit">
        <table  cellpadding="3" cellspacing="0">
            <tr>
                <td valign="top" style="width: 232px">
                    <asp:DetailsView DataKeyNames="pk_formElementID" ID="dvDetails" runat="server" AutoGenerateRows="False"
                        DataSourceID="FormElementDS2" OnModeChanged="dvDetails_modechanged" OnItemUpdating="dvDetails_ItemUpdating"
                        OnDataBound="dvDetails_OnItemDatabound" OnItemUpdated="dvDetails_ItemUpdated" AllowPaging="False"
                        Height="50px" Width="125px">
                        <Fields>
                            <asp:BoundField DataField="pk_formElementID" HeaderText="pk_formElementID" SortExpression="pk_formElementID" />
                            <asp:BoundField DataField="fk_SLFormID" HeaderText="fk_SLFormID" SortExpression="fk_SLFormID" />
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:BoundField DataField="label" HeaderText="label" SortExpression="label" />
                            <asp:BoundField DataField="Text" HeaderText="Text" SortExpression="Text" />
                            <asp:CheckBoxField DataField="required" HeaderText="required" SortExpression="required" />
                            <asp:BoundField DataField="CssClass" HeaderText="CssClass" SortExpression="CssClass" />
                            <asp:BoundField DataField="position" HeaderText="position" SortExpression="position" />
                            <asp:TemplateField HeaderText="Field Type" SortExpression="fk_typeID">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="dlTypeID" runat="server" DataTextField="name" DataValueField="pk_typeID"
                                        DataSourceID="FieldElementTypeDS">
                                    </asp:DropDownList>
                                    <!--<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fk_typeID") %>'></asp:TextBox> -->
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("fk_typeID") %>'></asp:TextBox>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:DropDownList ID="dlTypeID" runat="server" DataTextField="name" DataValueField="pk_typeID"
                                        DataSourceID="FieldElementTypeDS" Enabled="false">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lblEditValues" runat="server" Text="Manage Values" OnClick="lblEditValues_onClick"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Fields>
                        <HeaderStyle CssClass="gridviewClass_Header" />
                        <RowStyle CssClass="gridviewClass_TableRow" />
                        <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                    </asp:DetailsView>
                </td>
                <td style="width: 379px" valign="top" id="Td1" runat="server">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:GridView ID="gvMultiValueList" runat="server" OnRowDeleting="gvMultiValueList_RowDeleting"
                                    OnRowDataBound="gvMultiValueList_RowDataBound" AutoGenerateColumns="False" DataSourceID="FieldElementValue"
                                    AllowSorting="false" AllowPaging="False">
                                    <Columns>
                                        <asp:BoundField DataField="pk_valueID" HeaderText="pk_valueID" SortExpression="pk_valueID" />
                                        <asp:BoundField DataField="fk_typeID" HeaderText="fk_typeID" SortExpression="fk_typeID" />
                                        <asp:BoundField DataField="value" HeaderText="value" SortExpression="value" />
                                        <asp:BoundField DataField="fk_formElementID" HeaderText="fk_formElementID" SortExpression="fk_formElementID" />
                                        <asp:BoundField DataField="position" HeaderText="position" SortExpression="position" />
                                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                    </Columns>
                                    <HeaderStyle CssClass="gridviewClass_Header" />
                                    <RowStyle CssClass="gridviewClass_TableRow" />
                                    <AlternatingRowStyle CssClass="gridviewClass_TableRowAlternating" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" runat="server" id="gvElementValues_tableCell">
                                <div style="text-align: right;">
                                    <asp:LinkButton ID="lnkAddNewMultiValue" runat="server" Font-Size="10pt" OnClick="lnkAddNewMultiValue_Click"
                                        Visible="False">Add New</asp:LinkButton>&nbsp;&nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:ObjectDataSource ID="FormElementDS2" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="{0}"
        SelectMethod="GetDataByFormElementID" TypeName="Dealer_Locator.DA.FormElementTDSTableAdapters.DL_FormElementTableAdapter"
        UpdateMethod="UpdateQuery">
        <SelectParameters>
            <asp:ControlParameter ControlID="gvElements" DefaultValue="-1" Name="FormElementID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_formElementID" Type="Int32" />
            <asp:Parameter Name="fk_SLFormID" Type="Int32" />
            <asp:Parameter Name="ID" Type="String" />
            <asp:Parameter Name="label" Type="String" />
            <asp:Parameter Name="Text" Type="String" />
            <asp:Parameter Name="required" Type="Boolean" />
            <asp:Parameter Name="CssClass" Type="String" />
            <asp:Parameter Name="position" Type="Int32" />
            <asp:Parameter Name="fk_typeID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="pk_formElementID" Type="Int32" />
            <asp:Parameter Name="fk_SLFormID" Type="Int32" />
            <asp:Parameter Name="ID" Type="String" />
            <asp:Parameter Name="label" Type="String" />
            <asp:Parameter Name="Text" Type="String" />
            <asp:Parameter Name="required" Type="Boolean" />
            <asp:Parameter Name="CssClass" Type="String" />
            <asp:Parameter Name="position" Type="Int32" />
            <asp:Parameter Name="fk_typeID" Type="Int32" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dev %>"
        SelectCommand="SELECT ID, [name], label,  Text, fk_SLFormID, CssClass, pk_formElementID, position, required FROM [DL.FormElement], [DL.ElementType]&#13;&#10;WHERE (fk_SLFormID = @FormID)&#13;&#10;AND [DL.ElementType].pk_typeID = fk_typeID"
        DeleteCommand="DELETE FROM [DL.FormElement] WHERE pk_formElementID = @FormID">
        <SelectParameters>
            <asp:SessionParameter Name="FormID" SessionField="formID" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="formID" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:Label ID="lblTypeID" runat="server" Visible="False"></asp:Label>
    <asp:ObjectDataSource ID="FieldElementTypeDS" runat="server" InsertMethod="Insert"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.FieldElementTypeTDSTableAdapters.DL_ElementTypeTableAdapter">
        <InsertParameters>
            <asp:Parameter Name="pk_typeID" Type="Int32" />
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="isMultiValue" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Dev %>"
        SelectCommand="SELECT pk_typeID, name&#13;&#10;FROM [DL.ElementType], [DL.FormElement]&#13;&#10;WHERE [DL.ElementType].pk_typeID = [DL.FormElement].fk_typeID&#13;&#10;AND pk_formElementID = @formElementID&#13;&#10;">
        <SelectParameters>
            <asp:ControlParameter ControlID="gvElements" DefaultValue="-1" Name="formElementID"
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:ObjectDataSource ID="FieldElementValue" runat="server" DeleteMethod="DeleteQuery"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByFormElementID"
        TypeName="Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter"
        UpdateMethod="UpdateQuery">
        <DeleteParameters>
            <asp:Parameter Name="pk_valueID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="pk_valueID" Type="Int32" />
            <asp:Parameter Name="fk_typeID" Type="Int32" />
            <asp:Parameter Name="value" Type="String" />
            <asp:Parameter Name="fk_formElementID" Type="Int32" />
            <asp:Parameter Name="position" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="dvDetails" DefaultValue="-1" Name="FormElementID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_valueID" Type="Int32" />
            <asp:Parameter Name="fk_typeID" Type="Int32" />
            <asp:Parameter Name="value" Type="String" />
            <asp:Parameter Name="fk_formElementID" Type="Int32" />
            <asp:Parameter Name="position" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
