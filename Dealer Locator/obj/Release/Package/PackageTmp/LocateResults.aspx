<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocateResults.aspx.cs"
    Inherits="Dealer_Locator.LocateResults" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="css/main-ui.css" />
    <link rel="Stylesheet" type="text/css" href="css/LocateResults.css" />
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
</head>
<body>
    <form id="form1" runat="server">
    <div id="DistributorTabContent">
        <asp:Literal ID="litResults" runat="server"></asp:Literal>
        <div id="MoreInformationButtonSection">
            <div class="graybutton" style="margin-left: 12px;">
                <input id="btnSalesLeadFormRedirect" class="button" type="button" value="More Information"
                    runat="server" onclick="btnSalesLeadFormRedirect_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
