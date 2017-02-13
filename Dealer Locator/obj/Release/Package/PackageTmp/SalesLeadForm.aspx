<%@ Page Language="C#" MasterPageFile="~/DealerLocator.Master" AutoEventWireup="true"
    CodeBehind="SalesLeadForm.aspx.cs" Inherits="Dealer_Locator.SalesLeadForm" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="usercontrols/MainCategoryCarouselUserControl.ascx" TagName="MainCategoryCarouselUserControl"
    TagPrefix="DL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" src="./js/jquery.infieldlabel.js"></script>

    <script type="text/javascript" src="./js/jquery.carouFredSel-6.2.1.js"></script>
    <script type="text/javascript" src="./js/jquery.maskedinput.js"></script>

    <script type="text/javascript" src="./js/SalesLeadForm.js"></script>
    <link type="text/css" href="./css/salesleadform.css" media="screen" rel="stylesheet" />
    <link type="text/css" href="./css/navDock.css" media="screen" rel="stylesheet" />
    <link type="text/css" href="./css/jquery.qtip.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function pageLoad() {

            var kioskCountry = $("#<%= lblKioskCountry.ClientID %>");
            if ($(kioskCountry).length > 0 && $(kioskCountry).html().length > 0) {
                if ($(kioskCountry).html().toUpperCase().indexOf("CANADA") > -1) {
                    $(kioskCountry).val("Canada");
                } else {
                    $(kioskCountry).val("Other");
                }

                $(kioskCountry).change();
            }
        }

        function callServer() {

        }

        function DisplayModels() {

        }

    </script>
    <script language="javascript" type="text/javascript">


        $(document).ready(function () {

            $("#<%= txtPhone.ClientID %>").mask("(999) 999-9999");
            $("#<%= txtFax.ClientID %>").mask("(999) 999-9999");

            $("label").inFieldLabels();

            var firstTime = true;
            var currentSelectedTab = 0;



            // initialize display
            $('.DefaultCountry').show();
            $('.OtherCountry').hide();

            $('#<%= cboCountryRegion.ClientID %>').change(function () {
                var selectedValue = $('#<%= cboCountryRegion.ClientID %> option:selected').text();

                $("#OtherCountryNameTextboxArea").hide();

                if (selectedValue != 'United States') {
                    $('.OtherCountry').show();
                    $('.DefaultCountry').hide();

                    if (selectedValue == "Other") {
                        $("#OtherCountryNameTextboxArea").show();
                    } else {
                        $("#OtherCountryNameTextboxArea").hide();
                    }

                } else {
                    $('.DefaultCountry').show();
                    $('.OtherCountry').hide();
                }

            });


        });

    </script>
    <script type="text/javascript">
        // Model code



    </script>

    <script type="text/javascript">

        $(document).ready(function () {


        });

    </script>
    <script language="javascript" type="text/javascript">
        function SelectProductCategory(productCategory, productCategoryId, largeImageUrl) {

            $(".ProductCategorySelectedHiddenField").text(productCategory);
            $(".ProductCategoryIdSelectedHiddenField").text(productCategoryId);
            $(".ProductCategoryImageURLSelectedHiddenField").text(largeImageUrl);
            UpdateSearchCriteria();
        }

       

        // Main Cateogy
        function UpdateSearchCriteria() {



            var mainCategoryName = $(".ProductCategorySelectedHiddenField").text();
            var mainCatImageURL = $(".ProductCategoryImageURLSelectedHiddenField").text();

            $.getJSON("ProductSearch.ashx", { category: mainCategoryName, action: 'list' },
            function (response) {

                $('#ProductsWrapper').html('');
                $('#ProductsWrapper').append('<ul></ul');

                $('#AccordionProductsWrapper').html('');
                var accordion = $('#AccordionProductsWrapper');
                
                var tabs = $('#ProductsWrapper');
                //$('#ProductsWrapper ul').append(tabs);

                tabs.tabs();

                var scriptToRun = '';

                $.each(response, function (i, item) {

                    $.each(item, function (i, category) {

                        if (category.name.length == 0) {
                            category.name = mainCategoryName;
                        }

                        var modelData = '';
                        var selectAll = '';
                        var selectNone = '';

                        $.each(category.products, function (k, model) {

                            var modelID = "product_" + category.subCatId + "_" + model.modelID;
                            modelData += "<span class='ProductCheckbox'><INPUT class='ProductCheckboxItem' TYPE=CHECKBOX data-modelID='" + model.modelID + "' id='" + modelID + "'  ";

                            if (model.selected == true) {
                                modelData += " CHECKED";

                            }


                            modelData += " >" + model.name + "</INPUT></span>";

                            selectAll += "console.log($('#" + modelID + "').is(':checked')); if ($('#" + modelID + "').is(':checked') == false) {$('#" + modelID + "').attr('checked',true); ProductStatusToggled('" + model.modelID + "', true)};";
                            selectNone += "$('#" + modelID + "').attr('checked',false);ProductStatusToggled('" + model.modelID + "', false);";

                        });

                        modelData += '<div class="SelectCheckboxButtons"><span><button type="button" onclick="' + selectAll + '">Select All</button></span>';
                        modelData += '<span><button type="button" onclick="' + selectNone + '">None</button></span></div>';

                        var catID = category.name + category.subCatId;

                        while (catID.indexOf(' ') > -1) {
                            catID = catID.replace(' ', '')
                        }

                        if ($(tabs).is(':visible') == false) {
                            $(tabs).show();
                        }

                        catID = catID.replace('/', '_');
                        var tab_content = '<div class="tab-selected-category-wrapper"><span class="tab-selected-category-data"><div><img src="' + mainCatImageURL + '" /></div><div><h4>' + mainCategoryName + '</h4></div></span><span class="tab-model-data"><BR>' + modelData + '</span></div>';
                        var data = $('<div style="border: 1px solid #aaaaaa; background-color: #FFFFED; border-bottom: 0px; height: 100%;" id="' + catID + '"></div>').append(tab_content);

                        $(tabs).find('ul').append("<li><a href='#" + catID + "'><div><h4>" + category.name + "</h4></div></a></li>");
                        $(tabs).append(data);

                        $(accordion).append("<div><h3>" + category.name + "</h3><div>" + tab_content + "</div></div>");

                    });
                });


                //$("#AccordionProductsWrapper").accordion()

                if ($(document).width() < 768) {
                    $("#ProductsWrapper").hide();
                    $("#AccordionProductsWrapper").accordion("refresh");
                    $("#AccordionProductsWrapper > .ui-accordion-content").css('height', 'auto');
                }
                else {
                    $("#AccordionProductsWrapper").hide();
                    $(tabs).tabs("refresh");
                    tabs.tabs("option", "active", 0);
                }

            });

        }

        $(document).ready(function () {
            $("#AccordionProductsWrapper").accordion({
                header: "> div > h3",
                collapsible: true,
                active: false,
                autoHeight: true,
                autoActivate: true
            });
        });

        $(document).on('click', '.ProductCheckbox input', function () {

            var modelID = $(this).attr("data-modelID");
            var checked = this.checked;

            ProductStatusToggled(modelID, checked);


        });

        function ProductStatusToggled(theCheckBoxName, checked) {

            console.log(theCheckBoxName + "    " + checked);

            $.getJSON("ProductSearch.ashx", { selected: checked, action: 'toggle', productId: theCheckBoxName },
            function (returnProduct) {

                if (checked) {
                    console.log(checked);
                    AddSelectedModel(returnProduct, theCheckBoxName);
                    $('#ProductSelectionTabLink').qtip('hide');
                } else {
                    console.log('fadeout');
                    FadeOutSelected("selected_", theCheckBoxName);
                }
            });

        }

        function SearchResultsProductStatusToggled(modelId, isCurrentlySelected) {
            var checked = true;

            // Invert the selection to toggle the state of the item
            if (isCurrentlySelected) {
                checked = false;
            }


            $('#' + modelId).attr('checked', checked);

            ProductStatusToggled(modelId, checked);
        }

        function RemoveAndFadeOutSelected(selectedPrefix, selectedName) {

            $.getJSON("ProductSearch.ashx", { selected: false, action: 'toggle', productId: selectedName },
            function (returnProduct) {


                FadeOutSelected(selectedPrefix, selectedName);

            });
        }

        function FadeOutSelected(selectedPrefix, selectedName) {

            $('#' + selectedPrefix + selectedName).fadeOut(1000, function () {

                $('#' + selectedPrefix + selectedName).remove();
            });

            $("input[name='" + selectedName + "']").attr('checked', false);
        }

        function AddSelectedModel(returnProduct, theCheckBoxName) {
            var selectedItemDiv = '<span class="ProductSelectedSpan" id="selected_' + theCheckBoxName + '"><span>' + returnProduct.name + '</span><span><a href="javascript:RemoveAndFadeOutSelected(\'selected_\',\'' + theCheckBoxName + '\');"><img src="images/dockmenu/close.png" /></a></span></span>';
            $('#SelectedProductsList').append(selectedItemDiv);
        }


        function DisplayModels() {
            $.getJSON("ProductSearch.ashx", { action: 'GetList' },
                        function (returnList) {

                            if (returnList != null) {

                                $.each(returnList, function (i, product) {
                                    if (product.selected == true) {
                                        AddSelectedModel(product, product.modelID);
                                    }
                                    //productList += "<li>-" + product.name + "</li>";

                                });



                            }



                        });
        }

        // ################################
        // # Search Models Area
        // ################################
        var autoCompleteItems;
        var totalItems = 0;
        var startingItem = 0;
        var itemSetSize = 6;
        var currentItemIndex = 0;

        $(function () {


            $("#ctl00_ContentPlaceHolder1_ProductSearchTextBox").autocomplete({
                //  source: "CityStateSearch.ashx",

                source: function (request, response) {
                    totalItems = 0;
                    autoCompleteItems = null;
                    startingItem = 0;

                    $.getJSON("ProductSearch.ashx", { action: 'search', q: request.term }, function (data) {


                        //ChangeSet
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
                minLength: 3,

                select: function (event, ui) {
                    //log(ui.item ? ("Selected: " + ui.item.zipcode + " - " + ui.item.city + " - " + ui.item.state) : "Nothing selected, input was " + this.value); // for debugging

                    //                    this.value = ui.item.ModelID;
                    SearchResultsProductStatusToggled(ui.item.ModelID, ui.item.Selected);

                    return false;
                }


            });

            $.ui.autocomplete.prototype._renderItem = function (ul, item) {
                var oldFn = $.ui.autocomplete.prototype._renderItem;

                var hiddenClass = '';
                if (currentItemIndex >= (startingItem + itemSetSize)) {
                    hiddenClass = 'HiddenField';
                }

                currentItemIndex += 1;

                var rowClass = 'EvenRowSearchResults';
                if ((currentItemIndex % 2) == 1) {
                    rowClass = 'OddRowSearchResults';
                }


                return $("<li class='" + hiddenClass + " " + rowClass + "'></li>")
    				.data("item.autocomplete", item)
    				.append("<a>" + item.label + "</a>")
    				.appendTo(ul);




            };



            function BuildDisplayValue(val) {

                var displayValue = val.Model + " (" + val.MainCategory;

                if (val.SubCategory.length > 0) {
                    displayValue += ", " + val.SubCategory;
                }

                displayValue += ")";

                if (displayValue.toUpperCase().indexOf(val.MatchingText.toUpperCase()) > -1) {
                    var matchStart = displayValue.toUpperCase().indexOf(val.MatchingText.toUpperCase());
                    var matchLength = val.MatchingText.length;

                    var tempDisplayValue = displayValue.substr(0, matchStart);
                    tempDisplayValue += "<b>" + displayValue.substr(matchStart, matchLength) + "</b>";
                    tempDisplayValue += displayValue.substr(matchStart + matchLength, displayValue.length - matchLength - matchStart);

                    displayValue = tempDisplayValue;
                }

                displayValue = displayValue.replace(val.MatchingText, "<b>" + val.MatchingText + "</b>");

                return displayValue;
            }


        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#CustomerInformationTab').show('slide', { direction: 'left' });

        });
        function ShowStep(stepID) {
            $('.form-section-wrapper').hide()
            $('#' + stepID).show('slide', {
                direction: 'left', complete: function () {
                    if (stepID == 'ProductSelectionTab')
                        window.carousel.reloadSlider();
                }
            });
        }


        function CheckStep1() {

            var isValid = true;
            var required = '';
            $(".RequiredField").each(function (i, val) {
                // check values here
                required += $(this).val();

                if ($(this).val().length == 0) {
                    isValid = false;
                    ShowToolTip(this.id);
                }

            });

            var selectedValue = $('#<%= cboCountryRegion.ClientID %> option:selected').text();

            if (selectedValue != 'United States') {
                // Loop through 'other country' required fields

                $(".RequiredFieldOtherCountry").each(function (i, val) {
                    // check values here
                    required += $(this).val();

                    if ($(this).val().length == 0) {

                        if (selectedValue == "Other") {
                            isValid = false;
                            ShowToolTip(this.id);
                        } else {
                            if (this.id.indexOf("OtherCountryNameTextboxArea") > -1) {
                                isValid = false;
                                ShowToolTip(this.id);
                            }
                        }
                    }

                });
            } else {
                // Loop through 'default country' required fields

                $(".RequiredFieldDefaultCountry").each(function (i, val) {
                    // check values here
                    required += $(this).val();

                    if ($(this).val().length == 0) {
                        isValid = false;
                        ShowToolTip(this.id);
                    }

                });
            }



            //                alert(isValid + " - " + required);

            return isValid;
        }

        function CheckStep2() {
            var isValid = true;

            var selectedOptionVal = $("#<%= PlanToBuy.ClientID %> option:selected").text();

            if (selectedOptionVal.toUpperCase() == "DO NOT PLAN TO BUY") {
                isValid = true;
            } else {
                if ($('.ProductSelectedSpan').length == 0) {
                    isValid = false;

                    ShowToolTip('ProductSelectionTabLink', 'Oops, don\'t forget to add at least one model before continuing');
                }
            }


            return isValid;
        }

        function ValidateActiveStep() {
            var isValid = true;
            if ($('#CustomerInformationTab').is(':visible')) {
                isValid = CheckStep1();

                $('#CustomerInformationTabBreadCrumb').removeClass('step-invalid');
                $('#CustomerInformationTabBreadCrumb').removeClass('step-valid');

                if (isValid)
                    $('#CustomerInformationTabBreadCrumb').addClass('step-valid');
                else
                    $('#CustomerInformationTabBreadCrumb').addClass('step-valid');

            }
            else if ($('#ProductSelectionTab').is(':visible')) {
                isValid = CheckStep2();

                $('#ProductSelectionTabBreadCrumb').removeClass('step-invalid');
                $('#ProductSelectionTabBreadCrumb').removeClass('step-valid');

                if (isValid)
                    $('#ProductSelectionTabBreadCrumb').addClass('step-valid');
                else
                    $('#ProductSelectionTabBreadCrumb').addClass('step-invalid');

            }
            else if ($('#SendRequestTabBreadCrumb').is(':visible')) {

                if (isValid)
                    $('#SendRequestTabBreadCrumb').addClass('step-valid');
                else
                    $('#SendRequestTabBreadCrumb').addClass('step-invalid');
            }

            if (isValid) {
                $('#CustomerInformationTabBreadCrumb').removeClass('step-active');
                $('#ProductSelectionTabBreadCrumb').removeClass('step-active')
                $('#SendRequestTabBreadCrumb').removeClass('step-active')
            }

            return isValid;
        }


        function ShowStep1() {
            if (ValidateActiveStep()) {
                ShowStep('CustomerInformationTab');
                $('#CustomerInformationTabBreadCrumb').addClass('step-active');
                $('#CustomerInformationTabBreadCrumb').removeClass('step-invalid');
            }
        }

        function ShowStep2() {

            if (ValidateActiveStep()) {
                ShowStep('ProductSelectionTab');
                $('#ProductSelectionTabBreadCrumb').addClass('step-active');
                $('#ProductSelectionTabBreadCrumb').removeClass('step-invalid');
                
            }

        }

        function ShowStep3() {

            if (ValidateActiveStep()) {
                ShowStep('SendRequestTab');
                $('#SendRequestTabBreadCrumb').addClass('step-active');
                $('#SendRequestTabBreadCrumb').removeClass('step-invalid');

                ShowSummary();
            }
        }

        function ShowSummary() {
            $.getJSON("ProductSearch.ashx", { action: 'GetList' },
                    function (returnList) {
                        var prodSumDivContent = "<div><div class='SummaryHeader'>PRODUCT(S) - <a href='javascript:ShowStep2();'>Edit</a></div>";

                        if (returnList != null) {
                            var productList = "<ul>";
                            $.each(returnList, function (i, product) {

                                if (product.selected == true) {
                                    productList += "<li>-" + product.name + "</li>";
                                }
                            });

                            productList += "</ul>";


                            var deliveryMethod = '';

                            if ($('#<%= PhysicalDelivery.ClientID %>').is(':checked')) {
                                deliveryMethod = "Physical";
                            }
                            else if ($('#<%= EmailDelivery.ClientID %>').is(':checked')) {
                                deliveryMethod = "Email";
                            }


                            prodSumDivContent += "<div class='SummaryContent'>" + productList + "</div><div id='DeliveryMethodLabel'>Delivery Method: <span id='DeliveryMethod'>" + deliveryMethod + "</span></div></div>";
                        }

                        $('#ProductSummaryDiv').html(prodSumDivContent);

                    });



            var custSumDivContent = '';

            var firstName = $('#<%= txtFirstName.ClientID %>').val();
            var lastName = $('#<%= txtLastName.ClientID %>').val();
            var fullName = firstName + ' ' + lastName;

            var countryRegion = $('#<%= cboCountryRegion.ClientID %>').val();
            var city, state, zip, countryName;

            var streetAddress1 = $('#<%= txtAddress1.ClientID %>').val();
            var cityStateZipCountry = '';

            if (countryRegion != 'United States') {

                city = $('#<%= txtOtherCity.ClientID %>').val();
                state = $('#<%= txtOtherStateProvince.ClientID %>').val();
                zip = $('#<%= txtOtherPostalCode.ClientID %>').val();
                countryName = $('#<%= txtOtherCountry.ClientID %>').val();

                cityStateZipCountry = city + ', ' + state + ' ' + zip + '</div><div>' + countryName;

            } else {
                try {
                    city = $('select[name="cboCityName"]').val();

                    if (city != undefined) {

                        state = $('#<%= txtState.ClientID %>').val();
                        zip = $('#<%= txtZip2.ClientID %>').val();
                        countryName = $('#<%= cboCountryRegion.ClientID %>').val();

                        cityStateZipCountry = city + ', ' + state + ' ' + zip;
                    }


                } catch (e) {


                }
            }

            var phone = $('#<%= txtPhone.ClientID %>').val();

            custSumDivContent += "<div><div class='SummaryHeader'>CUSTOMER - <a href='javascript:ShowStep1();'>Edit</a></div>";
            custSumDivContent += "<div class='SummaryContent'><div>" + fullName + "</div>";
            custSumDivContent += "<div>" + streetAddress1 + "</div>";
            custSumDivContent += "<div>" + cityStateZipCountry + "</div>";
            custSumDivContent += "<div>" + phone + "</div></div></div>";



            $('#CustomerSummaryDiv').html(custSumDivContent);
        }

        function ValidateVerificationCode() {

            var isValid = true;
            var selectedOptionVal = $("#<%= PlanToBuy.ClientID %> option:selected").text();

            var isDoNotPlanToBuy = false;

            if (selectedOptionVal.toUpperCase() == "DO NOT PLAN TO BUY") {
                isDoNotPlanToBuy = true;

                var comments = $('#ctl00_ContentPlaceHolder1_txtComments').val();

                if (comments.length == 0) {
                    isValid = false;
                    ShowToolTip('ctl00_ContentPlaceHolder1_txtComments');
                }

            }

            if (isValid) {
                var verificationCode = $('#ctl00_ContentPlaceHolder1_CodeNumberTextBox').val();
                var kioskModeValue = $('#ctl00_ContentPlaceHolder1_KioskModeHiddenField').val();
                var result = false;


                $.getJSON("LeadSubmission.ashx", { code: verificationCode, kioskMode: kioskModeValue, action: 'Verification', isDoNotPlanToBuy: isDoNotPlanToBuy }, function (response) {

                    if (response.IsValid) {

                        if (response.HasModelsSelected == false) {

                            ActivateTab(1); // $('#SalesLeadFormTabs').tabs({ selected: 2 });

                            ShowToolTip('ProductSelectionTabLink', 'Oops, don\'t forget to add at least one model before continuing');
                        } else {
                            ShowSubmissionSpinner();
                            $('#ctl00_ContentPlaceHolder1_btnSalesLeadFormHidden').trigger("click");
                        }

                    } else {

                        var verificationCode = $('#ctl00_ContentPlaceHolder1_CodeNumberTextBox').val('');

                        $('#CaptchaImage').attr('src', 'JpegImage.aspx?' + (new Date().getTime()));

                        ActivateTab(2); // $('#SalesLeadFormTabs').tabs({ selected: 2 });

                        ShowToolTip('ctl00_ContentPlaceHolder1_CodeNumberTextBox', 'Mis-typed verification Code.  Please try again');

                        if (response.HasModelsSelected == false) {
                            ShowToolTip('ProductSelectionTabLink', 'Oops, don\'t forget to add at least one model before continuing');
                        }


                    }

                });

            }

            if (result == false) {
            }

            return result;
        }

    </script>
    
    <asp:Label ID="ProductCategorySelectedHiddenField" CssClass="ProductCategorySelectedHiddenField HiddenField"
        runat="server" />
    <asp:Label ID="ProductCategoryImageURLSelectedHiddenField" CssClass="ProductCategoryImageURLSelectedHiddenField HiddenField"
        runat="server" />
    <asp:Label ID="DockMenuHiddenField" CssClass="HiddenField" runat="server" />
    <asp:HiddenField ID="KioskModeHiddenField" runat="server" Value="false" />


    <div id="ContentWrapper">
        <div id="SalesLeadFormTabs" class="">
            <div id="BreadCrumbsWrapper">
                <span id="CustomerInformationTabBreadCrumb" class="step-active"><a href="javascript:ShowStep1()">CUSTOMER INFO</a></span>
                <span class="breadcrumb-separator">>></span>
                <span id="ProductSelectionTabBreadCrumb" class="step-invalid"><a href="javascript:ShowStep2()" id="ProductSelectionTabLink">PRODUCT SELECTION</a></span>
                <span class="breadcrumb-separator">>></span>
                <span id="SendRequestTabBreadCrumb" class="step-invalid"><a href="javascript:ShowStep3()">REVIEW</a></span>
                <div id="HeaderRequiredFieldTitle">
                    <span id="HeaderRequiredFieldAsterisk"><span class="RequiredFieldIndicator">*</span> Required Field</span><br />
                </div>
            </div>

            <div id="CustomerInformationTab" class="form-section-wrapper">
                <div id="KioskControls" runat="server">
                    <br />
                    <asp:Button ID="btnBackToKioskScreen" runat="server" Text="Back To Kiosk Screen"
                        OnClick="btnBackToKioskScreen_Click" />
                </div>

                <div id="grouping-left">
                    <div class="fieldset-wrapper">
                        <fieldset class="FieldSetArea">
                            <legend>NAME</legend>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="InputControl RequiredField" />
                                <label for="<%= txtFirstName.ClientID %>" class="FormFieldHalfWidthLabel">
                                    First<span class="RequiredFieldIndicator">*</span></label>
                            </div>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="InputControl RequiredField" />
                                <label for="<%=txtLastName.ClientID %>" class="FormFieldHalfWidthLabel">
                                    Last<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtCompany_Agency" runat="server" CssClass="InputControl RequiredField" />
                                <label for="<%=txtCompany_Agency.ClientID %>" class="FormFieldFullWidthLabel">
                                    Company<span class="RequiredFieldIndicator">*</span></label>
                            </div>

                            <div class="form-field-wrapper">
                                <asp:CheckBox ID="chkRental" runat="server" CssClass="InputControl" />
                                <span class="FormFieldHalfWidthLabel"><b>This is a rental company</b></span>
                            </div>
                        </fieldset>
                    </div>
                    <div class="fieldset-wrapper">
                        <fieldset>
                            <legend>ADDRESS</legend>

                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="InputControl RequiredField"></asp:TextBox>
                                <label for="<%=txtAddress1.ClientID %>" class="FormFieldFullWidthLabel">
                                    Street Address<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>

                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="InputControl"></asp:TextBox>
                                <label for="<%=txtAddress2.ClientID %>" class="FormFieldFullWidthLabel">
                                    Address Line 2</label>
                            </div>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtCity" runat="server" CssClass="InputControl"></asp:TextBox>
                                <label for="<%=txtCity.ClientID %>" class="FormFieldFullWidthLabel">
                                    City<span class="RequiredFieldIndicator">*</span></label>
                            </div>
                            <div class="form-field-wrapper OtherCountry">
                                <asp:TextBox ID="txtOtherCity" runat="server" CssClass="InputControl RequiredFieldOtherCountry"></asp:TextBox><asp:Label
                                    ID="lblOtherCityCard" runat="server" BackColor="#7AE38B" Font-Bold="True" Visible="False"></asp:Label>
                                <label for="<%=txtOtherCity.ClientID %>" class="FormFieldFullWidthLabel">
                                    City<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper OtherCountry">
                                <asp:TextBox ID="txtOtherStateProvince" runat="server" CssClass="InputControl RequiredFieldOtherCountry"></asp:TextBox>
                                <label for="<%=txtOtherStateProvince.ClientID %>" class="FormFieldFullWidthLabel">
                                    State / Province / Region<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper OtherCountry">
                                <asp:TextBox ID="txtOtherPostalCode" runat="server" CssClass="InputControl RequiredFieldOtherCountry"></asp:TextBox>
                                <label for="<%=txtOtherPostalCode.ClientID %>" class="FormFieldFullWidthLabel">
                                    Postal Code<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div id="OtherCountryNameTextboxArea" class="form-field-wrapper OtherCountry">
                                <asp:TextBox ID="txtOtherCountry" runat="server" CssClass="InputControl RequiredFieldOtherCountry"></asp:TextBox>
                                <label for="<%=txtOtherCountry.ClientID %>" class="FormFieldFullWidthLabel">
                                    Country Name<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper FormFieldHalfWidthLabel DefaultCountry" id="lblCityCardWrapper" runat="server" visible="false">
                                <asp:Label ID="lblCityCard" runat="server" CssClass="InputControl" Visible="False"></asp:Label>
                                <div id="city" class="InputControl">
                                </div>
                                <label for="lblCityCard" class="FormFieldHalfWidthLabel">
                                    City<span class="RequiredFieldIndicator">*</span></label>
                            </div>
                            <div class="form-field-wrapper FormFieldHalfWidthLabel DefaultCountry">
                                <input type="text" id="txtState" runat="server" class="InputControl RequiredFieldDefaultCountry" />
                                <label for="<%=txtState.ClientID %>" class="FormFieldHalfWidthLabel">
                                    State<span class="RequiredFieldIndicator">*</span></label>
                            </div>
                            <div class="form-field-wrapper FormFieldHalfWidthLabel DefaultCountry">
                                <input type="text" id="txtZip2" onkeydown="return noNumbers(event)" onkeyup="javascript:callServer();"
                                    runat="server" maxlength="5" class="InputControl RequiredFieldDefaultCountry" />
                                <label for="<%=txtZip2.ClientID %>" class="FormFieldHalfWidthLabel">
                                    Postal / Zip Code<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper FormFieldHalfWidthLabel">
                                <asp:DropDownList ID="cboCountryRegion" runat="server" CssClass="InputControl">
                                </asp:DropDownList>
                                <asp:Label ID="lblKioskCountry" runat="server" BackColor="#7AE38B" Font-Bold="True"
                                    Visible="True"></asp:Label>
                                <%--<label for="cboCountryRegion" class="FormFieldHalfWidthLabel">
                                Country<span class="RequiredFieldIndicator">*</span>
                            </label>--%>
                            </div>
                        </fieldset>
                    </div>

                </div>
                <div id="grouping-right">
                    <div class="fieldset-wrapper">
                        <fieldset>
                            <legend>CONTACT</legend>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="InputControl RequiredField"></asp:TextBox>
                                <label for="<%=txtPhone.ClientID %>" class="FormFieldHalfWidthLabel">
                                    Phone<span class="RequiredFieldIndicator">*</span>
                                </label>
                            </div>
                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtFax" runat="server" CssClass="InputControl"></asp:TextBox>
                                <label for="<%=txtFax.ClientID %>" class="FormFieldHalfWidthLabel">
                                    Fax
                                </label>
                            </div>

                            <div class="form-field-wrapper">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="InputControl" />
                                <label for="<%=txtEmail.ClientID %>" class="FormFieldFullWidthLabel">
                                    Email
                                </label>
                            </div>
                        </fieldset>
                    </div>

                    <div class="fieldset-wrapper" id="PlanToBuyDivWrapper" style="position: relative;">
                        <fieldset>
                            <legend>ACTION</legend>
                            <div id="ReferredByWrapper">
                                <span id="ReferredByTitle">Referred By: </span>
                                <span id="ReferredByDropdown">
                                    <asp:Literal ID="litSalesLeadForm" runat="server" />
                                </span>
                            </div>
                            <div id="PlanToBuyWrapper">
                                <span>Plan to Buy<span class="RequiredFieldIndicator">*</span>: </span>
                                <span>
                                    <asp:DropDownList ID="PlanToBuy" runat="server">
                                        <asp:ListItem Text="Immediately" Value="Immediately" Selected="True" />
                                        <asp:ListItem Text="1-3 months" Value="1-3 months" />
                                        <asp:ListItem Text="4-6 months" Value="4-6 months" />
                                        <asp:ListItem Text="1 year" Value="1 year" />
                                        <asp:ListItem Text="Do not plan to buy" Value="Do not plan to buy" />
                                    </asp:DropDownList></span>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="next-button-wrapper">
                    <input class="button blue" type="button" value="NEXT" onclick="javascript: ShowStep2();" />
                </div>
            </div>
            <div id="ProductSelectionTab" class="form-section-wrapper">


                <div class="fieldset-wrapper ContentWrapper" id="ProductSearch">

                    <div class="form-field-wrapper">
                        <asp:TextBox ID="ProductSearchTextBox" runat="server" CssClass="ProductSearchTextBox InputControl" />
                        <label for="<%= ProductSearchTextBox.ClientID %>" class="FormFieldFullWidthLabel">Product Search</label>


                    </div>

                </div>
                <div id="DockWrapper" class="ContentWrapper">
                    <DL:MainCategoryCarouselUserControl ID="MainCategoryCarouselUserControl" runat="server" />
                </div>
                <div id="ProductsWrapper" class="ContentWrapper">
                    <ul>
                    </ul>
                </div>
                <div id="AccordionProductsWrapper" class="TabContentWrapper"></div>
                <div>
                    <div id="SelectedProductsWrapper">
                        <fieldset class="FieldSetArea" id="SelectedProductsFieldSet">
                            <legend>Your Selections</legend>
                            <div id="SelectedProductsList">
                            </div>
                        </fieldset>
                    </div>
                    <div id="DeliveryMethodWrapper">
                        <fieldset class="FieldSetArea" id="DeliveryMethodFieldSet">
                            <legend>Delivery Method</legend>
                            <div>
                                <span>
                                    <input type="checkbox" runat="server" id="PhysicalDelivery" checked /></span><span><img
                                        src="/images/dockmenu/Product.png" /></span><span>Physical Delivery</span>
                            </div>
                            <div>
                                <span>
                                    <input type="checkbox" runat="server" id="EmailDelivery" /></span><span><img src="/images/dockmenu/Mail.png" /></span><span>Email
                                        Delivery</span>
                            </div>
                        </fieldset>
                    </div>
                </div>

                <div class="next-button-wrapper">
                    <input class="button blue" type="button" value="NEXT" onclick="javascript: ShowStep3();" />
                </div>
            </div>
            <div id="SendRequestTab" class="form-section-wrapper">
                <div id="review-summary-wrapper">
                    <div class="fieldset-wrapper">
                        <fieldset>
                            <legend>Summary</legend><span><span id="CustomerSummaryDiv" class="FormFieldHalfWidthLabel">CUSTOMER </span><span id="ProductSummaryDiv" class="FormFieldHalfWidthLabel">PRODUCT(S)
                            </span></span>
                        </fieldset>
                    </div>
                    <div class="fieldset-wrapper">
                        <fieldset>
                            <legend>Comments</legend>
                            <textarea id="txtComments" class="txtComments" name="txtComments" rows="6" wrap="soft" runat="server"></textarea>

                        </fieldset>
                    </div>
                    <div class="fieldset-wrapper">
                        <fieldset>
                            <legend>Verification</legend>

                            <div>
                                <img src="JpegImage.aspx" alt="Verification Image" id="CaptchaImage" />
                            </div>
                            <div id="verification-code-wrapper">

                                <label for="<%= CodeNumberTextBox.ClientID  %>" class="FormFieldFullWidthLabel">
                                </label>
                                <asp:TextBox ID="CodeNumberTextBox" runat="server" CssClass="InputControl"></asp:TextBox>
                            </div>
                            <div id="verification-button-wrapper">
                                <asp:Button ID="Button3" runat="server" Text="Reset" CssClass="button blue" />
                                &nbsp;<asp:Button ID="btnSendSalesLeadForm" runat="server" OnClientClick="ValidateVerificationCode(); return false;"
                                    Text="Send Request" CssClass="button blue" /><asp:Button ID="btnSalesLeadFormHidden" runat="server" CssClass="HiddenField"
                                        OnClick="btnSendSalesLeadForm_Click" />
                            </div>
                        </fieldset>
                    </div>
                </div>
                <asp:Label ID="lblProductListEmpty" runat="server" BackColor="LightGray" Text="Please add at least one product before submitting this form"
                    Visible="False"></asp:Label>
                <asp:Literal ID="litError" runat="server"></asp:Literal>
                <div style="display: none;">
                    <input type="text" id="lblHiddenCity2" runat="server" value="rawr" />
                    &nbsp;
                </div>
                <div style="display: none;">
                    <asp:TextBox ID="txtItemToRemove" runat="server" Width="202px"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
