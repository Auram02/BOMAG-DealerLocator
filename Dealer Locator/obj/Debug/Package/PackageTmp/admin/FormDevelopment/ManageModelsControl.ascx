<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageModelsControl.ascx.cs"
    Inherits="Dealer_Locator.admin.Admin.ManageModelsControl" %>
<%@ Register Src="AddModifyModelControl.ascx" TagName="AddModifyModelControl" TagPrefix="DL" %>
<%@ Register Src="ReAssign/ReassignModelControl.ascx" TagName="ReassignModelControl" TagPrefix="DL" %>
<%@ Register Src="Reorder/ReorderModelsControl.ascx" TagName="ReorderModelsControl" TagPrefix="DL" %>
<div id="tabs3">
    <ul>
        <li><a href="#AddModifyTab"><span>Add / Modify</span></a></li>
        <li><a href="#ReorderTab"><span>Reorder</span></a></li>
        <li><a href="#ReAssignTab"><span>ReAssign</span></a></li>
    </ul>
    <div id="AddModifyTab">
        <DL:AddModifyModelControl ID="AddModifyModelControl" runat="server" />
    </div>
    <div id="ReorderTab">
        <DL:ReorderModelsControl id="ReorderModelsControl" runat="server" />
    </div>
    <div id="ReAssignTab">
        <DL:ReassignModelControl ID="ReassignModelControl" runat="server" />
    </div>
</div>
