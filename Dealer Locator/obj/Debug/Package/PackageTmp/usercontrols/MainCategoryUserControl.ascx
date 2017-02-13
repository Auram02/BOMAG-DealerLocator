<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainCategoryUserControl.ascx.cs" Inherits="Dealer_Locator.usercontrols.MainCategoryUserControl" %>
   <script language="javascript" type="text/javascript">
       function SelectProductCategory(productCategory, productCategoryId) {

           $(".ProductCategorySelectedHiddenField").text(productCategory);
           $(".ProductCategoryIdSelectedHiddenField").text(productCategoryId);
           
       }

       function ResetProductCategory() {
           $(".ProductCategorySelectedHiddenField").text('');
           $(".ProductCategoryIdSelectedHiddenField").text('');
           
       }

       $(document).ready(function () {
           //.msDropDown({ roundedCorner: false, visibleRows: 4 });
           var ddslick = $('.MainCategory-htmlselect').ddslick({
               selectText: "Product Category",
               onSelected: function (data) {
                   console.log(data);
                   if (data.selectedData.value > -1)
                       SelectProductCategory(data.selectedData.text, data.selectedData.value);
                   else
                       ResetProductCategory();
               },
               height: '250px',
               width: '275px'
           });

           $(".dd-container").each(function () {
               $(this).find('li:first').hide();
           });


       });

       function pageLoad() {

           $('.dd-option-text').css('line-height', '1em');
           $("ul li label").css('line-height', '1.0em');
           
       }


   </script>

<div class="MainCategoryDropdownWrapper">
    
        <asp:PlaceHolder ID="MainCategoryItems" runat="server" />
     
</div>
