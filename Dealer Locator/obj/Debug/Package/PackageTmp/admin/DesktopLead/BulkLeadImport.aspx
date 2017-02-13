<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true" CodeBehind="BulkLeadImport.aspx.cs" Inherits="Dealer_Locator.admin.DesktopLead.BulkLeadImport" %>
<%@ Register Src="~/admin/DataImport/ImportSalesLeadInformationControl.ascx" TagName="ImportSalesLeadInformationControl"
    TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <DL:ImportSalesLeadInformationControl ID="ImportSalesLeadInformationControlUserControl1"
            runat="server" />
</asp:Content>
