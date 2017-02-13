<%@ Page Language="C#" MasterPageFile="~/admin/Administration.Master" AutoEventWireup="true"
    CodeBehind="LatLongReport.aspx.cs" Inherits="Dealer_Locator.admin.Reports.LatLongReport"
    Title="Dealer Locator Admin - Latitude / Longitude Lookup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <font size="4" style="font-weight: bold;" color="#3D51AA">DATA IMPORT</font>
    <br />
    <br />
    <table cellpadding="3" cellspacing="0" class="contentContainer">
        <tr align="center">
            <td style="width: 248px">
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            State
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:DropDownList ID="cboState" runat="server" Width="160px" OnSelectedIndexChanged="cboState_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            City
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            <asp:DropDownList ID="cboCity" runat="server" Width="160px" OnSelectedIndexChanged="cboCity_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
                <table border="1" cellpadding="3">
                    <tr>
                        <td class="dlHeader" style="width: 520px">
                            Latitude / Longitude
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 520px">
                            Latitude:&nbsp;
                            <asp:Label ID="lblLatitude" runat="server"></asp:Label>
                            <br />
                            Longitude:
                            <asp:Label ID="lblLongitude" runat="server"></asp:Label>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
