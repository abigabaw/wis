<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ViewPAPMap.aspx.cs" Inherits="WIS.UI.PROJECT.ViewPAPMap" %>
<!DOCTYPE html>
<html>
  <head>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <title>Wayleaves Information System - PAP Location</title>
    <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 5 }
      #map-canvas { height: 600px }
    </style>
    <script type="text/javascript"
      src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCBnl0YlJH1-lh0a4joW4_rUx5oBIhPDD0&sensor=false">
    </script>
    <script type="text/javascript">
        function initialize(coordinateLst) {
            directionsDisplay = new google.maps.DirectionsRenderer();
            //var bengaluru = new google.maps.LatLng(12.9833, 77.5833);
            
            var lat1 = coordinateLst.split(';');
            var iMax = lat1.length;
            var centerCoordIndex = parseInt(iMax / 2);
            coordinat = lat1[centerCoordIndex].replace(/'/g, '');
            arr = coordinat.split(',');

            var bengaluru = new google.maps.LatLng(arr[0], arr[1]);
            var mapOptions = {
                zoom: 8,
                center: bengaluru,
                mapTypeId: google.maps.MapTypeId.TERRAIN
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'),
                                mapOptions);

            
            var jMax = 4;
            var beaches = new Array();

            for (i = 0; i < iMax; i++) {
                beaches[i] = new Array();
                coordinat = lat1[i].replace(/'/g, '');
                arr = coordinat.split(',');

                beaches[i][0] = 'Route: ' + arr[2] + '\nLat: ' + arr[0] + '\nLng:' + arr[1];
                beaches[i][1] = arr[0];
                beaches[i][2] = arr[1];
                beaches[i][3] = i + 1;
                beaches[i][4] = 'Route: ' + arr[2] ;
            }

            //setMarkers(map, beaches);
            
            coordinateLstPap = "<%= GetPap() %>";
            if(coordinateLstPap.toString() == ''){
                alert('Pap Coordinates do not exist.');
                window.close();
            }
            initializePapMap(map, coordinateLstPap);
            //var lat = ['12.3024, 76.6386', '12.5167, 76.9000', '12.6500, 77.2167', '12.7200, 77.3000', '12.8167, 77.4000', '12.9833, 77.5833']
            var lat = coordinateLst.split(';');

            var latlng = [];

            for (var i = 0; i < lat.length; i++) {
                coordinat = lat[i].replace(/'/g, '');
                routeCoordinates = coordinat.split(',');
                //routeCoordinates = lat[i].split(',');

                latlng[i] = new google.maps.LatLng(routeCoordinates[0], routeCoordinates[1]);
            }

            var flightPlanCoordinates = latlng;

            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);
        }

        function calcRoute(coordinateLst) {
            var bengaluru = "12.9833" + ", 77.5833";
            var Mysore = "12.3024" + ", 76.6386";
            var start = bengaluru;
            var end = Mysore;

            var arrCoordinates = start.split(",");
            var myLatLngStart = new google.maps.LatLng(arrCoordinates[0], arrCoordinates[1]);

            arrCoordinates = end.split(",");
            var myLatLngEnd = new google.maps.LatLng(arrCoordinates[0], arrCoordinates[1]);

            var request = {
                origin: myLatLngStart,
                destination: myLatLngEnd,
                waypoints: waypts,
                optimizeWaypoints: true,
                travelMode: google.maps.DirectionsTravelMode.WALKING
            };

            //alert(request.origin);
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });
        }

        
        function initializePapMap(map, coordinateLst) {
            var lat1 = coordinateLst.split(';');
            var iMax = lat1.length;
            var centerCoordIndex = parseInt(iMax / 2);
            coordinat = lat1[centerCoordIndex].replace(/'/g, '');
            arr = coordinat.split(',');


            var jMax = 4;
            var beaches = new Array();

            for (i = 0; i < iMax; i++) {
                beaches[i] = new Array();
                coordinat = lat1[i].replace(/'/g, '');
                arr = coordinat.split(',');

                beaches[i][0] = 'Route: ' + arr[2] + '\nLat: ' + arr[0] + '\nLng:' + arr[1];
                beaches[i][1] = arr[0];
                beaches[i][2] = arr[1];
                beaches[i][3] = i + 1;
                beaches[i][4] = 'Route: ' + arr[2];
            }

            setMarkers(map, beaches);


            //var lat = ['12.3024, 76.6386', '12.5167, 76.9000', '12.6500, 77.2167', '12.7200, 77.3000', '12.8167, 77.4000', '12.9833, 77.5833']
            var lat = coordinateLst.split(';');

            var latlng = [];

            for (var i = 0; i < lat.length; i++) {
                coordinat = lat[i].replace(/'/g, '');
                routeCoordinates = coordinat.split(',');
                //routeCoordinates = lat[i].split(',');

                latlng[i] = new google.maps.LatLng(routeCoordinates[0], routeCoordinates[1]);
            }

            var flightPlanCoordinates = latlng;

            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);
        }


        function setMarkers(map, locations) {
            // Add markers to the map

            // Marker sizes are expressed as a Size of X,Y
            // where the origin of the image (0,0) is located
            // in the top left of the image.

            // Origins, anchor positions and coordinates of the marker
            // increase in the X direction to the right and in
            // the Y direction down.
            var image = {
                url: '../../image/circle_orange.png',
                // This marker is 20 pixels wide by 32 pixels tall.
                size: new google.maps.Size(20, 20),
                // The origin for this image is 0,0.
                origin: new google.maps.Point(0, 0),
                // The anchor for this image is the base of the flagpole at 0,32.
                //anchor: new google.maps.Point(-10, 0)
            };
            var shadow = {
                url: '../../image/circle_orange.png',
                // The shadow image is larger in the horizontal dimension
                // while the position and offset are the same as for the main image.
                size: new google.maps.Size(20, 20),
                origin: new google.maps.Point(0, 0),
                //anchor: new google.maps.Point(-10, 0)
            };
            // Shapes define the clickable region of the icon.
            // The type defines an HTML &lt;area&gt; element 'poly' which
            // traces out a polygon as a series of X,Y points. The final
            // coordinate closes the poly by connecting to the first
            // coordinate.
            var shape = {
                coord: [1, 1, 1, 20, 18, 20, 18, 1],
                type: 'poly'
            };
            var PAPdata= "<%= GetPapData() %>";
            for (var i = 0; i < locations.length; i++) {
                var beach = locations[i];
                var myLatLng = new google.maps.LatLng(beach[1], beach[2]);              
                
                var contentString = '<div>'+
                      '<div>'+
                      '</div>'+
                      '<div>'+
                      '<p>'+PAPdata+' <br /><b>Lat: </b>' + beach[1] + ' <br /><b>Lng: </b>' + beach[2] +
                      '</div>'+
                      '</div>';
//              var  infowindow = new google.maps.InfoWindow({
//                    content: contentString,
//                    position: myLatLng,
//                    zIndex: beach[3]
//                });
                var infowindow = new google.maps.InfoWindow;
              var  marker = new google.maps.Marker({
                    position: myLatLng,
                    map: map,
//                    shadow: shadow,
//                    icon: image,
                    shape: shape,
                    title: beach[0],
                    zIndex: beach[3]
                });
                
                bindInfoW(map, marker, contentString, infowindow);
//                google.maps.event.addListener(marker, 'click', function() {
//                infowindow.open(map,marker);
//                });
            }
        }
        
        function bindInfoW(map, marker, contentString, infowindow){
                google.maps.event.addListener(marker, 'click', function() {
                    infowindow.setContent(contentString);
                    infowindow.open(map, marker);
                });
        }
        //google.maps.event.addDomListener(window, 'load', initialize);

        function HideMap() {
            var summaryPanel = document.getElementById('map-canvas');
            summaryPanel.innerHTML = '<font color=red><b>Final Route is not yet approved.</b></font>';

        }
    </script>
  </head>
  <body>
  <form id="form1" runat="server">
    <center>
    <asp:Label ID="lblMessage" runat="server" Text="<font color=red><b>Final Route is not yet approved.</b></font>" Visible="false"></asp:Label>
 </center>
  
    <div id="map-canvas" style="float:left;margin-left:5px;width:99%;height:600px;"></div>
    <div id="control_panel" style="float:right;width:30%;text-align:left;padding-top:20px">
    <div style="margin:20px;border-width:2px;display:none">
    <b>Start:</b>
    <select id="start">
      <option value="0.3167,32.5833">Kampala</option>
    </select>
    <br>
    <b>Waypoints:</b> <br>
    <i>(Ctrl-Click for multiple selection)</i> <br>
    <select multiple id="waypoints">
      <option value="0.448224,33.181414">S1</option>
      <option value="0.446771,33.182216">S2</option>
      <option value="0.446073,33.181292">S3</option>
    <option value="0.445075,33.179944">S4</option>
    <option value="0.443125,33.17731">S5</option>
    <option value="0.441382,33.174956">S6</option>
    <option value="0.439389,33.172264">S7</option>
    <option value="0.437978,33.169459">S8</option>
    <option value="0.436482,33.168338">S9</option>
    <option value="0.434703,33.165934">S10</option>
    </select>
    <br>
    <b>End:</b>
    <select id="end">
      <option value="0.434703,33.165934">Buikwe</option>
    </select>
    <br>
      <input type="submit" onclick="calcRoute();">
    </div>
    <div id="directions_panel" style="margin:20px;background-color:#FFEE77;display:none"></div>    
    </div>
<asp:HiddenField ID="hdnStartingCoordinate" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hdnEndingCoordinate" ClientIDMode="Static" runat="server" />
</form>
  </body>
</html>
