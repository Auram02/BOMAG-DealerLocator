<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReorderModelsControl.ascx.cs"
    Inherits="Dealer_Locator.admin.FormDevelopment.ReOrder.ReorderModelsControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="navHeader contentHeader">
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
            <table style="border: 1px black solid;" cellspacing="0" cellpadding="3">
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
                            <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged"
                                Width="203px">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </center>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #404FAC; color: #FFFFFF; width: 228px;">
                        <center>
                            SUB CATEGORY</center>
                    </td>
                </tr>
                <tr>
                    <td style="background-color: #E0E0E0; width: 228px;">
                        <center>
                            <br />
                            <asp:DropDownList ID="cboSubCategory" runat="server" AutoPostBack="True" Width="203px">
                            </asp:DropDownList>
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
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="reorderListDemo">
                                            <cc1:ReorderList ID="ReorderList1" runat="server" AllowReorder="True" DataSourceID="sqlModels"
                                                PostBackOnReorder="False" SortOrderField="position" CallbackCssStyle="callbackStyle"
                                                DragHandleAlignment="Left">
                                                <ItemTemplate>
                                                    <div class="itemArea">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("modelName") %>'></asp:Label>
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
                            <br />
                        </center>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:SqlDataSource ID="sqlModels" runat="server" ConnectionString="<%$ ConnectionStrings:Dev %>"
    SelectCommand="SELECT        fk_subCatID,  pk_modelID, modelName, disable, position, modelUrl,fk_mainCatID
FROM            [DL.Model]
WHERE        (fk_subCatID = @subCatID) AND (fk_mainCatID = @mainCatID) AND (disable = 'false')
ORDER BY position" UpdateCommand="UPDATE    [DL.Model]
SET              fk_subCatID = @fk_subCatID, pk_modelID = @pk_modelID, modelName = @modelName, disable = @disable, position = @position, 
                      modelUrl = @modelUrl, fk_mainCatID = @fk_mainCatID
WHERE     (pk_modelID = @pk_modelID)">
    <UpdateParameters>
        <asp:Parameter Name="fk_subCatID" Type="Int32" />
        <asp:Parameter Name="pk_modelID" Type="Int32" />
        <asp:Parameter Name="modelName" Type="String" />
        <asp:Parameter Name="disable" Type="Boolean" />
        <asp:Parameter Name="position" Type="Int32" />
        <asp:Parameter Name="modelUrl" Type="String" />
        <asp:Parameter Name="fk_mainCatID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="cboSubCategory" Name="subCatID" PropertyName="SelectedValue"
            Type="Int32" DefaultValue="-1" />
        <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="-1" Name="mainCatID"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:ObjectDataSource ID="objModels" runat="server" OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetDataByMainCatID_SubCatID" TypeName="Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter"
    UpdateMethod="UpdateQuery1" ConflictDetection="OverwriteChanges">
    <UpdateParameters>
        <asp:Parameter Name="fk_subCatID" Type="Int32" />
        <asp:Parameter Name="pk_modelID" Type="Int32" />
        <asp:Parameter Name="modelName" Type="String" />
        <asp:Parameter Name="disable" Type="Boolean" />
        <asp:Parameter Name="position" Type="Int32" />
        <asp:Parameter Name="modelUrl" Type="String" />
        <asp:Parameter Name="fk_mainCatID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:ControlParameter ControlID="cboSubCategory" Name="subCatID" PropertyName="SelectedValue"
            Type="Int32" DefaultValue="-1" />
        <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="-1" Name="mainCatID"
            PropertyName="SelectedValue" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
