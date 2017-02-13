<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReorderSubCategoriesControl.ascx.cs"
    Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderSubCategoriesControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="navHeader contentHeader" cellpadding="3">
    <tr>
        <td>
            Category Dashboard
        </td>
    </tr>
</table>
<br />
<br />
<table cellpadding="3" cellspacing="0" class="contentContainer">
    <tr>
        <td align="center" style="border-right: medium none; width: 400px" valign="top">
            <center>
                <table style="border: 1px black solid;" cellspacing="0" cellpadding="0">
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>
                                MAIN CATEGORY</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #E0E0E0; width: 228px;">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" Width="203px" />
                                <br />
                                <br />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                            <center>
                                REORDER ITEMS ACTION</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #D4D7EA;">
                            <center>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="reorderListDemo">
                                            <cc1:reorderlist id="ReorderList1" runat="server" allowreorder="True" datasourceid="objSubCat"
                                                postbackonreorder="False" sortorderfield="position" callbackcssstyle="callbackStyle"
                                                draghandlealignment="Left">
                                                    <ItemTemplate>
                                                        <div class="itemArea">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("categoryName") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <DragHandleTemplate>
                                                        <div class="dragHandle">
                                                        </div>
                                                    </DragHandleTemplate>
                                                    <ReorderTemplate>
                                                        <asp:Panel ID="Panel2" runat="server" CssClass="reorderCue" />
                                                    </ReorderTemplate>
                                                </cc1:reorderlist>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </center>
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;</center>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="objSubCat" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataByMainCatID_nonDisabled" TypeName="Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter"
    UpdateMethod="UpdateQuery">
    <UpdateParameters>
        <asp:Parameter Name="pk_subCatID" Type="Int32" />
        <asp:Parameter Name="fk_mainCatID" Type="Int32" />
        <asp:Parameter Name="categoryName" Type="String" />
        <asp:Parameter Name="disable" Type="Boolean" />
        <asp:Parameter Name="position" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="cboMainCategory" Name="MainCatID" PropertyName="SelectedValue"
            Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="pk_subCatID" Type="Int32" />
        <asp:Parameter Name="fk_mainCatID" Type="Int32" />
        <asp:Parameter Name="categoryName" Type="String" />
        <asp:Parameter Name="disable" Type="Boolean" />
        <asp:Parameter Name="position" Type="Int32" />
    </InsertParameters>
</asp:ObjectDataSource>
