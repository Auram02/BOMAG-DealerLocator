<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="FormsGrouping.aspx.cs" Inherits="Dealer_Locator.admin.Forms" %>

<%@ Register Src="../Administration/DefaultSalesLeadControl.ascx" TagName="DefaultSalesLeadControl"
    TagPrefix="DL" %>
<%@ Register Src="CopyFormControl.ascx" TagName="CopyFormControl"
    TagPrefix="DL" %>
<%@ Register Src="FormTemplateControl.ascx" TagName="FormTemplateControl"
    TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tabs">
        <ul>
            <li><a href="#SetDefaultTab"><span>Set Default</span></a></li>
            <li><a href="#FormEditTab"><span>Form Editor</span></a></li>
            <li><a href="#FormDupTab"><span>Form Duplicator</span></a> </li>
        </ul>
        <div id="SetDefaultTab">
            <DL:defaultsalesleadcontrol id="DefaultSalesLeadControl1" runat="server" />
        </div>
        <div id="FormEditTab">
            <DL:formtemplatecontrol id="FormTemplateControl1" runat="server" />
        </div>
        <div id="FormDupTab">
            <DL:copyformcontrol id="CopyFormControl1" runat="server" />
        </div>
    </div>
</asp:Content>
