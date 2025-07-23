<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapView.aspx.cs" Inherits="dola.Web.map.MapView" %>
<%@ Import Namespace="dola.Module.Web" %>
<%@ Import Namespace="System.Globalization" %>
<!DOCTYPE html>
<html>
  <head>
    <title>Advanced Marker Basic Customization</title>

    <link rel="stylesheet" type="text/css" href="./style.css" />
    <script type="module">
        const parser = new DOMParser();

        async function initMap() {
            // Request needed libraries.
            const { Map } = await google.maps.importLibrary("maps");
            const { AdvancedMarkerElement, PinElement } = await google.maps.importLibrary(
                "marker",
            );
            const map = new Map(document.getElementById("map"), {
                 //40.7446227,30.2383646
                center: { lat: 40.7446227, lng: 30.2383646 },
                zoom: 8,
                mapId: "4504f8b37365c3d0",
            });
<%
     NumberFormatInfo nfi = new NumberFormatInfo();
     nfi.NumberDecimalSeparator = ".";
     if (MapStatic.StaticMapPointOrderTransferList != null)
     {   
         foreach (var item in MapStatic.StaticMapPointOrderTransferList){%>  

            let divCardContainer<%=item.Key%>  = document.createElement("div");
            let divCardHeader<%=item.Key%>  = document.createElement("div");
            let divGridContainer<%=item.Key%>  = document.createElement("div");
            let divGridFrom<%=item.Key%>  = document.createElement("div");
            let divGridTo<%=item.Key%>  = document.createElement("div");
            //	let divGridFromVal= document.createElement("div");
            //	let divGridFromToVal= document.createElement("div");

            divCardContainer<%=item.Key%> .className = 'card-container';
            divGridContainer<%=item.Key%> .className = 'grid-container';
            divCardHeader<%=item.Key%> .className = 'header';

            divCardHeader<%=item.Key%> .textContent = '<%=item.Title%>'
            divGridFrom<%=item.Key%> .textContent = 'Giris:' + '<%=item.FromOrderQuantity%>'
            divGridTo<%=item.Key%> .textContent = 'Cikis: 10'
            //	divGridFromVal.textContent='10'
            //	divGridFromToVal.textContent='20'


            divCardContainer<%=item.Key%> .appendChild(divCardHeader<%=item.Key%> );
            divCardContainer<%=item.Key%> .appendChild(divGridContainer<%=item.Key%> );
            divGridContainer<%=item.Key%> .appendChild(divGridFrom<%=item.Key%> );
            divGridContainer<%=item.Key%> .appendChild(divGridTo<%=item.Key%> );
            //divGridContainer.appendChild(divGridFromVal); 
            //divGridContainer.appendChild(divGridFromToVal); 


            let marker<%=item.Key%> = new AdvancedMarkerElement({
                map,
                position: { lat: <%=item.Latitude.ToString(nfi)%>, lng: <%=item.Longitude.ToString(nfi)%> },
                content: divCardContainer<%=item.Key%> ,
            });   

         <% 
            }
        }
         if (MapStatic.StaticMapPointOrderTransferList != null)
     {   
         foreach (var item in MapStatic.StaticPointList){%>  

            let divCardContainer<%=item.Key%>  = document.createElement("div");
            let divCardHeader<%=item.Key%>  = document.createElement("div");
            let divGridContainer<%=item.Key%>  = document.createElement("div");
        
            //	let divGridFromVal= document.createElement("div");
            //	let divGridFromToVal= document.createElement("div");

            divCardContainer<%=item.Key%> .className = 'card-container';
            divGridContainer<%=item.Key%> .className = 'grid-container';
            divCardHeader<%=item.Key%> .className = 'header';

            divCardHeader<%=item.Key%> .textContent = '<%=item.Title%>'
        
            //	divGridFromVal.textContent='10'
            //	divGridFromToVal.textContent='20'


            divCardContainer<%=item.Key%> .appendChild(divCardHeader<%=item.Key%> );
            divCardContainer<%=item.Key%> .appendChild(divGridContainer<%=item.Key%> );
           
            //divGridContainer.appendChild(divGridFromVal); 
            //divGridContainer.appendChild(divGridFromToVal); 


            let marker<%=item.Key%> = new AdvancedMarkerElement({
                map,
                position: { lat: <%=item.Latitude.ToString(nfi)%>, lng: <%=item.Longitude.ToString(nfi)%> },
                content: divCardContainer<%=item.Key%> ,
            });

         <% 
            }
        }
%> 
            function getRandomColor() {
                var letters = '0123456789ABCDEF';
                var color = '#';
                for (var i = 0; i < 6; i++) {
                    color += letters[Math.floor(Math.random() * 16)];
                }
                return color;
            }
                
<% 
        if (MapStatic.StaticRouteDrawList != null) {
            foreach (var line in MapStatic.StaticRouteDrawList)
            {
                var latlongList = MapStatic.DecodePolylinePoints(line.EncodedPolyline);

%>             
            var polyLine = [];
            <%
        foreach (var pl in latlongList)
        {
            %>
            polyLine.push({ lat:<%=pl.lat.ToString(nfi)%>, lng:<%=pl.lng.ToString(nfi)%>});
            <%
        }
        %>
           
            const flightPath<%=line.SysCode%> = new google.maps.Polyline({
                map,
                path:polyLine,
                geodesic: true,
                strokeColor: getRandomColor(),
                strokeOpacity: 2,
                strokeWeight: 4,
                });
<%
        }
        }
%>


        }  

        initMap();

    </script>
        <script>(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]") ?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })
    ({ key: "AIzaSyAlQGCtvldPPETwFL7mO6nEX7adcCYoQgE", v: "weekly" });</script>
  </head>
  <body>
    <div id="map"></div>

    <!-- prettier-ignore -->
  
  </body>
</html>
