<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ElectronicDistributionControl.ascx.cs"
    Inherits="Dealer_Locator.admin.ElectronicDistributionUserControl" %>
<%@ Register Src="RentalAdministratorEmailControl.ascx" TagName="RentalAdministratorControl" TagPrefix="DL" %>
<%@ Register Src="DoNotPlanToBuyControl.ascx" TagName="DoNotPlanToBuyControl" TagPrefix="DL" %>
<%@ Register Src="LocationsControl.ascx" TagName="LocationsControl" TagPrefix="DL" %>
<div id="tabs2">
    <ul>
        <li><a href="#LocationsTab"><span>Locations</span></a></li>
        <li><a href="#DoNotPlanToBuyTab"><span>Do Not Play To Buy</span></a></li>
        <li><a href="#RentalAdministratorTab"><span>Rental Administrator</span></a></li>        
    </ul>
    <div id="LocationsTab">
        <DL:LocationsControl ID="LocationsControl" runat="server" />
    </div>
    <div id="DoNotPlanToBuyTab">
        <DL:DoNotPlanToBuyControl ID="DoNotPlanToBuyControl" runat="server" />
    </div>
    <div id="RentalAdministratorTab">
        <DL:RentalAdministratorControl ID="RentalAdministratorControl" runat="server" />
    </div>
</div>
