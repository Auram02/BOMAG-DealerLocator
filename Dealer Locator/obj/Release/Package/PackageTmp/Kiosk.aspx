<%@ Page Language="C#" MasterPageFile="~/DealerLocator.Master" AutoEventWireup="true"
    CodeBehind="Kiosk.aspx.cs" Inherits="Dealer_Locator.Kiosk" Title="Dealer Locator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        $('#<%= txtFormInput.ClientID %>').live('keydown', function (e) {
            var keyCode = e.keyCode || e.which;
            var key;
            var isCtrl;

            if (window.event) {
                key = window.event.keyCode;     //IE
                if (window.event.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }
            else {
                key = e.which;     //firefox
                if (e.ctrlKey)
                    isCtrl = true;
                else
                    isCtrl = false;
            }

            if (keyCode == 17 || isCtrl) {
                e.preventDefault();

                if (keyCode == 17) {

                    var currentValue = $("#<%= txtFormInput.ClientID %>").val();
                    $("#<%= txtFormInput.ClientID %>").val(currentValue + "\t");
                }
            }
        });

        function pageLoad() {

            $("#YesCardClick").hide();
            $("#YesCardReset").hide();
            $("#NoCardButton").show();
        }

        $("#<%= btnYesCard.ClientID %>").live("click", function (e) {
            e.preventDefault();

            $("#YesCardClick").show();
            $("#YesCardReset").show();
            $("#NoCardButton").hide();
            $(this).hide();
            $("#<%= txtFormInput.ClientID %>").val('');
            $("#<%= txtFormInput.ClientID %>").focus();
            

        });

    </script>
    <table width="500">
        <tr>
            <td colspan="2" align="center">
                DO YOU HAVE A CARD?
            </td>
        </tr>
        <tr valign="top">
            <td style="width: 250px" align="center">
                <asp:Button ID="btnYesCard" runat="server" Text="YES" Font-Size="Larger" Height="55px"
                    Width="133px" />
                <br />
                <div id="YesCardClick">
                    <asp:Label ID="lblScanInstructions" runat="server" ForeColor="Black">Please make sure your cursor is in the box below before scanning and continuing</asp:Label><br />
                    <asp:TextBox ID="txtFormInput" runat="server" Height="129px" TextMode="MultiLine"
                        Width="371px"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnProcessAndContinue" runat="server" Height="58px" OnClick="btnProcessAndContinue_Click"
                        Text="CONTINUE TO SALES LEAD FORM" Width="320px" />
                </div>
            </td>
            <td style="width: 250px" align="center" id="NoCardButton">
                <asp:Button ID="btnNoCard" runat="server" Text="NO" Font-Size="Larger" Height="55px"
                    Width="133px" OnClick="btnNoCard_Click" />
            </td>
        </tr>
        <tr id="YesCardReset">
            <td colspan="2" align="center">
                <br />
                <br />
                <br />
                <asp:Button ID="btnReset" runat="server" Text="RESET PAGE" Font-Size="Larger" Height="55px"
                    Width="133px" OnClick="btnReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
