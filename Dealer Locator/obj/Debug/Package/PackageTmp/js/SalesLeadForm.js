function ProductStatusToggled(theCheckBoxName) {

    var checked = false;

    if ($("input[name='" + theCheckBoxName + "']").attr('checked') == true) {
        checked = true;
    }

    $.getJSON("ProductSearch.ashx", { selected: checked, action: 'toggle', productId: theCheckBoxName },
    function (returnProduct) {

        if (checked) {
            AddSelectedModel(returnProduct, theCheckBoxName);
            $('#ProductSelectionTabLink').qtip('hide');
        } else {
            FadeOutSelected("selected_", theCheckBoxName);
        }
    });

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

var autoCompleteItems;
var totalItems = 0;
var startingItem = 0;
var itemSetSize = 6;
var currentItemIndex = 0;


$(".ProductSearchTextBox").autocomplete({
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

    //                var displayValue = item.label;
    //                displayValue = displayValue.replace(item.MatchingText, "<b>" + item.MatchingText + "</b>");
    //                item.label = displayValue;


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


function noNumbers(e) {
    var keynum
    var keychar
    var numcheck

    if (window.event) // IE
    {
        keynum = e.keyCode
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which
    }
    keychar = String.fromCharCode(keynum)

    var returnVal = false;

    var ValidChars = "0123456789";

    // alert('\'' + keynum + '\'');

    if (ValidChars.indexOf(keychar) > -1) {
        returnVal = true;
    }

    if (keynum > 95 && keynum < 107) {
        returnVal = true;
    }

    numcheck = /[\b]|[\t]/

    var backspacecheck = /[\b]|[\t]/


    if (numcheck.test(keychar)) {
        returnVal = true;

    }


    return returnVal;
}

function ShowSubmissionSpinner() {

    //    $('#LoadingSymbol').slideDown("slow");
    $("#LoadingSymbol").dialog({
        height: 100,
        width: 140,
        modal: true,
        draggable: false,
        resizable: false,
        open: function (event, ui) { $(".ui-dialog-titlebar-close").hide(); }

    });


}

function HideSubmissionSpinner() {
    $('#LoadingSymbol').hide();
}

function ValidateVerificationCode() {

    var isValid = true;
    var selectedOptionVal = $("#<%= PlanToBuy.ClientID %> input:radio:checked").val();

    var isDoNotPlanToBuy = false;

    if (selectedOptionVal.toUpperCase() == "DO NOT PLAN TO BUY") {
        isDoNotPlanToBuy = true;

        var comments = $('#<%= txtComments.ClientID %>').val();

        if (comments.length == 0) {
            isValid = false;
            ShowToolTip('<%= txtComments.ClientID %>');
        }

    }

    if (isValid) {
        var verificationCode = $('#<%= CodeNumberTextBox.ClientID %>').val();
        var kioskModeValue = $('#<%= KioskModeHiddenField.ClientID %>').val();
        var result = false;


        $.getJSON("LeadSubmission.ashx", { code: verificationCode, kioskMode: kioskModeValue, action: 'Verification', isDoNotPlanToBuy: isDoNotPlanToBuy }, function (response) {

            if (response.IsValid) {

                if (response.HasModelsSelected == false) {

                    ActivateTab(1); // $('#SalesLeadFormTabs').tabs({ selected: 2 });

                    ShowToolTip('ProductSelectionTabLink', 'Oops, don\'t forget to add at least one model before continuing');
                } else {
                    ShowSubmissionSpinner();
                    $('#<%= btnSalesLeadFormHidden.ClientID %>').trigger("click");
                }

            } else {

                var verificationCode = $('#<%= CodeNumberTextBox.ClientID %>').val('');

                $('#CaptchaImage').attr('src', 'JpegImage.aspx?' + (new Date().getTime()));

                ActivateTab(2); // $('#SalesLeadFormTabs').tabs({ selected: 2 });

                ShowToolTip('<%= CodeNumberTextBox.ClientID %>', 'Mis-typed verification Code.  Please try again');

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


function FakeFill() {

    $("#<%= txtFirstName.ClientID %>").val("First");
    $("#<%= txtLastName.ClientID %>").val("Last");
    $("#<%= txtCompany_Agency.ClientID %>").val("Company");
    $("#<%= txtAddress1.ClientID %>").val("Address 1");
    $("#<%= txtAddress2.ClientID %>").val("Address 2");
    $("#<%= txtPhone.ClientID %>").val("Phone");
    $("#<%= txtFax.ClientID %>").val("Fax");
    $("#<%= txtEmail.ClientID %>").val("nwilson@nwilson.org");
    $("#Test Multiline Textbox").val('asdf');
}

function ShowToolTip(objectName, optionalMessage) {

    var toolTipMessage = 'Please fill in this required field';

    if (optionalMessage != undefined && optionalMessage.length > 0) {
        toolTipMessage = optionalMessage;
    }

    $('#' + objectName).qtip({
        content: toolTipMessage,
        position: {
            my: 'bottom middle',
            at: 'top middle',
            target: $('#' + objectName)
        },
        show: {
            event: false, // Only show when show() is called manually
            ready: true // Also show on page load
        },
        hide: {
            event: 'keypress onchange'
        }
    });


}