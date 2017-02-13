<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImportGrouping.ascx.cs"
    Inherits="Dealer_Locator.admin.DataImport.ImportGrouping" %>

<%@ Register Src="~/admin/DataImport/ImportDealerInformation.ascx" TagName="ImportDealerInformation"
    TagPrefix="DL" %>
<%@ Register Src="~/admin/DataImport/ZipCodeImport.ascx" TagName="ZipCodeImport"
    TagPrefix="DL" %>
<div id="tabs2">
    <ul>
        <li><a href="#ImportDealerInformation"><span>DDA Import</span></a></li>
        <li><a href="#ZipCodeImport"><span>Zip Code Import</span></a></li>
    </ul>

    <div id="ImportDealerInformation">
        <DL:ImportDealerInformation ID="ImportDealerInformationUserControl1" runat="server" />
    </div>
    <div id="ZipCodeImport">
        <DL:ZipCodeImport ID="ZipCodeImportUserControl1" runat="server" />
    </div>
</div>
