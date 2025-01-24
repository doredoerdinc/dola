using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dola.Module
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Distance
    {
        public string text { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
    }

    public class EndLocation
    {
        public LatLng latLng { get; set; }
    }

    public class GeocodingResults
    {
    } 
    public class High
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    } 
    public class LatLng
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class PolyLatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class GoogleLeg
    {
        public int distanceMeters { get; set; }
        public string duration { get; set; }
        public string staticDuration { get; set; }
        public Polyline polyline { get; set; }
        public StartLocation startLocation { get; set; }
        public EndLocation endLocation { get; set; }
        public List<Step> steps { get; set; }
        public LocalizedValues localizedValues { get; set; }
    }

    public class LocalizedValues
    {
        public Distance distance { get; set; }
        public StaticDuration staticDuration { get; set; }
        public Duration duration { get; set; }
    }

    public class Low
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class NavigationInstruction
    {
        public string maneuver { get; set; }
        public string instructions { get; set; }
    }

    public class Polyline
    {
        public string encodedPolyline { get; set; }
    }

    public class PolylineDetails
    {
    }

    public class GoogleRouteResponseRoot
    {
        public List<GoogleRoute> routes { get; set; }
        public GeocodingResults geocodingResults { get; set; }
    }

    public class GoogleRoute
    {
        public List<GoogleLeg> legs { get; set; }
        public int distanceMeters { get; set; }
        public string duration { get; set; }
        public string staticDuration { get; set; }
        public Polyline polyline { get; set; }
        public Viewport viewport { get; set; }
        public TravelAdvisory travelAdvisory { get; set; }
        public LocalizedValues localizedValues { get; set; }
        public List<string> routeLabels { get; set; }
        public PolylineDetails polylineDetails { get; set; }
    }

    public class StartLocation
    {
        public LatLng latLng { get; set; }
    }

    public class StaticDuration
    {
        public string text { get; set; }
    }

    public class Step
    {
        public string staticDuration { get; set; }
        public Polyline polyline { get; set; }
        public StartLocation startLocation { get; set; }
        public EndLocation endLocation { get; set; }
        public NavigationInstruction navigationInstruction { get; set; }
        public LocalizedValues localizedValues { get; set; }
        public string travelMode { get; set; }
    }

    public class TravelAdvisory
    {
    }

    public class Viewport
    {
        public Low low { get; set; }
        public High high { get; set; }
    }
   

}
