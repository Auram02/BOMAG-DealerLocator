<%@ Page Title="" Language="C#" MasterPageFile="~/DealerLocator.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="Dealer_Locator.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="/js/jquery-current.min.js"></script>
    <script type="text/javascript" src="/js/jquery-ui-1.8.4.custom.min.js"></script>
   <script type="text/javascript" src="/js/dock.interface.js"></script>
   
<style>



/* dock2 - bottom */
#dock2 {
	width: 100%;
	bottom: 0px;
	position: absolute;
	left: 0px;
}
.dock-container2 {
	position: absolute;
	height: 50px;
	background: url(images/dock-bg.gif);
	padding-left: 20px;
}
a.dock-item2 {
	display: block; 
	font: bold 12px Arial, Helvetica, sans-serif;
	width: 40px; 
	color: #000; 
	bottom: 0px; 
	position: absolute;
	text-align: center;
	text-decoration: none;
}
.dock-item2 span {
	display: none;
	padding-left: 20px;
}
.dock-item2 img {
	border: none; 
	margin: 5px 10px 0px; 
	width: 100%; 
}
</style>

<!--bottom dock -->
<div class="dock" id="dock2">
  <div class="dock-container2">
	  <a class="dock-item2" href="#"><span>Home</span><img src="/images/dock/hypac.png" alt="home" /></a> 
	  <a class="dock-item2" href="#"><span>Contact</span><img src="/images/dock/recycler.png" alt="contact" /></a> 
	  <a class="dock-item2" href="#"><span>Portfolio</span><img src="/images/dock/refuse.png" alt="portfolio" /></a> 
	  <a class="dock-item2" href="#"><span>Music</span><img src="/images/dock/pavers.png" alt="music" /></a> 
	  <a class="dock-item2" href="#"><span>Video</span><img src="/images/dock/milling.png" alt="video" /></a> 
	  <a class="dock-item2" href="#"><span>History</span><img src="/images/dock/light_equip.png" alt="history" /></a> 
	  <a class="dock-item2" href="#"><span>Calendar</span><img src="/images/dock/light-tandem.png" alt="calendar" /></a> 
	  <a class="dock-item2" href="#"><span>Links</span><img src="/images/dock/medium-tandem.png" alt="links" /></a> 
	  <a class="dock-item2" href="#"><span>RSS</span><img src="/images/dock/heavy-tandems.png" alt="rss" /></a> 
	  <a class="dock-item2" href="#"><span>RSS2</span><img src="/images/dock/single-drums.png" alt="rss" /></a> 
  </div>

</div>
<script type="text/javascript">

    $(document).ready(
		function () {

		    $('#dock2').Fisheye(
				{
				    maxWidth: 60,
				    items: 'a',
				    itemsText: 'span',
				    container: '.dock-container2',
				    itemWidth: 60,
				    proximity: 80,
				    alignment: 'left',
				    valign: 'bottom',
				    halign: 'center'
				}
			)
		}
	);

</script>


</asp:Content>
