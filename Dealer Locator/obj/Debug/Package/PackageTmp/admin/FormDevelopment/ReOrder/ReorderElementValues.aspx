<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ReorderElementValues.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderElementValues"
    Title="Dealer Locator Admin - Reorder Element Values" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table class="navHeader contentHeader" cellpadding="3">
        <tr>
            <td>
                FORM DASHBOARD:
                <asp:Literal ID="litFormName" runat="server" />
            </td>
        </tr>
    </table>
    <br />
    <div>
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
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/FormElements.aspx" style="text-decoration: none;"><font
                                            style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                            Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderFormElements.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Reorder Form Elements </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg2.gif" bgcolor="#ffffff">
                                        <a href="/admin/FormDevelopment/ReOrder/ReorderElementValues.aspx" style="text-decoration: none;">
                                            <font style="text-decoration: none;" color="#000000" face="arial,sans-serif" size="2">
                                                Reorder Element Values </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r2.gif" bgcolor="#ffffff">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_l1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" />
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_bg1.gif" bgcolor="#eeeeee">
                                        <a href="/admin/FormDevelopment/FormTemplate.aspx?edit=SL" style="text-decoration: none;"><font style="text-decoration: none;"
                                            color="#000000" face="arial,sans-serif" size="2">Exit Dashboard </font></a>
                                    </td>
                                    <td background="../../../admin/Images/Tabs/ui_tab_r1.gif" bgcolor="#eeeeee">
                                        <img src="../../../admin/Images/Tabs/spacer.gif" border="0" height="20" width="2" /></td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <br />
    <table border="0"  cellpadding="3" cellspacing="0" class="contentContainer">
        <tr>
            <td align="center" style="border-right: medium none; width: 400px" valign="top">
                <table style="border: 1px black solid;" cellspacing="0"  cellpadding="3">
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>MultiValue Field</center></td>
                    </tr>
                    <tr>
                        <td style="background-color: #E0E0E0; width: 228px;">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboFormElements" runat="server" OnSelectedIndexChanged="cboFormElements_SelectedIndexChanged"
                                    Width="203px" AutoPostBack="True">
                                </asp:DropDownList><br />
                                <br />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>REORDER ITEMS ACTION</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #D4D7EA;">
                            <center>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <div class="reorderListDemo">
                                                <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="objFormElements"
                                                    PostBackOnReorder="False" SortOrderField="position" CallbackCssStyle="callbackStyle"
                                                    DragHandleAlignment="Left">
                                                    <ItemTemplate>
                                                        <div class="itemArea">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("value") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <DragHandleTemplate>
                                                        <div class="dragHandle">
                                                        </div>
                                                    </DragHandleTemplate>
                                                    <ReorderTemplate>
                                                        <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                                    </ReorderTemplate>
                                                </cc1:ReorderList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="objFormElements" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByFormElementID" TypeName="Dealer_Locator.DA.ElementValueTDSTableAdapters.DL_ElementValueTableAdapter"
        UpdateMethod="UpdateQuery" InsertMethod="Insert">
        <UpdateParameters>
            <asp:Parameter Name="pk_valueID" Type="Int32" />
            <asp:Parameter Name="fk_typeID" Type="Int32" />
            <asp:Parameter Name="value" Type="String" />
            <asp:Parameter Name="fk_formElementID" Type="Int32" />
            <asp:Parameter Name="position" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboFormElements" Name="FormElementID" PropertyName="SelectedValue"
                Type="Int32" />
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
