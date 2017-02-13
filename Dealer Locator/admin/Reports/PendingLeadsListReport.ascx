<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingLeadsListReport.ascx.cs" Inherits="Dealer_Locator.admin.Reports.PendingLeadsListReport" %>
   
<%@ Register Src="LeadList.ascx" TagName="LeadList" TagPrefix="uc1" %>
   
    <br />
    <div id="searchResults" runat="server">
        <table>
            <tr>
                <td>
                    0-6 Months
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:LeadList ID="LeadList1" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    12 Months
                </td>
            </tr>
            <tr>
                <td>
                    <uc1:LeadList ID="LeadList2" runat="server" />
                </td>
            </tr>
        </table>
    </div>