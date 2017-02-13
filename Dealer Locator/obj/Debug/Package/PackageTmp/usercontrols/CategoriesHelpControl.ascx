<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoriesHelpControl.ascx.cs"
    Inherits="Dealer_Locator.usercontrols.CategoriesHelpControl" %>
<script type="text/javascript">
    $(document).ready(function () {

        // Accordion
        $("#<%=accordion.ClientID %>").accordion({ header: ".ProductTypesAccordionHeading" });
        //$("#<%=accordion.ClientID %>").accordion("option", "icons", false);
        $("#<%=accordion.ClientID %>").accordion({ icons: { 'header': 'accordion-arrow-inactive', 'headerSelected': 'accordion-arrow-active'} });

        $("#<%=accordion.ClientID %>").accordion({
            autoHeight: false,
            navigation: true
        });
        $("#<%=accordion.ClientID %>").accordion({
            collapsible: true
        });

        $(".SubCategoryListItem ul").hide();
        //        $(".SubCategoryListItem").hide();

        $(".SubCategoryListItem").on("click", function () {

            if (jQuery(this).find("ul").is(":visible") == true) {
                // jQuery(this).find("ul").hide();
            } else {
                jQuery(this).find("ul").slideDown();
                jQuery(this).css( 'cursor', 'default' );
            }

            //            $(".SubCategoryListItem ul").show();

        });

    });

</script>
<asp:PlaceHolder ID="ProductCategoryCSS" runat="server" />
<div id="accordion" class="accordion" runat="server">
    <asp:PlaceHolder ID="ProductCategoryHelp" runat="server" />
</div>
