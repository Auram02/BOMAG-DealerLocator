<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageCategoriesControl.ascx.cs"
    Inherits="Dealer_Locator.admin.Admin.ManageCategoriesControl" %>
<%@ Register Src="ReOrder/ReorderMainCategoriesControl.ascx" TagName="ReorderMainCategoriesControl"
    TagPrefix="DL" %>
<%@ Register Src="ReOrder/ReorderSubCategoriesControl.ascx" TagName="ReorderSubCategoriesControl"
    TagPrefix="DL" %>
<%@ Register Src="SubCategoriesControl.ascx" TagName="SubCategoriesControl" TagPrefix="DL" %>
<%@ Register Src="MainCategoryDockImageManagerControl.ascx" TagName="MainCategoryDockImageManagerControl"
    TagPrefix="DL" %>
<div id="tabs2">
    <ul>
        <li><a href="#ReorderMainCategoriesTab"><span>Reorder Main Categories</span></a></li>
        <li><a href="#SubCategoriesTab"><span>Sub Categories</span></a></li>
        <li><a href="#ReorderSubCategoriesTab"><span>Reorder Sub Categories</span></a></li>
        <li><a href="#MainCategoryDockMenuTab"><span>Main Category Dock Menu</span></a></li>
    </ul>
    <div id="ReorderMainCategoriesTab">
        <DL:ReorderMainCategoriesControl ID="ReorderMainCategoriesControl" runat="server" />
    </div>
    <div id="SubCategoriesTab">
        <DL:SubCategoriesControl ID="SubCategoriesControl" runat="server" />
    </div>
    <div id="ReorderSubCategoriesTab">
        <DL:ReorderSubCategoriesControl ID="ReorderSubCategoriesControl" runat="server" />
    </div>
    <div id="MainCategoryDockMenuTab">
        <DL:MainCategoryDockImageManagerControl ID="MainCategoryDockImageManagerControl"
            runat="server" />
    </div>
</div>
