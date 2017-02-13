<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="MapReport.aspx.cs" Inherits="Dealer_Locator.admin.MapReport" %>

<head>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <link rel="Stylesheet" href="Styles/MapReport.css" />
    <link rel="Stylesheet" href="Styles/leaflet.ie.css" />
    <link rel="Stylesheet" href="Styles/leaflet.css" />
    <link rel="Stylesheet" href="Styles/leaflet.label.css" />
    <link rel="stylesheet" href="Styles/MapReportPrint.css" type="text/css" media="print" />
    <!--[if IE]>
    <style type="text/css">
        

        #legendWrapper
        {
            float: right;
            width: 100%;
        }

        #mapHeader
        {
            position: absolute;
        }

    </style>
    <![endif]-->
    <script type="text/javascript">//Ensures there will be no 'console is undefined' errors
        window.console = window.console || (function () {
            var c = {}; c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function () { };
            return c;
        })();
    </script>

    <script type="text/javascript" src="Scripts/jquery-1.10.0.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript" src="Scripts/leaflet-src.js"></script>
    <script type="text/javascript" src="Scripts/leaflet-google.js"></script>
    <script type="text/javascript" src="Scripts/leaflet.label.js"></script>
    <script type="text/javascript" src="Scripts/tinycolor.js"></script>
    <script type="text/javascript">

        var map;
        var geojson_mastercounties;

        var distributors = null;
        var stateList = [];

        var infowindow = new google.maps.InfoWindow();

        var mapReportData = null;

        function makeKey(data) {
            return data.join("_").toUpperCase();
        }

        function find(name, StateName) {
            var key = makeKey([name, StateName]);
            return index[key]; //this is an array of results
        }

        function onEachFeature(feature, layer) {
            // does this feature have a property named popupContent?
            if (feature.properties && feature.properties.popupContent) {
                layer.bindPopup(feature.properties.popupContent);
            }
        }

        function SendToCanvas() {
            var map = $("#map");

            canvg(window.document.getElementById('mapCanvas'), $(".leaflet-zoom-animated").html());

        }

        function getInternetExplorerVersion() {

            var rv = -1; // Return value assumes failure.
            console.log("NAVIGATOR: " + navigator.appName);

            if (navigator.appName == 'Microsoft Internet Explorer') {

                var ua = navigator.userAgent;

                var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");

                if (re.exec(ua) != null)

                    rv = parseFloat(RegExp.$1);

            }

            return rv;

        }

        function rgbToHex(R, G, B) { return toHex(R) + toHex(G) + toHex(B) }
        function toHex(n) {
            n = parseInt(n, 10);
            if (isNaN(n)) return "00";
            n = Math.max(0, Math.min(n, 255));
            return "0123456789ABCDEF".charAt((n - n % 16) / 16)
                 + "0123456789ABCDEF".charAt(n % 16);
        }

        var HSLColorGenerator = (function () {

            var _hue = 0;
            var _hueStep = 35;
            var _hueMin = 0;
            var _hueMax = 255;

            var _saturation = 100;
            var _saturationStep = -20;
            var _saturationMin = 0;
            var _saturationMax = 100;

            var _luminosity = 50;
            var _luminosityStep = 15;
            var _luminosityMin = 20;
            var _luminosityMax = 90;

            function HSLColorGenerator() { };

            HSLColorGenerator.prototype.GetNextColor = function GetNextColor() {
                //var hslColor = "hsl(" + _hue.toString() + "," + _saturation.toString() + "%," + _luminosity.toString() + "%)";
                var rgbArray = this.HSLToRGB(_hue / 100, _saturation / 100, _luminosity / 100);

                var hslColor = "hsl(" + _hue + ", " + _saturation + ", " + _luminosity + ")";
                var tiny = tinycolor(hslColor);

                var rgbString = tiny.toRgbString();
                rgbString = rgbString.replace("rgb", "rgba").replace(")", ",0.5)");

                //console.log(_hue + ":" + _saturation + ":" + _luminosity + " - " + rgbString);
                var rgbColor = rgbString;

                var version = getInternetExplorerVersion();

                if (version > -1) {
                    if (version < 9.0) {
                        rgbColor = tiny.toHexString();
                        console.log("IE 8 or lower detected - " + rgbColor);
                    } else {
                        rgbColor = tiny.toHexString();
                        console.log("IE 9 or higher detected");
                    }
                } else {
                    console.log("Non-IE browser detected");
                }

                _hue = parseInt(_hue) + parseInt(_hueStep);

                if (_hue > _hueMax) {
                    _hue = _hueMin;

                    _saturation = parseInt(_saturation) + parseInt(_saturationStep);
                    if (_saturation < _saturationMin) {
                        _saturation = _saturationMax;

                        _luminosity = parseInt(_luminosity) + parseInt(_luminosityStep);
                        if (_luminosity > _luminosityMax) {
                            _luminosity = _luminosityMin;

                        }
                    }
                }

                return rgbColor;
            }

            HSLColorGenerator.prototype.Reset = function Reset() {

                _hue = 0;
                _saturation = 100;
                _luminosity = 50;

            };

            HSLColorGenerator.prototype.HSLToRGB = function HSLToRGB(h, s, l) {
                var r, g, b;

                if (s == 0) {
                    r = g = b = l; // achromatic
                } else {
                    function hue2rgb(p, q, t) {
                        if (t < 0) t += 1;
                        if (t > 1) t -= 1;
                        if (t < 1 / 6) return p + (q - p) * 6 * t;
                        if (t < 1 / 2) return q;
                        if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
                        return p;
                    }

                    var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                    var p = 2 * l - q;
                    r = hue2rgb(p, q, h + 1 / 3);
                    g = hue2rgb(p, q, h);
                    b = hue2rgb(p, q, h - 1 / 3);
                }

                return [r * 255, g * 255, b * 255];
            };

            HSLColorGenerator.prototype.setLuminosity = function setLuminosity(luminosity) {

                _luminosity = luminosity;

            };
            HSLColorGenerator.prototype.setHue = function setHue(hue) {

                _hue = hue;

            };
            HSLColorGenerator.prototype.setSaturation = function setSaturation(saturation) {

                _saturation = saturation;

            };

            return HSLColorGenerator;

        })();




    </script>
</head>
<body onload="GetReportData();">
    <!--[if IE]>
    <div>
    
    <div id="map">
    </div>
        </div>
                <![endif]-->
    <div id="legendWrapper">
        <div id="legend" class="legend">
            <div id="ZoomButtonWrapper">
                <div id="ZoomIn" class="ZoomButton">+</div>
                <div id="ZoomOut" class="ZoomButton">-</div>
            </div>
            <div id="legendEntryWrapper">
            </div>
            <div id="ErrorWrapper">
            </div>
            <div id="footer">
                <div id="HideDistributors">
                    <input type="checkbox" id="HideDistributorsCheckbox" class="HideDistributorsCheckbox" runat="server" />Hide Distributors
                </div>
                <div id="MapLoading">Loading...<img src="../../images/spinner.gif" /></div>
                <div id="StateWrapper">
                    State:
                <select id="StateDropdown">
                    <option value="ChooseAState">Choose a State</option>
                </select>
                </div>
            </div>
        </div>
        <div id="mapHeader">
            <div id='legendTitle'>
                Legend
            </div>
            <div><span id="timeStamp"></span><span id="zoomLevel"></span><span id="IEMessage" style="display: none;">Map colors may not print correctly in IE</span></div>
            <div id="mapTitleLegendWrapper"></div>
        </div>
    </div>
    <!--[if !IE]><!-->
    <div>
    
    <div id="map">
    </div>
        </div>
                <![endif]-->
    <div id="mapTarget">
    </div>
    <%--        <canvas id="mapCanvas"></canvas>
        <div>
            <input type="button" onclick="javascript:SendToCanvas();" value="Send to Canvas" />
        </div>--%>
    <script type="text/javascript">
        // initialize the map on the "map" div with a given center and zoom
        //var map = new L.Map('map', { center: new L.LatLng(39.90, -88.92), zoom: 6 });
        var cloudmadeUrl = 'http://{s}.tile.cloudmade.com/4d97eea70b724664b587d23f7a531564/{styleId}/256/{z}/{x}/{y}.png',
        cloudmadeAttribution = '';

        var index = [];
        var geoZoom = [];
        var selectedState = '';
        var reportID = '';
        var bounds = new L.LatLngBounds();
        var overviewReport = false;
        var distMapReport = false;
        var unusedCounties = false;

        var overviewGroups = [];
        var masterGroupListIds = [];

        var minLat, maxLat, minLong, maxLong;
        minLat = 10000;
        minLong = 10000;
        maxLat = -10000;
        maxLong = -10000;

        // var minimal = L.tileLayer(cloudmadeUrl, { styleId: 22677, attribution: cloudmadeAttribution });
        // var map = new L.Map('map', { center: new L.LatLng(39.90, -88.92), zoom: 9, layers: [minimal] });
        var map = new L.Map('map', { center: new L.LatLng(39.90, -88.92), zoom: 9, zoomControl: false });

        var masterGroupList = [];
        var googleLayer = new L.Google('ROADMAP');
        //map.addLayer(googleLayer);

        //            var baseMaps = {
        //                "Minimal": minimal
        //            };

        //            L.control.layers(baseMaps).addTo(map);

        $(".HideDistributorsCheckbox").change(function () {
            if ($(this).is(':checked')) {
                $('.leaflet-marker-pane img').hide();
                $('.leaflet-shadow-pane img').hide();

            } else {
                $('.leaflet-marker-pane img').show();
                $('.leaflet-shadow-pane img').show();
            }

        });

        $("#ZoomIn").click(function () {
            var zoom = map.getZoom();
            zoom += 0.10;
            map.setZoom(zoom);
            $("#zoomLevel").text("Zoom: " + zoom.toFixed(2));
        });

        $("#ZoomOut").click(function () {
            var zoom = map.getZoom();
            zoom -= 0.10;
            map.setZoom(zoom);
            $("#zoomLevel").text("Zoom: " + zoom.toFixed(2));
        });

        function qs(key) {
            key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
            var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
            return match && decodeURIComponent(match[1].replace(/\+/g, " "));
        }

        function GetReportData() {

            var mapReportFile = qs("id");
            reportID = mapReportFile;

            $.ajax({
                url: 'Data/' + mapReportFile + '.json',
                dataType: 'json',
                cache: false,
                success: function (data) {
                    mapReportData = data;
                    var newDate = new Date();
                    var datetime = "Date Generated: " + newDate.today() + " " + newDate.timeNow();

                    $("#legendTitle").text(mapReportData.LegendTitle);
                    $("#timeStamp").text(datetime);

                    for (var i = mapReportData.Counties.length; i--;) {

                        var countyItem = mapReportData.Counties[i];



                    }

                    $.each(mapReportData.States, function (key, state) {
                        stateList.push(state);
                    });

                    stateList.sort();

                    //if ($.inArray(countyItem.StateName, stateList) == -1)
                    //    stateList.push(countyItem.StateName);

                    $.each(stateList, function (key, state) {
                        $("#StateDropdown").append('<option value="' + state + '">' + state + '</option>');
                    });

                    geojson_mastercounties = new Object();
                    geojson_mastercounties.features = [];

                    GetStateData();

                }
            });
        }

        var stateCountyMap = [52];

        function GetStateData() {

            _d = 0;

            function fireRequest(state) {
                return $.ajax({
                    url: 'GeoData/' + state + '.json',
                    dataType: 'json',
                    cache: true,
                    success: function (data) {
                        //console.log("Getting JSON Data for: " + state);


                        var geojson_temp = data;

                        // create haskey map for loaded counties
                        for (var geoItem in geojson_temp.features) {
                            var curItem = geojson_temp.features[geoItem];

                            var stateName = curItem.properties.STATE_NAME;
                            var countyName = curItem.properties.NAME;
                            var key = makeKey([countyName, stateName]);

                            index[key] = index[key] || [];
                            index[key].push(curItem);

                            stateCountyMap[stateName] = stateCountyMap[stateName] || [];

                            stateCountyMap[stateName].push(curItem);

                        }

                        if (_d == stateList.length) {
                            console.log('JSON File Load Complete');

                            $("#MapLoading").hide();

                            if (qs("DistMapReport") == "true")
                                distMapReport = true;

                            if (qs("overview") == "true") {



                                $('#HideDistributors').show();

                                overviewReport = true;
                                $("#StateWrapper").hide();

                                console.log('overview report detected');

                                $.each(stateList, function (key, state) {

                                    if (state != "Guam and New Pacific Area") {
                                        //$("#StateDropdown").append('<option value="' + state + '">' + state + '</option>');
                                        //console.log("Overview: Loading state: " + state);
                                        LoadJSON(state);
                                        //console.log("Load of " + state + " complete");
                                    }
                                });

                                BuildLegend(unusedCounties);

                                map.panTo(new L.LatLng(39.50, -98.35));
                                //LAT. 39°50' LONG. -98°35'

                                console.log("Overview map load complete");
                                map.setZoom(3.2);
                                $("#zoomLevel").text("Zoom: 4.2");
                                $("#legendTitle").text(mapReportData.LegendTitle + " - Overview Map Report");

                            } else {
                                $("#StateWrapper").show();
                            }

                            //LoadJSON

                        }


                    }
                });
            }

            var startingpoint = $.Deferred();
            startingpoint.resolve();

            $.each(stateList, function (key, state) {
                startingpoint = startingpoint.pipe(function () {

                    _d = _d + 1;

                    if (state == "Guam and New Pacific Area") {

                    } else {
                        //console.log("making requst for " + state);
                        return fireRequest(state);
                    }
                });
            });


        }

        Date.prototype.today = function () {
            return (((this.getMonth() + 1) < 10) ? "0" : "") + (this.getMonth() + 1) + "/" + ((this.getDate() < 10) ? "0" : "") + this.getDate() + "/" + this.getFullYear()
        };
        //For the time now
        Date.prototype.timeNow = function () {
            return ((this.getHours() < 10) ? "0" : "") + this.getHours() + ":" + ((this.getMinutes() < 10) ? "0" : "") + this.getMinutes() + ":" + ((this.getSeconds() < 10) ? "0" : "") + this.getSeconds();
        };

        $("#StateDropdown").change(function () {

            selectedState = $("#StateDropdown option:selected").text();
            ClearMap();

            if (selectedState != "ChooseAState")
                LoadJSON(selectedState);
        });

        function ResetMinMaxLatLong() {
            minLat = 10000;
            minLong = 10000;
            maxLat = -10000;
            maxLong = -10000;
        }

        function LoadJSON(selectedState) {
            var colorGen = new HSLColorGenerator();

            console.log("overviewReport:" + overviewReport)
            if (overviewReport == false) {
                console.log('reset');
                colorGen.Reset();
                ResetMinMaxLatLong();
                masterGroupListIds = [];
                unusedCounties = false;
            }

            var marker;
            //marker.addTo(map);
            //marker.removeFrom(map);


            $("#legendTitle").text(mapReportData.LegendTitle + ' - ' + selectedState);


            geojson_mastercounties.features = [];  // no longer needed, clear to save memory


            masterGroupList = [];

            //colorGen.Reset();

            bounds = new L.LatLngBounds();
            var boundsExtened = false;

            geoZoom = [];

            var mappedCounties = [];
            var mappedSplitCounties = [];

            // loop all counties in the JSON data from the groups
            // update the loaded counties, adding the properties for styling to match those from the group counties
            for (var i = mapReportData.Counties.length; i--;) {
                //mapReportData.Counties[i].Style
                var countyItem = mapReportData.Counties[i];

                var stateName = countyItem.StateName;

                if (stateName == selectedState) {
                    if ($.inArray(countyItem.GroupID, masterGroupListIds) == -1) {

                        if (mapReportData.Groups[countyItem.GroupID] === undefined)
                            console.log("WARNING: countyItem.GroupID is undefined: " + countyItem.GroupID + " - " + countyItem.Name);  // ohio, Wyandot county

                        var newColor = false;

                        if (mapReportData.Groups[countyItem.GroupID].IsOverlap == false) {

                            if ($.inArray(countyItem.GroupID, masterGroupListIds) < 0) {
                                masterGroupListIds.push(countyItem.GroupID);
                                newColor = true;
                                var color = colorGen.GetNextColor();
                                //console.log("Got new color: " + countyItem.GroupID);
                                mapReportData.Groups[countyItem.GroupID].Style.FillColor = color;
                                mapReportData.Groups[countyItem.GroupID].Style.StrokeColor = "#000000";
                            }
                        } else {
                            newColor = true;
                            mapReportData.Groups[countyItem.GroupID].Style.StrokeColor = "red";

                            if ($.inArray(countyItem.GroupID, masterGroupListIds) < 0)
                                masterGroupListIds.push(countyItem.GroupID);
                        }

                        mapReportData.Groups[countyItem.GroupID].Style.StrokeWeight = 1.0;
                        //

                        //if ($.inArray(countyItem.GroupID, masterGroupListIds) < 0)
                        //    masterGroupListIds.push(countyItem.GroupID);
                        //if (newColor)
                        //    masterGroupListIds.push(countyItem.GroupID);
                    } else {

                    }

                    var countyName = countyItem.Name;
                    mappedCounties.push(countyName);



                    if (countyItem.IsSplit) {
                        //    countyName = countyItem.SplitCountyName;
                        mappedSplitCounties.push(countyItem.SplitCountyName);
                    }

                    var key = makeKey([countyName, stateName]);

                    //console.log('Adding ' + countyName + ' (' + stateName + ')');

                    if (index[key] === undefined) {
                        console.log('WARNING: Cannot find geometry data for ' + key);
                        LogError('Cannot find Geometry data', key);
                    } else {

                        var geoItem = index[key][0];

                        ParseBounds(geoItem, countyItem);

                        // Set the leaflet specific properties
                        var groupItem = mapReportData.Groups[countyItem.GroupID];

                        geoItem.properties.style = [];
                        geoItem.properties.style.color = groupItem.Style.StrokeColor;
                        geoItem.properties.style.opacity = groupItem.Style.StrokeOpacity;

                        geoItem.properties.style.weight = groupItem.Style.StrokeWeight;
                        geoItem.properties.style.fillColor = groupItem.Style.FillColor;

                        if (mapReportData.Groups[countyItem.GroupID].IsOverlap == false) {
                            //geoItem.properties.style.fillOpacity = 0.5;  // groupItem.Style.FillOpacity;
                            geoItem.properties.style.fillOpacity = 1.0;  // groupItem.Style.FillOpacity;

                        } else {
                            geoItem.properties.style.fillOpacity = 0;
                            geoItem.properties.style.weight = 3.0;
                        }

                        geoItem.properties.popupContent = "";

                        var drawCounty = true;

                        if (geoItem.properties.hasOwnProperty("draw_county") && geoItem.properties.draw_county == "false")
                            drawCounty = false;

                        if (drawCounty) {
                            if (overviewReport == false)
                                PlotCountyLabel(geoItem, countyName);

                            L.geoJson(geoItem, { style: geoItem.properties.style }).addTo(map);
                        }

                    }
                }
            }

            console.log("masterGroupListIds.length: " + masterGroupListIds.length);

            for (var groupItem in mapReportData.Groups) {
                var curItem = mapReportData.Groups[groupItem];

                if ($.inArray(curItem.GroupID, masterGroupListIds) > -1)
                    masterGroupList.push(curItem);

            }

            // loop mappedCounties, plot any un-used counties
            for (var n = 0; n < stateCountyMap[selectedState].length; n++) {
                var masterCountyName = stateCountyMap[selectedState][n].properties.NAME;

                if ($.inArray(masterCountyName, mappedCounties) < 0 && $.inArray(masterCountyName, mappedSplitCounties) < 0) {
                    PlotEmptyCounty(stateCountyMap[selectedState][n]);
                    unusedCounties = true;
                }
            }

            if (overviewReport == false) {
                var center = bounds.getCenter();
                map.panTo(new L.LatLng(center.lat, center.lng));

                var maxZoom = map.getBoundsZoom(geoZoom);
                //map.setZoom(maxZoom);

                map.fitBounds(bounds);

                $("#zoomLevel").text("Zoom: " + map.getBoundsZoom(bounds).toFixed(2));

            }
            
            if (navigator.appName == 'Microsoft Internet Explorer') {
                $("#IEMessage").show();
                console.log("IEMessage");
            }

            for (var subGroupItem in mapReportData.SubGroups) {
                var curItem = mapReportData.SubGroups[subGroupItem];

                if (curItem.ContractState == selectedState) {
                    SetMarker(curItem);
                }
            }


            //var mapZoom = map.getZoom();
            if (overviewReport == false) {
                BuildLegend(unusedCounties);
            }


            //google.maps.event.addListener(map, 'bounds_changed', function () {
            //    console.log(map.getZoom());
            //    $("#zoomLevel").text("Zoom: " + this.getZoom().toFixed(2));
            //});

        }

        function ParseBounds(geoItem) {

            if (!(geoItem.geometry.coordinates === undefined)) {

                // Do the split county logic here

                for (var k = 0; k < geoItem.geometry.coordinates.length; k++) {
                    for (var j = 0; j < geoItem.geometry.coordinates[k].length - 1; j++) {
                        window.geoZoom.push(geoItem.geometry.coordinates[k][j]);

                        var tempLat = geoItem.geometry.coordinates[k][j][0];
                        var tempLong = geoItem.geometry.coordinates[k][j][1];

                        var myLatLng = new google.maps.LatLng(tempLong, tempLat);
                        var leafletBounds = new L.LatLng(tempLong, tempLat);
                        bounds.extend(leafletBounds);


                        if (tempLat < minLat)
                            minLat = tempLat;

                        if (tempLong < minLong)
                            minLong = tempLong;

                        if (tempLat > maxLat)
                            maxLat = tempLat;

                        if (tempLong > maxLong)
                            maxLong = tempLong;
                    }
                }

            }
        }

        function PlotCountyLabel(geoItem, countyName) {
            var countyCentroid = null;

            var centroidCoordinates = [];

            if (geoItem.geometry.type == "MultiPolygon") {
                var innerCoords = [];

                for (var k = 0; k < geoItem.geometry.coordinates.length; k++) {
                    for (var j = 0; j < geoItem.geometry.coordinates[k].length; j++) {
                        for (var l = 0; l < geoItem.geometry.coordinates[k][j].length; l++) {
                            innerCoords.push(geoItem.geometry.coordinates[k][j][l]);
                        }
                    }
                }

                centroidCoordinates.push(innerCoords);
            } else {
                centroidCoordinates = geoItem.geometry.coordinates;
            }

            countyCentroid = GetCentroid(centroidCoordinates);
            /*  REVERT
            var offset = -1 * ((4 * countyName.length) + 27);
            */
            var offset = -1 * ((4 * countyName.length) + 5);

            var myIcon = L.icon({
                iconUrl: 'Styles/images/empty.png',
                iconSize: [16, 16],
                iconAnchor: [0, 0],
                labelAnchor: [offset, 7] // as I want the label to appear 2px past the icon (10 + 2 - 6)
            });

            if (geoItem.properties.hasOwnProperty("IsSplit") && geoItem.properties.IsSplit == "true") {

                if (countyName.indexOf("East") > -1 || countyName.indexOf("North") > -1) {
                    if (countyName.indexOf("East") > -1) {
                        countyCentroid[1] += 0.15;  // y
                        countyCentroid[0] += 0.35;
                    }
                    else {
                        countyCentroid[1] += 0.05;  // y
                        countyCentroid[0] += 0.25;
                    }
                } else {

                    if (countyName.indexOf("West") > -1) {
                        countyCentroid[1] -= 0.15;  // y
                        countyCentroid[0] += 0.25;
                    }
                    else {
                        countyCentroid[1] += 0.05;  // y
                        countyCentroid[0] += 0.25;
                    }

                    countyCentroid[0] += 0.25;
                }


            }

            if (countyCentroid != null) {
                var marker = L.marker([countyCentroid[1], countyCentroid[0]], {

                    icon: myIcon,
                    offset: new L.Point(-15, -15)

                });

                marker.bindLabel(countyName, { noHide: true });
                marker.addTo(map).showLabel();

            }
        }

        function PlotEmptyCounty(geoItem) {

            geoItem.properties.style = [];
            geoItem.properties.style.color = "#000000";
            geoItem.properties.style.opacity = 1.0;

            geoItem.properties.style.weight = 1;
            geoItem.properties.style.fillColor = "#FFFFFF";
            geoItem.properties.style.fillOpacity = 1.0;

            var drawCounty = true;

            if (geoItem.properties.hasOwnProperty("draw_county") && geoItem.properties.draw_county == "false")
                drawCounty = false;

            if (drawCounty) {
                //console.log(overviewReport);
                if (overviewReport == false) {
                    PlotCountyLabel(geoItem, geoItem.properties.NAME);
                }

                L.geoJson(geoItem, { style: geoItem.properties.style }).addTo(map);

            }

            ParseBounds(geoItem);
        }

        function BuildLegend(unusedCounties) {

            var mapTitleLegendWrapper = $("#mapTitleLegendWrapper");

            console.log(masterGroupList.length);

            for (var groupItemIndex in masterGroupList) {
                var curItem = masterGroupList[groupItemIndex];
                var itemName = curItem.Name;
                var fillColor = curItem.Style.FillColor;
                var isOverlap = curItem.IsOverlap;
                console.log(itemName);
                if (isOverlap == false && itemName.indexOf(' ') > -1) {
                    var itemArray = itemName.split(' ').slice(0, 2);
                    itemName = itemArray[0] + ' ' + itemArray[1];
                }

                if (overviewReport == false || (overviewReport == true && isOverlap == false)) {

                    var divLegendEntry = BuildLegendEntry(itemName, fillColor, isOverlap);

                    $(divLegendEntry).appendTo(mapTitleLegendWrapper);
                }

            }

            if (unusedCounties) {
                if (overviewReport == false && distMapReport == false) {
                    var divLegendEntry = BuildLegendEntry("None", "#FFFFFF", false);
                    $(divLegendEntry).appendTo(mapTitleLegendWrapper);
                }
            }
        }

        function BuildLegend_Back(unusedCounties) {

            var legendEntryWrapper = $("#legendEntryWrapper");

            for (var groupItemIndex in masterGroupList) {
                var curItem = masterGroupList[groupItemIndex];
                var itemName = curItem.Name;
                var fillColor = curItem.Style.FillColor;
                var isOverlap = curItem.IsOverlap;

                var divLegendEntry = BuildLegendEntry(itemName, fillColor, isOverlap);

                $(divLegendEntry).appendTo(legendEntryWrapper);

            }

            if (unusedCounties) {

                var divLegendEntry = BuildLegendEntry("No Distributor", "#FFFFFF", false);
                $(divLegendEntry).appendTo(legendEntryWrapper);
            }
        }

        function BuildLegendEntry(itemName, fillColor, isOverlap) {
            var divLegendEntry = "<span class='legendEntry'><div class='legendColor";

            if (isOverlap)
                divLegendEntry += " Overlap";
            else
                divLegendEntry += "' style='background-color:" + fillColor;

            divLegendEntry += "'></div><div class='legendName'>" + itemName + "</div></span>";

            return divLegendEntry;
        }

        function ClearMap() {
            var m = map;

            for (i in m._layers) {
                if (m._layers[i]._path != undefined) {
                    try {
                        m.removeLayer(m._layers[i]);
                    }
                    catch (e) {
                        console.log("WARNING: problem with " + e + m._layers[i]);
                    }
                }
            }

            $(".leaflet-label").remove();
            $(".leaflet-marker-pane > img").remove();
            $(".leaflet-shadow-pane > img").remove();
            $(".legendEntry").remove();
            $(".legendEntryWrapper > div").remove();
            $("#ErrorWrapper > div").remove();
        }

        $(function () {
            // $("#legend").draggable();
        });

        function SetMarker(curItem) {
            var address = curItem.Address;
            var city = curItem.City;
            var state = curItem.State;
            var stateAbbreviation = curItem.StateAbbreviation;
            var zip = curItem.Zip;

            var lat = curItem.Lat;
            var lng = curItem.Lng;

            lat = lat || null;
            lng = lng || null;

            if (city == "Paducah") {
                lat = 37.0249475;
                lng = -88.6715905;

                console.log("Paducah Lat/Lng override");
            }

            if (lat != null || lng != null) {
                PlaceMarkerLatLng(curItem.Name, address, city, stateAbbreviation, zip, lat, lng);
                //console.log("Loading from data");
            } else {

                address = address.replace('(Hwy.)', '');
                var googleUrl = "http://maps.googleapis.com/maps/api/geocode/json?address=" + address + "," + city + "," + state + " " + zip + "&sensor=false";
                var googleUrl2 = "http://maps.googleapis.com/maps/api/geocode/json?address=" + city + "," + state + " " + zip + "&sensor=false";

                //console.log(googleUrl);
                //console.log(googleUrl2);

                $.ajax({
                    dataType: "json",
                    url: googleUrl,
                    success: function (data) {
                        if (data.status == "ZERO_RESULTS") {
                            console.log(googleUrl);

                            $.ajax({
                                dataType: "json",
                                url: googleUrl2,
                                success: function (data2) {
                                    console.log("Secondary Geocode without address: " + googleUrl2);
                                    if (data2.status == "ZERO_RESULTS") {
                                        console.log(googleUrl);
                                        LogError("Geocode Failed", googleUrl + " : " + googleUrl2);
                                    } else {
                                        PlaceMarker(curItem.Name, address, city, stateAbbreviation, zip, data2.results[0].geometry.location);
                                    }
                                },
                                error: function (request, error) { alert(request + " " + error); }
                            });

                        } else {
                            if (data.results[0] === undefined) {
                                console.log("WARNING: data.results[0] is undefined: " + data.status + " - " + curItem.name + " - " + address + " - " + city + " - " + stateAbbreviation + " - " + zip);
                            } else {
                                PlaceMarker(curItem.Name, address, city, stateAbbreviation, zip, data.results[0].geometry.location);
                            }
                        }
                    },
                    error: function (request, error) { alert(request + " " + error); }
                });
            }
        }

        function PlaceMarker(name, address, city, state, zip, location) {
            var lat = location.lat;
            var lng = location.lng;

            //console.log("Place Marker: " + name);

            PlaceMarkerLatLng(name, address, city, state, zip, lat, lng);

        }

        function PlaceMarkerLatLng(name, address, city, state, zip, lat, lng) {

            var popupContent = "<b>" + name + "</b><BR>" + address + "<BR>" + city + ", " + state + " " + zip;

            var marker = L.marker([lat, lng], { title: name }).bindPopup(popupContent);
            //console.log("Place Marker Lat Lng: " + name);
            marker.addTo(map);
        }

        function GetCentroid(paths) {
            var f;
            var x = 0;
            var y = 0;
            var nPts = paths[0].length;
            var j = nPts - 1;
            var area = 0;

            for (var i = 0; i < nPts; j = i++) {
                var pt1 = paths[0][i];
                var pt2 = paths[0][j];
                f = pt1[0] * pt2[1] - pt2[0] * pt1[1];
                x += (pt1[0] + pt2[0]) * f;
                y += (pt1[1] + pt2[1]) * f;

                area += pt1[0] * pt2[1];
                area -= pt1[1] * pt2[0];
            }
            area /= 2;
            f = area * 6;
            return [x / f, y / f];
        }

        function LogError(error, additionalData) {
            var errorWrapper = $("#ErrorWrapper");

            additionalData = additionalData || '';

            var divErrorEntry = '<div class="errorEntry">Error: ' + error;

            if (additionalData.length > 0)
                divErrorEntry += '   >>   ' + additionalData;

            divErrorEntry += '   >>   <b>[ ' + reportID + ' , ' + selectedState + ' ]</b>';
            divErrorEntry += '</div>';

            $(divErrorEntry).appendTo(errorWrapper);

        }


    </script>
</body>
