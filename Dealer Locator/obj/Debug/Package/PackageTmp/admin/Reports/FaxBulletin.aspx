<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    Codebehind="FaxBulletin.aspx.cs" Inherits="Dealer_Locator.admin.Reports.FaxBulletin"
    Title="Dealer Locator Admin - Fax Bulletin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr style="background-color: #e1e1e1" valign="top">
            <td style="width: 478px">
                Fax Bulletin</td>
        </tr>
        <tr>
            <td style="width: 478px">
                <table style="border:1px solid black" cellspacing="0" cellpadding="5">
                    <tr align="left">
                        <td style="width: 115px; border:1px solid black">
                            Subject:</td>
                        <td style="border:1px solid black">
                            <br />
                            <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 115px; border:1px solid black">
                            Upload:<br />
                            (max of 3 files)</td>
                        <td style="border:1px solid black">
                            <br />
                            <asp:FileUpload ID="fu1" runat="server" />
                            <asp:FileUpload ID="fu2" runat="server" />
                            <asp:FileUpload ID="fu3" runat="server" /><br />
                        </td>
                    </tr>
                    <tr align="left">
                        <td style="width: 115px; border:1px solid black">
                            Distributor Type(s):&nbsp;</td>
                        <td style="border:1px solid black">
                            <br />
                            &nbsp;
                            <asp:CheckBoxList ID="cblCategories" runat="server" DataSourceID="ObjectDataSource1"
                                DataTextField="CategoryName" DataValueField="CategoryID">
                            </asp:CheckBoxList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" InsertMethod="Insert"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Dealer_Locator.DA.ContractTDSTableAdapters.CategoryTableAdapter">
                                <InsertParameters>
                                    <asp:Parameter Name="CategoryID" Type="Int32" />
                                    <asp:Parameter Name="CategoryName" Type="String" />
                                </InsertParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnSendFax" runat="server" Text="FAX" Width="73px" OnClick="btnSendFax_Click" /><br />
            </td>
        </tr>
    </table>
    <br />
    <asp:Literal ID="litResults" runat="server"></asp:Literal>
    <br />
    <asp:Literal ID="litStackTrace" runat="server"></asp:Literal><br />
</asp:Content>
