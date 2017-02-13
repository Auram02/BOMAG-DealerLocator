var _territoryRepID = -1;

function ShowAdminInterface() {
    $("#TMReportTM").show();
}

function ShowTerritoryRepInterface(repID) {
    _territoryRepID = repID;

    SetupUI(_territoryRepID);

    $("#TMMapReport1").show();
    $("#TMMapReport2").show();

}

function SetupUI(territoryRepID) {
    //mainCategories
    //states
    $.ajax({
        url: "/admin/Reports/ReportHandler.ashx",
        data: { action: "GetStates", repID: territoryRepID },
        dataType: "json",
        success: function (response) {
            console.log('states complete');

            dropdown = $(".StatesList1");

            $(dropdown).empty();

            $.each(response, function (i, dataItem) {

                $("<option />", { val: dataItem.Key, text: dataItem.Value }).appendTo($(dropdown));

                console.log(dataItem.Key + " - " + dataItem.Value);
            });
        }
    });


    $.ajax({
        url: "/admin/Reports/ReportHandler.ashx",
        data: { action: "GetDistributors", repID: territoryRepID },
        dataType: "json",
        success: function (response) {
            console.log('GetDistributors complete');

            var dropdown = $(".DistributorList2");
            $(dropdown).empty();
            $("<option />", { val: "", text: "Select..." }).appendTo($(dropdown));

            $.each(response, function (i, dataItem) {
                $("<option />", { val: dataItem.Key, text: dataItem.Value }).appendTo($(dropdown));

                console.log(dataItem.Key + " - " + dataItem.Value);
            });
        }
    });
}

function ClearUI() {
    $("#TMMapReport1").hide();
    $("#TMMapReport2").hide();

    
}

$(document).ready(function () {

    $('.TMList').change(function () {
        console.log('change');

        $("#TMMapReport1").show();
        $("#TMMapReport2").show();

        $("#TMReport1GenerateWrapper").show();

        $("#ProductLinesList2").hide();

        if ($(this).val() != "") {
            // get state list and product list
            console.log("TM changed");

            _territoryRepID = $(this).val();
        
            SetupUI(_territoryRepID);

        } else {
            console.log("TM changed to Empty");
            ClearUI();
        }

    });

    $(".StatesList1").change(function () {
        var selectedStateList = [];
        $(".StatesList1 > option:selected").each(function () {
            selectedStateList.push($(this).text());

        });

        $.ajax({
            url: "/admin/Reports/ReportHandler.ashx",
            data: { action: "GetProductLinesByStateList", repID: _territoryRepID, stateNameList: selectedStateList },
            dataType: "json",
            success: function (data) {
                console.log('Get ProductLines - Region Map Report complete');
                var dropdown = $('.ProductLinesList1');
                $(dropdown).empty();

                $.each(data, function (i, dataItem) {
                    $("<option />", { val: dataItem.Key, text: dataItem.Value }).appendTo($(dropdown));

                    console.log(dataItem.Key + " - " + dataItem.Value);
                });
            }
        });


    });

    $('.DistributorList2').change(function () {

        var distributorID = $(this).val();

        if (distributorID != "") {


            $("#TMReport2GenerateWrapper").show();
            $("#TMProductLinesWrapper2").show();
            $("#ProductLinesList2").show();

            $.ajax({
                url: "/admin/Reports/ReportHandler.ashx",
                data: { action: "GetProductLines", DistributorID: distributorID },
                dataType: "json",
                success: function (data) {
                    console.log('GetProductLines complete');
                    var dropdown = $(".ProductLinesList2");
                    $(dropdown).empty();

                    $.each(data, function (i, dataItem) {
                        $("<option />", { val: dataItem.Key, text: dataItem.Value }).appendTo($(dropdown));

                        console.log(dataItem.Key + " - " + dataItem.Value);
                    });
                }
            });
        } else {
            $(".ProductLinesList2").empty();
            $("#TMReport2GenerateWrapper").hide();
            $("#TMProductLinesWrapper2").hide();
            $("#ProductLinesList2").hide();
        }

    });


    $('.GenerateReportButton1').click(function (e) {
        e.preventDefault();
        
        var territoryRepID = _territoryRepID;
        var selectedCategoryID = $(".ProductLinesList1").val();
        var selectedCategoryName = $(".ProductLinesList1 option:selected").text();

        var selectedStateList = [];

        $(".StatesList1 > option:selected").each(function () {
            selectedStateList.push($(this).text());

        })


        console.log("stateNameList: " + selectedStateList + " - territoryRepID: " + territoryRepID + " - CategoryID/Name: " + selectedCategoryID + "/" + selectedCategoryName);
        if (selectedStateList.length > 0) {

            $("#ReportLoadingDialog").dialog("open");

            $.ajax({
                url: "/admin/Reports/ReportHandler.ashx",
                data: { action: "TMMapReport1", repID: territoryRepID, stateNameList: selectedStateList, categoryID: selectedCategoryID, categoryName: selectedCategoryName },
                dataType: "json",
                success: function (response) {
                    console.log("TMMapReport1");
                    window.open("/admin/Reports/MapReport.aspx?id=" + response, '', 'toolbar=1,titlebar=1,menubar=1,width=' + screen.width + ',height=' + screen.height + ',resizable=1');

                    $("#ReportLoadingDialog").dialog("close");

                }
            });

        } else {
            alert("Please select at least one state");
        }

        
        console.log('Generate 1');

    });

    $('.GenerateReportButton2').click(function (e) {
        console.log('Generate 2');
        e.preventDefault();

        var productIDList = [];
        
        $(".ProductLinesList2 > option:selected").each(function () {
            productIDList.push($(this).val());

        })



        var territoryRepID = _territoryRepID;
        var distributorID = $('.DistributorList2').val();

        if (productIDList != null && productIDList.length > 0) {

            $("#ReportLoadingDialog").dialog("open");

            $.ajax({
                url: "/admin/Reports/ReportHandler.ashx",
                data: { action: "TMMapReport2", repID: territoryRepID, distributorID: distributorID, categoryIDList: productIDList },
                dataType: "json",
                success: function (response) {
                    console.log("TMMapReport2");

                    
                    $.each(response, function (i, dataItem) {
                        window.open("/admin/Reports/MapReport.aspx?id=" + dataItem.Value + "&DistMapReport=true", '', 'toolbar=1,titlebar=1,menubar=1,width=' + screen.width + ',height=' + screen.height + ',resizable=1');

                        console.log(dataItem.Key + " - " + dataItem.Value);
                    });
                    
                    $("#ReportLoadingDialog").dialog("close");

                }
            });

        } else {
            alert("Please select at least one product line");
        }

    });

    $(".GenerateOverviewReportButton").click(function (e) {
        e.preventDefault();

        console.log("Generate overview report");
        
        var productIDList = [];

        $(".TMProductLinesOverviewList > option:selected").each(function () {
            productIDList.push($(this).val());

        })

        if (productIDList != null && productIDList.length > 0) {

            $("#ReportLoadingDialog").dialog("open");

            $.ajax({
                url: "/admin/Reports/ReportHandler.ashx",
                data: { action: "OverviewReport", categoryIDList: productIDList },
                dataType: "json",
                success: function (response) {
                    console.log("Overview Report");


                    $.each(response, function (i, dataItem) {
                        window.open("/admin/Reports/MapReport.aspx?id=" + dataItem.Value + "&overview=true", '', 'toolbar=1,titlebar=1,menubar=1,width=' + screen.width + ',height=' + screen.height + ',resizable=1');

                        console.log(dataItem.Key + " - " + dataItem.Value);
                    });

                    $("#ReportLoadingDialog").dialog("close");


                }
            });

        } else {
            alert("Please select at least one product line");
        }

    });

    $("#ReportLoadingDialog").dialog({
        autoOpen: false,
        width: 350,
        height: 200,
        resizeable: false,
        modal: true

    });


});
