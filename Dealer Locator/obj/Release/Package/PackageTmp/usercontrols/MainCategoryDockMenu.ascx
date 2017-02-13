<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainCategoryDockMenu.ascx.cs"
    Inherits="Dealer_Locator.usercontrols.MainCategoryDockMenu" %>
   
   <script type="text/javascript" src="/js/jquery.jqDock.min.js"></script>
   <script type="text/javascript" src="/js/dock.interface.js"></script>    <!-- http://www.ndesign-studio.com/demo/css-dock-menu/css-dock.html -->
   
   <script language="javascript" type="text/javascript">
       function SelectProductCategory(productCategory, productCategoryId) {

           $(".ProductCategorySelectedHiddenField").text(productCategory);
           $(".ProductCategoryIdSelectedHiddenField").text(productCategoryId);
           UpdateSearchCriteria();
       }
   
   </script>
   
   <asp:HiddenField ID="MenuIdHiddenField" runat="server" />
<div class="dock" id="dock2" runat="server">
  <div class="dock-container2"  id="dockcontainer" runat="server">
        <asp:PlaceHolder ID="DockMenuItems" runat="server" />
    </div>
</div>
