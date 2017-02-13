<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExcelDownload.ascx.cs" Inherits="Dealer_Locator.admin.DataExport.ExcelDownload" %>
   <link rel="Stylesheet" type="text/css" href="/admin/Styles/ExcelDownloads.css" />
    <br />

    <script language="javascript" type="text/javascript">

        $("#<%= StartDate.ClientID %>").live("focus", function () {
            $(this).datepicker();
        });

        $("#<%= EndDate.ClientID %>").live("focus", function () {
            $(this).datepicker();
        });
    
    </script>

    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr style="background-color: #e1e1e1" valign="top">
            <td style="width: 478px">
                Select Date Range
            </td>
        </tr>
        <tr>
            <td style="width: 478px">
                <br />
                Date Range:
                <asp:DropDownList ID="cboUseDateRange" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboUseDateRange_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <div class="CalendarEntry" id="CalendarEntryStart" runat="server">
                    <div class="DisplayInline">
                        <div class="CalendarRangeType">
                            Start:
                        </div>
                    </div>
                    <div class="DisplayInline">
                        <asp:TextBox ID="StartDate" runat="server" CssClass="CalendarTextbox" /></div>
                    <div class="DisplayInline">
                        <img alt="Popup Calendar" src="../Images/Calendar_scheduleHS.png" id="CalendarStartButton"
                            runat="server" /></div>
                </div>
                <div class="CalendarEntry" id="CalendarEntryEnd" runat="server">
                    <div class="DisplayInline">
                        <div class="CalendarRangeType">
                            End:
                        </div>
                    </div>
                    <div class="DisplayInline">
                        <asp:TextBox ID="EndDate" runat="server" CssClass="CalendarTextbox" />
                    </div>
                    <div class="DisplayInline">
                        <img src="../Images/Calendar_scheduleHS.png" id="CalendarEndButton"
                            runat="server" /></div>
                    <div class="DisplayInline">
                    </div>
                </div>

            </td>
        </tr>
    </table>
<%--                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="StartDate"
                        PopupButtonID="CalendarStartButton" />
                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="EndDate"
                            PopupButtonID="CalendarEndButton" />--%>

    <br />
    &nbsp;<table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr>
            <td>
                <table cellpadding="3" cellspacing="0" style="border-right: black 1px solid; border-top: black 1px solid;
                    border-left: black 1px solid; border-bottom: black 1px solid">
                    <tr style="background-color: #e1e1e1" valign="top">
                        <td style="width: 472px; height: 30px">
                            MAIN CATEGORY
                            <asp:DropDownList ID="cboMainCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboMainCategory_SelectedIndexChanged"
                                Width="351px">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="background-color: #e1e1e1" valign="top">
                        <td style="width: 472px; height: 30px">
                            SUB CATEGORY
                            <asp:DropDownList ID="cboSubCategory" runat="server" AutoPostBack="True" Width="351px"
                                OnSelectedIndexChanged="cboSubCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="background-color: #e1e1e1" valign="top">
                        <td style="width: 472px; height: 30px">
                            PRODUCT MODEL&nbsp;<asp:DropDownList ID="cboModel" runat="server" AutoPostBack="True"
                                Width="351px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnGenerateExcel" runat="server" Text="Generate Excel" OnClick="btnGenerateExcel_Click" /><br />
                <br />
                <asp:Label ID="lblResult" runat="server" Font-Names="Tecumseh MS,Arial" Font-Size="Small"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="MainCategoryDS" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData_nonDisabled" TypeName="Dealer_Locator.DA.MainCategoryTDSTableAdapters.DL_MainCategoryTableAdapter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SubCatTDS" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID" TypeName="Dealer_Locator.DA.SubCategoryTDS2TableAdapters.DL_SubCategoryTableAdapter"
        UpdateMethod="UpdateQuery">
        <UpdateParameters>
            <asp:Parameter Name="pk_subCatID" Type="Int32" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
            <asp:Parameter Name="categoryName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboMainCategory" DefaultValue="0" Name="MainCatID"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="pk_subCatID" Type="Int32" />
            <asp:Parameter Name="fk_mainCatID" Type="Int32" />
            <asp:Parameter Name="categoryName" Type="String" />
            <asp:Parameter Name="disable" Type="Boolean" />
            <asp:Parameter Name="position" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ModelDS" runat="server" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetDataByMainCatID_SubCatID" TypeName="Dealer_Locator.DA.ModelTDSTableAdapters.DL_ModelTableAdapter"
        UpdateMethod="UpdateQuery">
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
            <asp:ControlParameter ControlID="cboSubCategory" DefaultValue="-1" Name="subCatID"
                PropertyName="SelectedValue" Type="Int32" />
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