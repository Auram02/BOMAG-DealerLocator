<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainCategoryCarouselUserControl.ascx.cs" Inherits="Dealer_Locator.usercontrols.WebUserControl1" %>


    <link type="text/css" href="/css/jquery.bxslider.css" media="screen" rel="stylesheet" />
<script src="/js/jquery.bxslider.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {

        //$("#MainCategoryCarousel").carouFredSel();
        /*	CarouFredSel: a circular, responsive jQuery carousel.
	Configuration created by the "Configuration Robot"
	at caroufredsel.dev7studios.com
*/

        //$("#foo2").carouFredSel({
        //    circular: false,
        //    infinite: false,
        //    auto: false,
        //    prev: {
        //        button: "#foo2_prev",
        //        key: "left"
        //    },
        //    next: {
        //        button: "#foo2_next",
        //        key: "right"
        //    },
        //    pagination: "#foo2_pag"
        //});

        //$("#MainCategoryCarousel").carouFredSel(
        //        {
        //            auto: false,
        //            prev: "#carousel-prev-arrow",
        //            next: "#carousel-next-arrow",
        //            height: 200,
        //            pagination: "#foo3_pag",
        //            infinite: false,
        //            circular: false,
        //            items: {
        //                visible: 5
        //            }
        //        });

        
        
        var minSlides = 2;
        var maxSlides = 5;

        if ($(document).width() < 768) {
            minSlides = 1;
            maxSlides = 1;
        }

        var carousel = $('.MainCategoryCarousel').bxSlider({
            infiniteLoop: false,
            hideControlOnEnd: true,
            minSlides: minSlides,
            maxSlides: maxSlides,
            slideWidth: 160,
            slideMargin: 10
        });


        window.carousel = carousel;
    });

</script>

<div class="MainCategoryCarouselWrapper image_carousel">

    <asp:PlaceHolder ID="MainCategoryItems" runat="server" />
    <div class="clearfix"></div>

    <div id="carousel-prev-arrow-wrapper"><a class="prev" id="carousel-prev-arrow" href="#"><span>prev</span></a></div>
    <div id="carousel-next-arrow-wrapper"><a class="next" id="carousel-next-arrow" href="#"><span>next</span></a></div>
    <div class="pagination" id="foo3_pag"></div>
</div>

<div>
</div>
