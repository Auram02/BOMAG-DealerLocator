<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="ReassignModels.aspx.cs" Inherits="Dealer_Locator.admin.FormDevelopment.ReAssign.ReassignModels"
    Title="Dealer Locator Admin - Reassign Product Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label><br />
    <br />
    <table>
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" style="border-right: black 1px solid; border-top: black 1px solid;
                    border-left: black 1px solid; border-bottom: black 1px solid">
                    <tr>
                        <td style="width: 228px; color: #ffffff; background-color: #404fac">
                            <center>
                                MAIN CATEGORY</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 228px; background-color: #e0e0e0">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged"
                                    Width="203px">
                                </asp:DropDownList><br />
                                <br />
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 228px; color: #ffffff; background-color: #404fac">
                            <center>
                                SUB CATEGORY</center>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 228px; background-color: #e0e0e0">
                            <center>
                                <br />
                                <asp:DropDownList ID="cboSubCategory" runat="server" AutoPostBack="True" Width="203px">
                                </asp:DropDownList><br />
                                <br />
                            </center>
                        </td>
                        <tr>
                            <td style="width: 228px; color: #ffffff; background-color: #404fac">
                                <center>
                                    MODEL</center>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 228px; background-color: #e0e0e0">
                                <center>
                                    <br />
                                    <br />
                                    <asp:CheckBoxList ID="lstModels" runat="server" DataSourceID="objModels" DataTextField="modelName"
                                        DataValueField="pk_modelID" Height="122px" Width="179px">
                                    </asp:CheckBoxList><br />
                                    &nbsp;</center>
                            </td>
                        </tr>
                        <tr>
                </table>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;
                ReAssign To ==>
                &nbsp;&nbsp;&nbsp;</td>
            <td valign="top">

                            <table cellpadding="3" cellspacing="0" style="border-right: black 1px solid; border-top: black 1px solid;
                                border-left: black 1px solid; border-bottom: black 1px solid" style="vertical-align: top;">
                                <tr>
                                    <td style="width: 228px; color: #ffffff; background-color: #404fac">
                                        <center>
                                            MAIN CATEGORY</center>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 228px; background-color: #e0e0e0">
                                        <center>
                                            <br />
                                            <asp:DropDownList ID="cboNewMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged"
                                                Width="203px">
                                            </asp:DropDownList><br />
                                            <br />
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 228px; color: #ffffff; background-color: #404fac">
                                        <center>
                                            SUB CATEGORY</center>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 228px; background-color: #e0e0e0">
                                        <center>
                                            <br />
                                            <asp:DropDownList ID="cboNewSubCategory" runat="server" AutoPostBack="True" Width="203px">
                                            </asp:DropDownList><br />
                                            <br />
                                        </center>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnReassign" runat="server" OnClick="btnReassign_Click" Text="ReAssign Model(s)" /></td>
                                </tr>
                            </table>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="objModels" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID_SubCatID" TypeName="Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter"
        UpdateMethod="UpdateQuery" InsertMethod="Insert">
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
                Type="Int32" DefaultValue="" />
            <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="-1" Name="mainCatID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_modelID" Type="Int32" />
            <asp:Parameter Name="fk_subCatID" Type="Int32" />
            <asp:Parameter Name="modelName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
            <asp:Parameter Name="modelUrl" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
