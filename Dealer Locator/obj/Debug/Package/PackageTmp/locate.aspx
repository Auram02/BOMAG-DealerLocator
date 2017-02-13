<%@ Page Language="C#" MasterPageFile="~/DealerLocator.Master" AutoEventWireup="true"
    CodeBehind="locate.aspx.cs" Inherits="Dealer_Locator.locate" Title="Untitled Page" %>

<%@ Register Src="usercontrols/MainCategoryUserControl.ascx" TagName="MainCategoryUserControl"
    TagPrefix="DL" %>
<%@ Register Src="usercontrols/CategoriesHelpControl.ascx" TagName="CategoriesHelpControl"
    TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- google maps key 
       <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;key=ABQIAAAAgqdyjmO5_eFZguqMLo8_IRSX6kVpdh7m28mTRWGCCVFRikBZwxS_cpOv8bDRmNnP0qDWnhGvLu2z6Q"
      type="text/javascript"></script> -->
    <!-- // This is for findbomag.com
       
    key=ABQIAAAAgqdyjmO5_eFZguqMLo8_IRQU4fQL4VKJAZJkZLvvl6qn154OyRTdpJS2BJlJhLTq9cl653kGkcvF2w
      -->
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
        var geocoder;
        var map;

        function ShowMap(mapAddress, mapAddressDisplay) {

            try {
                var myOptions = {
                    zoom: 8,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                }

                map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
                geocoder = new google.maps.Geocoder();

                var address = mapAddress;

                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);

                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location,
                            title: mapAddressDisplay
                        });

                        var infoWindow = new google.maps.InfoWindow({
                            position: results[0].geometry.location,
                            content: mapAddressDisplay
                        });

                        google.maps.event.addListener(marker, 'click', function () {
                            infoWindow.open(map, marker);
                        });

                    } else {
                        alert("Geocode was not successful for the following reason: " + status);
                    }

                });


            } catch (err) {
                // do not show google maps if an error occurs, such as no internet connection
            }
        }

    </script>
    <link type="text/css" href="./css/locate.css" rel="stylesheet" />
    <script language="javascript">

        var currentItemIndex = 0;

        $(function () {
            function log(message) {
                $("<div/>").text(message).prependTo("#log");
                $("#log").attr("scrollTop", 0);
            }

            $("#<%= txtZip.ClientID %>").autocomplete({
                source: function (request, response) {
                    totalItems = 0;
                    autoCompleteItems = null;
                    startingItem = 0;

                    $.getJSON("ZipSearch.ashx", { term: request.term }, function (data) {

                        var suggestions = [];
                        currentItemIndex = 0;
                        autoCompleteItems = data;

                        $.each(data, function (i, val) {


                            totalItems += 1;

                            val.label = BuildDisplayValue(val);
                            suggestions.push(val);

                        });
                        response(suggestions);
                    });

                },
                minLength: 4,
                select: function (event, ui) {
                    //log(ui.item ? ("Selected: " + ui.item.zipcode + " - " + ui.item.city + " - " + ui.item.state) : "Nothing selected, input was " + this.value); // for debugging
                    this.value = this.value;  // retain the zip code entered.  On select, set the "selected" value elsewhere

                    $("input#<%= ZipSelectedHiddenField.ClientID %>").val(ui.item.zipcode);
                    $("input#<%= CitySelectedHiddenField.ClientID %>").val(ui.item.city);
                    $("input#<%= StateSelectedHiddenField.ClientID %>").val(ui.item.state);

                    $("#ZipSelectedCity").text(ui.item.city + ", " + ui.item.state + " (" + ui.item.zipcode + ")");

                    return false;
                }
            });

            //.data("autocomplete")._renderMenu = function (ul, items) {
            //    var self = this;

            //    $.each(items, function (index, item) {
            //        self._renderItem(ul, item);
            //    });


            //};

            $.ui.autocomplete.prototype._renderItem = function (ul, item) {
                var oldFn = $.ui.autocomplete.prototype._renderItem;

                currentItemIndex += 1;

                var rowClass = 'EvenRowSearchResults';
                if ((currentItemIndex % 2) == 1) {
                    rowClass = 'OddRowSearchResults';
                }

                return $("<li class='" + rowClass + "'></li>")
    				.data("item.autocomplete", item)
    				.append("<a>" + item.label + "</a>")
    				.appendTo(ul);
            };

            function BuildDisplayValue(val) {
                var displayValue = val.value
                return displayValue;
            }

            $("#<%= txtCityEntry.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.getJSON("CityStateSearch.ashx", {

                        term: request.term + ";" + $("#<%= cboState.ClientID %>").val()

                    }, response);
                },
                minLength: 3,

                select: function (event, ui) {
                    //log(ui.item ? ("Selected: " + ui.item.zipcode + " - " + ui.item.city + " - " + ui.item.state) : "Nothing selected, input was " + this.value); // for debugging
                    this.value = ui.item.city;  // retain the zip code entered.  On select, set the "selected" value elsewhere

                    $("input#<%= ZipSelectedHiddenField.ClientID %>").val(ui.item.zipcode);
                    $("input#<%= CitySelectedHiddenField.ClientID %>").val(ui.item.city);
                    $("input#<%= StateSelectedHiddenField.ClientID %>").val(ui.item.state);

                    $("#CityEntrySelectedCity").text(ui.item.city + ", " + ui.item.state + " (" + ui.item.zipcode + ")");

                    return false;
                }
            });
        });


        // this example shows the use of onShow and onHide callbacks. Make
        // sure to read the documentation for futher instructions, and
        // an explanation of the "hash" argument.

        $().ready(function () {
            
            $(".loading").dialog({
                
                autoOpen: false,
                resizable: false,
                modal: true,
                dialogClass: "noclose"
            });

        });

        var firstTime = true;

        $(function () {
            $('#tabsDistributor').tabs({
                load: function (event, ui) {
                    $('a', ui.panel).click(function () {
                        $(ui.panel).load(this.href);

                        return false;
                    });
                }


            });
            //InitializedDockMenu();
            $('#tabs').tabs({

                select: function (event, ui) {
                    $("input#<%= CitySelectedHiddenField.ClientID %>").val('');
                    $("input#<%= StateSelectedHiddenField.ClientID %>").val('');

                    UpdateSearchCriteria();

                    //                    
                    //                   

                    if (ui.index == 0) {
                        $('#<%= txtCityEntry.ClientID %>').val('');
                    } else if (ui.index == 1) {
                        $('#<%= txtZip.ClientID %>').val('');

                        $.getJSON("ProductSearch.ashx", { action: 'GetList' },
                        function (returnList) {
                            if (firstTime) {

                                $('#<%= DockMenuHiddenField.ClientID %>').html('dock');

                                InitializedDockMenu();

                                //eval('InitializedDockMenu_< %=MainCategoryDockMenu1.DockMenuId %>();');

                                firstTime = false;
                            }
                        });
                    }
                }

            });

        });

    function LocateDistributor() {

        

        var city = $("input#<%= CitySelectedHiddenField.ClientID %>").val();
        var zip = $("input#<%= ZipSelectedHiddenField.ClientID %>").val();
        var state = $("input#<%= StateSelectedHiddenField.ClientID %>").val();
        var productCategory = $(".ProductCategorySelectedHiddenField").text();
        var productCategoryId = $(".ProductCategoryIdSelectedHiddenField").text();

        var hfID = $.urlParam("hfID");
        var slID = $.urlParam("slID");

        while (city.indexOf(' ') > -1) {
            city = city.replace(' ', '-');
        }

        if (city.length > 0 && state.length > 0 && productCategoryId.length > 0) {

            $('.loading').dialog("open");

            //$('#DistributorTabContent').html("<div id='DistributorLoadingSpinner' class='DistributorLoadingSpinner'><div class='spinner-loading-image'><img src='/images/spinner.gif' /></div><div class='spinner-loading-text'>Loading...</div></div>");

            $("#loader1").removeClass('HiddenField');
            $("#loader2").removeClass('HiddenField');

            $("#loader1").addClass('HiddenField');
            $("#loader2").addClass('HiddenField');

            $("#tabsDistributor").fadeIn('slow');

            var urlToLoad = "LocateHandler.ashx?city=" + city + "&state=" + state + "&pcID=" + productCategoryId;

            if (zip != '') {
                urlToLoad += "&zip=" + zip;
            }

            if (hfID != 0) {
                urlToLoad += "&hfID=" + hfID;
            }

            if (slID != 0) {
                urlToLoad += "&slID=" + slID;
            }



            //$("#DistributorTabContent").load(urlToLoad);

            $.getJSON(urlToLoad + "&action=LocateResults", {},
                    function (distributorOutputArray) {

                        //drShortestDistance.ShippingAddress + ", " + drShortestDistance.CityName + ", " + abbreviation + " " + drShortestDistance.fk_ZipID;
                        //string addressDisplay = drShortestDistance.DistName + "<BR>" + drShortestDistance.ShippingAddress + "<BR>" + drShortestDistance.CityName + ", " + abbreviation + " " + drShortestDistance.fk_ZipID;

                        var tabContentHtml = "";
                        var addressGoogle = "";
                        var addressDisplay = "";

                        var addressGoogle2 = "";
                        var addressDisplay2 = "";

                        $.each(distributorOutputArray, function (i, distributorOutput) {
                            if (i == 0) {
                                addressGoogle = distributorOutput.ShippingAddress + ", " + distributorOutput.City + ", " + distributorOutput.StateAbbreviation + " " + distributorOutput.ZipCode;
                                addressDisplay = distributorOutput.DistributorName + "<BR>" + distributorOutput.ShippingAddress + "<BR>" + distributorOutput.City + ", " + distributorOutput.StateAbbreviation + " " + distributorOutput.ZipCode;

                                $('#<%= RedirectURLHiddenField.ClientID %>').val(distributorOutput.SalesLeadFormUrl);
                            }

                            if (i == 1) {
                                addressGoogle2 = distributorOutput.ShippingAddress + ", " + distributorOutput.City + ", " + distributorOutput.StateAbbreviation + " " + distributorOutput.ZipCode;
                                addressDisplay2 = distributorOutput.DistributorName + "<BR>" + distributorOutput.ShippingAddress + "<BR>" + distributorOutput.City + ", " + distributorOutput.StateAbbreviation + " " + distributorOutput.ZipCode;
                            }


                            if (i == 1) {
                                tabContentHtml += "<div id='search-manufacturer-rep-wrapper'>"

                                tabContentHtml += "<div><br /><br /><hr width='75%' /><br /><h2>Manufacturer Representative</h2><br /></div>";
                            }
                            if (i == 0) {
                                tabContentHtml += "<div id='search-distributor-info-wrapper'><a name='SearchDistributorInfoSection'></a>"

                                tabContentHtml += "<div id='map-wrapper'>";
                                tabContentHtml += "<div id='map_canvas' style='width: 300px; height: 300px;' />";
                                tabContentHtml += "<div><span id='DistanceLabel'>DISTANCE: </span><span>" + distributorOutput.DistanceToDistributor + " miles</span></div></div>";
                            }
                            tabContentHtml += "<div id='distributor-wrapper'><b>" + distributorOutput.DistributorName + "</b>";
                            tabContentHtml += "<div>" + distributorOutput.ShippingAddress + "</div>";
                            tabContentHtml += "<div>" + distributorOutput.City + ", " + distributorOutput.StateAbbreviation + " " + distributorOutput.ZipCode + "</div>";
                            tabContentHtml += "<div>" + distributorOutput.Phone + "</div>";
                            tabContentHtml += "<BR>";
                            tabContentHtml += "<div><b>Distributor of BOMAG Americas Product(s):</b></div>";
                            tabContentHtml += "<div>" + distributorOutput.Categories + "</div>";

                            if (i == 0) {
                                tabContentHtml += '<div id="MoreInformationButtonSection"><div class="graybutton"><input id="btnSalesLeadFormRedirect" class="button blue" type="button" value="More Information" onclick="javascript:Redirect();" /></div></div>';
                            }

                            tabContentHtml += "</div>";
                            tabContentHtml += "</div>";


                        });


                        $('#DistributorTabContent').html(tabContentHtml);

                        $('.loading').dialog("close");
                        location.href = "#SearchDistributorInfoSection";
                        ShowMap(addressGoogle, addressDisplay);

                        $("#loader1").addClass('HiddenField');
                        $("#loader2").addClass('HiddenField');

                    });





                } else {
                    alert("You must select a City and Product to continue");
                }
            }

            function Redirect() {
                location.href = $('#<%= RedirectURLHiddenField.ClientID %>').val();
            }

            $(document).ready(function () {

                $("#loader1").addClass('HiddenField');
                $("#loader2").addClass('HiddenField');

                $("#tabsDistributor").hide();

                $.getJSON("ProductSearch.ashx", { action: 'GetList' }, function (returnList) {

                });

            });

    </script>
    <asp:Label ID="DockMenuHiddenField" CssClass="HiddenField" runat="server" />
    <asp:HiddenField ID="ZipSelectedHiddenField" runat="server" />
    <asp:HiddenField ID="RedirectURLHiddenField" runat="server" />
    <asp:HiddenField ID="CitySelectedHiddenField" runat="server" />
    <asp:HiddenField ID="StateSelectedHiddenField" runat="server" />
    <asp:HiddenField ID="HeaderFooterIdHiddenField" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:Label ID="ProductCategorySelectedHiddenField" CssClass="ProductCategorySelectedHiddenField HiddenField"
        runat="server" />
    <asp:Label ID="ProductCategoryIdSelectedHiddenField" CssClass="ProductCategoryIdSelectedHiddenField HiddenField"
        runat="server" />
    <div id="ContentWrapper">
        <header id="LocateHeader">Welcome to findbomag.com.  This site will allow you to locate the BOMAG Dealership closest to your location.  Start below by choosing to search by zipcode or city & state.</header>
        <div id="tabs">
            <ul>
                <li class="tab-option" id="ZipTab"><a href="#ZipCodeTab"><span>Zip Code</span></a></li>
                <li class="tab-option" id="CityTab"><a href="#CityStateTab"><span>City & State</span></a> </li>
            </ul>
            <div class="tab-content" id="ZipCodeTab">
                <div class="TabContentWrapper">
                    <div class="Step1EnterLocationDiv StepWrapper">
                        <div class="StepNumber Step1">
                        </div>
                        <div class="StepContent">
                            <div class="heading-wrapper">
                                <div class="heading">
                                    <div class="headingText">
                                        Enter Zip Code
                                    </div>
                                </div>
                                <div class="headingDescription">
                                    Type your Zip Code and select your city
                                </div>
                            </div>
                            <div>
                                <asp:TextBox ID="txtZip" runat="server"></asp:TextBox>
                            </div>
                            <div id="ZipSelectedCity">
                            </div>
                        </div>
                    </div>
                    <div class="Step2SelectProductDiv StepWrapper">
                        <div class="StepNumber Step2">
                        </div>
                        <div class="StepContent">
                            <div class="heading-wrapper">
                                <div class="heading">
                                    <div class="headingText">
                                        Select Product
                                    </div>
                                </div>
                                <div class="headingDescription">
                                    Select the BOMAG Product line you are trying to locate.  For a list click here
                                </div>
                            </div>

                            <div class="main-category-control-wrapper">
                                <DL:MainCategoryUserControl ID="MainCategoryUserControl" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="Step3LocateDiv StepWrapper">
                        <div class="LocateButtonWrapper InlineDiv">
                            <span class="StepNumber Step3"></span>
                            <div class="StepContent">
                                <div class="heading-wrapper">
                                    <div class="heading">
                                        <div class="headingText">
                                            Locate
                                        </div>
                                    </div>
                                    <div class="headingDescription">
                                        Click the button below to Locate your BOMAG Distributor
                                    </div>
                                </div>
                                <div>
                                    <input class="button blue" type="button" value="Locate" onclick="javascript: LocateDistributor()" />
                                </div>
                            </div>
                        </div>
                        <span id="SearchCriteria" class="SearchCriteria"></span>

                    </div>
                    <div>
                        <asp:Label ID="Label1" runat="server" BackColor="#FFFFC0" BorderColor="White" ForeColor="Black"
                            Visible="False" Width="276px"></asp:Label>
                    </div>
                    <div>&nbsp;</div>
                </div>
            </div>
            <div class="tab-content" id="CityStateTab">
                <div class="TabContentWrapper">
                    <div class="Step1EnterLocationDiv StepWrapper">
                        <div class="StepNumber Step1">
                        </div>
                        <div class="StepContent">
                            <div class="heading-wrapper">
                                <div class="heading">
                                    <div class="headingText">
                                        Select City & State
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cboState" runat="server" AutoPostBack="False">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div>
                                <asp:TextBox ID="txtCityEntry" runat="server"></asp:TextBox>
                            </div>
                            <div id="CityEntrySelectedCity">
                            </div>
                        </div>
                    </div>
                    <div class="Step2SelectProductDiv StepWrapper">
                        <div class="StepNumber Step2">
                        </div>
                        <div class="StepContent">
                            <div class="heading-wrapper">
                                <div class="heading">
                                    <div class="headingText">
                                        Select Product
                                    </div>

                                </div>
                                <div class="headingDescription">
                                    Select the BOMAG Product line you are trying to locate.  For a list click here
                                </div>
                            </div>
                            <div class="main-category-control-wrapper">
                                <DL:MainCategoryUserControl ID="MainCategoryUserControl1" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="Step3LocateDiv StepWrapper">
                        <div class="LocateButtonWrapper InlineDiv">
                            <span class="StepNumber Step3"></span>
                            <div class="StepContent">
                                <div class="heading-wrapper">
                                    <div class="heading">
                                        <div class="headingText">
                                            Locate
                                        </div>
                                    </div>
                                    <div class="headingDescription">
                                        Click the button below to Locate your BOMAG Distributor
                                    </div>
                                </div>
                                <div>
                                    <input class="button blue" type="button" value="Locate" onclick="javascript: LocateDistributor()" />
                                </div>
                            </div>
                        </div>
                        <span id="SearchCriteria2" class="SearchCriteria"></span>
                        <div id="loader2" class="loading">
                            <div class="spinner-loading-image">
                                <img src="/images/spinner.gif" />
                            </div>
                            <div class="spinner-loading-text">
                                Loading...
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="Label2" runat="server" BackColor="#FFFFC0" BorderColor="White" ForeColor="Black"
                            Visible="False" Width="276px"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="Separator" class="SeparatorBar"></div>
        <!--<div class="ui-widget" style="margin-top: 2em; font-family: Arial">
        Result:
        <div id="log" style="height: 200px; width: 300px; overflow: auto;" class="ui-widget-content">
        </div>
    </div>
    -->
        <div id="tabsDistributor">
            <ul>
                <li class="tab-option" id="DistributorHeaderTab"><a href="#DistributorTab"><span>YOUR DISTRIBUTOR</span></a></li>
            </ul>
            <div class="tab-content" id="DistributorTab">
                <div id="DistributorTabContent">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
        <div style="display: none;">
            <div id="ResultTable" runat="server">
                <table width="400px">
                    <tr id="SalesLeadFormRow">
                        <td>
                            <asp:Literal ID="litResults" runat="server"></asp:Literal><br />
                            <div class="graybutton">
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                &nbsp;<br />
                <asp:Literal ID="litNewSearch" runat="server"></asp:Literal>
            </div>
        </div>
        <table width="400px" style="display: none;">
            <tr id="ZipRow" runat="server">
                <td valign="top">
                    <asp:Label ID="lblZip" runat="server" Text="Zip Code:"></asp:Label>
                </td>
                <td valign="top" style="width: 279px"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblZipValid" runat="server" BackColor="#FFFFC0" BorderColor="White"
                        ForeColor="Black" Visible="False" Width="276px"></asp:Label>
                </td>
            </tr>
            <tr id="OrRow" runat="server">
                <td colspan="2" align="center">OR<br />
                </td>
            </tr>
            <tr id="CityRowEntry" runat="server">
                <td valign="top">
                    <asp:Label ID="lblCityEntry" runat="server" Text="City:"></asp:Label>
                </td>
                <td valign="top" style="width: 279px"></td>
            </tr>
            <tr id="StateRow" runat="server">
                <td valign="top">
                    <asp:Label ID="lblStateEntry" runat="server" Text="State:"></asp:Label>
                </td>
                <td valign="top" style="width: 279px"></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblCityStateValid" runat="server" BackColor="#FFFFC0" BorderColor="White"
                        ForeColor="Black" Visible="False" Width="276px"></asp:Label>
                </td>
            </tr>
            <tr id="CategoryRow" runat="server">
                <td valign="top">
                    <asp:Label ID="lblProductLine" runat="server" Text="Product Line:"></asp:Label>
                </td>
                <td style="width: 279px">
                    <asp:DropDownList ID="cboProductLine" runat="server" Width="276px" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <div align="right">
                        <asp:Button ID="btnSubmitZip" runat="server" Text="Next" Width="63px" OnClick="btnSubmitZip_Click"
                            Height="24px" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>
            <tr id="CityRow" runat="server">
                <td valign="top" style="width: 74px">
                    <asp:Label ID="lblCity" runat="server" Text="City Closest To You:" Width="138px"></asp:Label>
                </td>
                <td style="width: 279px">
                    <asp:DropDownList ID="cboCity" runat="server" Width="276px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="LocateButtonRow" runat="server">
                <td colspan="2" style="height: 14px">
                    <div align="right">
                        <asp:Button ID="btnLocate" CssClass="button blue" runat="server" Text="Locate" Height="24px" Width="63px"
                            OnClick="btnLocate_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$
    ConnectionStrings:Dev %>"
            SelectCommand="SELECT [CategoryName], [CategoryID] FROM
    [Category]"></asp:SqlDataSource>
        <asp:Label ID="lblSelectedCity" runat="server" Width="138px"></asp:Label>
        <div style="position: absolute; margin: -100px 0 0 100px;">
        </div>
        <div id="loader1" class="loading">
            <div class="loading-inner">
                <div class="spinner-loading-image">
                    <img src="/images/spinner.gif" />
                </div>
                <div class="spinner-loading-text">
                    Locating Distributor...
                </div>
            </div>
        </div>
</asp:Content>
