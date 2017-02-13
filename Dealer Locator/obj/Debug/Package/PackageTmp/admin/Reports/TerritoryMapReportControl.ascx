<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TerritoryMapReportControl.ascx.cs" Inherits="Dealer_Locator.admin.TerritoryMapReportControl" %>
<div id="TMReportWrapper">
    <div id="TMReportTM" class="TMReportWrapper">




        <div class="Title OverviewTitle">Overview Report</div>
        <div id="TMProductLinesOverviewWrapper" class="TMReport2">
            <span class="label">Select Product Line(s): </span><span>
                <asp:ListBox ID="TMProductLinesOverviewList" CssClass="TMProductLinesOverviewList" runat="server" SelectionMode="Multiple"></asp:ListBox></span>
        </div>


        <div id="TMOverviewReportWrapper">
            <span>
                <asp:Button Text="Generate Overview Report" runat="server" ID="GenerateOverviewReportButton" CssClass="GenerateReportsButton GenerateOverviewReportButton" /></span>

        </div>

        <div class="Title OverviewTitle">TM Reports</div>
        <div id="TMListWrapper" class="TMReport">
            Select Territory Manager:
    <asp:DropDownList ID="TMList" CssClass="TMList" runat="server">

        <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>

    </asp:DropDownList>

        </div>
    </div>

    <div id="TMMapReport1" class="TMReportWrapper">
        <div class="Title">Region Map Report</div>
        <div id="TMStatesWrapper1" class="TMReport2">
            <span class="label">Select State: </span><span>
                <select id="StatesList1" class="StatesList1" runat="server" multiple></select>
            </span>

        </div>

        <div id="TMProductLinesWrapper1" class="TMReport2">
            <span class="label">Select Product Line: </span><span>
                <asp:DropDownList ID="ProductLinesList1" CssClass="ProductLinesList1" runat="server"></asp:DropDownList></span>
        </div>
        <div id="TMReport1GenerateWrapper" class="TMReportButtonWrapper">
            <asp:Button Text="Generate Report" runat="server" ID="GenerateReportButton1" CssClass="GenerateReportsButton GenerateReportButton1" />
        </div>
    </div>

    <div id="TMMapReport2" class="TMReportWrapper">
        <div class="Title">Distributor Map Report</div>
        <div id="TMDistributorWrapper2" class="TMReport2">
            <span class="label">Select Distributor: </span><span>
                <select id="DistributorList2" class="DistributorList2">
                </select>
            </span>

        </div>
        <div id="TMProductLinesWrapper2" class="TMReport2">
            <span class="label">Select Product Line: </span>
            <span>
                <select id="ProductLinesList2" class="ProductLinesList2" multiple>
                </select>
            </span>
        </div>
        <div id="TMReport2GenerateWrapper" class="TMReportButtonWrapper">
            <asp:Button Text="Generate Report" runat="server" ID="GenerateReportButton2" CssClass="GenerateReportsButton GenerateReportButton2" />
        </div>

    </div>
</div>
<div id="ReportLoadingDialog">
    <span id="OverviewReportPleaseWaitWrapper">
        <span>Your report will open in a new window/tab when ready.  This dialog will then close, please wait.  </span>
        <div id="SpinnerWrapper">
            <img src="/images/spinner.gif"><div>Generating Report...</div>
        </div>
    </span>
</div>
