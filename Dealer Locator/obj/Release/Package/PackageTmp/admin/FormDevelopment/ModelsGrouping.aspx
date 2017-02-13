<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="ModelsGrouping.aspx.cs" Inherits="Dealer_Locator.admin.ModelsGrouping" %>

    
<%@ Register Src="ManageCategoriesControl.ascx" TagName="ManageCategoriesControl"
    TagPrefix="DL" %>
<%@ Register Src="ManageModelsControl.ascx" TagName="ManageModelsControl"
    TagPrefix="DL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#ManageCategoriesTab"><span>Manage Categories</span></a></li>
            <li><a href="#ManageModelsTab"><span>Manage Models</span></a></li>
        </ul>
        <div id="ManageCategoriesTab">
            <DL:ManageCategoriesControl id="ManageCategoriesControl" runat="server" />
        </div>
        <div id="ManageModelsTab">
            <DL:ManageModelsControl id="ManageModelsControl" runat="server" />
        </div>
    </div>
</asp:Content>
