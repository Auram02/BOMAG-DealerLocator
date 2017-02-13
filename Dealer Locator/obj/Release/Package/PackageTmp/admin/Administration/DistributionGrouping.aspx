<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="DistributionGrouping.aspx.cs" Inherits="Dealer_Locator.admin.DistributionGrouping" %>
<%@ MasterType VirtualPath="~/admin/Administration.Master" %>
<%@ Register Src="PhysicalDistributionControl.ascx" TagName="PhysicalDistributionControl" TagPrefix="DL" %>
<%@ Register Src="ElectronicDistributionControl.ascx" TagName="ElectronicDistributionControl" TagPrefix="DL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




<div id="tabs">
        <ul>
            <li><a href="#PhysicalDistributionTab"><span>Physical Distribution</span></a></li>
            <li><a href="#ElectronicDistributionTab"><span>Electronic Distribution</span></a></li>
            
        </ul>
        <div id="PhysicalDistributionTab">
            <DL:PhysicalDistributionControl id="PhysicalDistributionControl" runat="server" />
        </div>
        <div id="ElectronicDistributionTab">
            <DL:ElectronicDistributionControl id="ElectronicDistributionControl" runat="server" />
        </div>
        
    </div>
</asp:Content>
