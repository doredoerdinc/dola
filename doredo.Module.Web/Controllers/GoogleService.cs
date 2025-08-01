using System.Net.Http.Json;
using LgsLib.Base;
using LgsLib.Base.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Newtonsoft.Json; 
using System.Net;  
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Web;
using DevExpress.ExpressApp;
using System.Net.Http;
using System.Globalization;
using DevExpress.Data.Filtering;
using System.Threading;

namespace dola.Module.Web
{
    public class GOrigin
    {
        public GLocation location { get; set; }
    }
    public class GDestination
    {
        public virtual List<GWayPoint> WayPoints { get; set; }
    }
    public class GWayPoint
    {
        public GLocation latLng { get; set; }
    }
    public class GLocation
    {
        public GLatLng latLng { get; set; }
    }
    public class GLatLng
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }
    public class googleService
    {
        public static string GoogleAPIKEY="AIzaSyBIMx0eWPwIW4LyicLuTUhpdWuuNq3rVz4";
        public static void CalculateRouteMatrixInChunks(List<Address> fromAddresses, List<Address> toAddresses, IObjectSpace objectSpace)
        {
            const int maxOrigins = 10;
            const int maxDestinations = 10;

            for (int i = 0; i < fromAddresses.Count; i += maxOrigins)
            {
                var originChunk = fromAddresses.Skip(i).Take(maxOrigins).ToList();

                for (int j = 0; j < toAddresses.Count; j += maxDestinations)
                {
                    var destChunk = toAddresses.Skip(j).Take(maxDestinations).ToList();

                    try
                    {
                        CalculateRouteMatrix(originChunk, destChunk, objectSpace);
                    }
                    catch (Exception ex)
                    {
                        // Hata loglanabilir veya kullanıcıya bildirilebilir
                        Console.WriteLine($"Google Matrix çağrısı hatası: {ex.Message}");
                    }

                    // Google’ın saniyelik limiti için istekler arasında kısa gecikme
                    Thread.Sleep(300); // 300ms bekleme, gerekirse artır
                }
            }
        }


        public static void CalculateRouteMatrix(List<Address> fromAddresses, List<Address> toAddresses, IObjectSpace objectSpace)
        {
            using (HttpClient client = new HttpClient())
            { 
                var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?key={GoogleAPIKEY}";

                var origins = string.Join("|", fromAddresses.Select(f =>
                    $"{f.Latitude.ToString(CultureInfo.InvariantCulture)},{f.Longitude.ToString(CultureInfo.InvariantCulture)}"));

                var destinations = string.Join("|", toAddresses.Select(t =>
                    $"{t.Latitude.ToString(CultureInfo.InvariantCulture)},{t.Longitude.ToString(CultureInfo.InvariantCulture)}"));

                url += $"&origins={origins}&destinations={destinations}&mode=driving";

                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                var matrix = JsonConvert.DeserializeObject<DistanceMatrixResponse>(responseString);

                if (matrix.Status != "OK")
                {
                    //var message = matrix.status switch
                    //{"INVALID_REQUEST" => "Geçersiz istek. Origins veya destinations eksik olabilir.",
                    //    "OVER_QUERY_LIMIT" => "API kota limiti aşıldı. Daha sonra tekrar deneyin.",
                    //    "REQUEST_DENIED" => "API erişimi reddedildi. Anahtar geçersiz olabilir.",
                    //    "UNKNOWN_ERROR" => "Bilinmeyen bir hata oluştu. Lütfen tekrar deneyin.",
                    //    _ => $"Google servisinden bilinmeyen hata: {matrix.status}"
                    //};
                    throw new ArgumentException($"Google API Hatası: {matrix}");
                }

                for (int i = 0; i < fromAddresses.Count; i++)
                {
                    string uncalculateAddress = "";
                    var fromAddress = objectSpace.GetObjectByKey<Address>(fromAddresses[i].SysCode);

                    for (int j = 0; j < toAddresses.Count; j++)
                    {
                        var toAddress = objectSpace.GetObjectByKey<Address>(toAddresses[j].SysCode);

                        if (fromAddresses[i].SysCode == toAddresses[j].SysCode)
                            continue;

                        var element = matrix.Rows[i].Elements[j];

                        if (element.Status != "OK" || element.Distance == null || element.Duration == null)
                        {
                            uncalculateAddress += $"#Name={toAddress.Name},Code={toAddress.SysCode},Status={element.Status}; ";
                            continue;
                        }

                        var admatCriteria = GroupOperator.Combine(
                            GroupOperatorType.And,
                            new BinaryOperator("FromAddress.Syscode", fromAddresses[i].SysCode, BinaryOperatorType.Equal),
                            new BinaryOperator("ToAddress.SysCode", toAddresses[j].SysCode, BinaryOperatorType.Equal));

                        AddressRouteMatrix admat = objectSpace.FindObject<AddressRouteMatrix>(admatCriteria);
                        if (admat == null)
                        {
                            admat = objectSpace.CreateObject<AddressRouteMatrix>();
                        }

                        admat.FromAddress = fromAddress;
                        admat.ToAddress = toAddress;
                        admat.Duration = element.Duration.Value;
                        admat.StaticDuration = element.Duration.Value;
                        admat.DictanceMeters = element.Distance.Value;
                        admat.EncodedPolyline = element.PolygonEncoded;
                        objectSpace.CommitChanges();
                    }

                    int unCalculateRoute = matrix.Rows[i].Elements.Count(x => x.Status != "OK" || x.Distance == null);
                    fromAddress.UncalculateAddress = uncalculateAddress;
                    fromAddress.UnCalculateRouteAddress = unCalculateRoute;
                    fromAddress.CalculateRouteAddress = fromAddress.AddressRouteMatrixies.Count();
                    fromAddress.CalculateRouteRequest = toAddresses.Count;
                    objectSpace.CommitChanges();
                }
            }
        }

        public static void CalculateDirectionsMatrix(List<Address> fromAddresses, List<Address> toAddresses, IObjectSpace objectSpace)
        {
            var client = new HttpClient();

            foreach (var from in fromAddresses)
            {
                foreach (var to in toAddresses)
                {
                    if (from.SysCode == to.SysCode)
                        continue;

                    var origin = $"{from.Latitude.ToString(CultureInfo.InvariantCulture)},{from.Longitude.ToString(CultureInfo.InvariantCulture)}";
                    var destination = $"{to.Latitude.ToString(CultureInfo.InvariantCulture)},{to.Longitude.ToString(CultureInfo.InvariantCulture)}";

                    var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={GoogleAPIKEY}&mode=driving";

                    var response = client.GetAsync(url).Result;
                    if (!response.IsSuccessStatusCode)
                        continue;

                    var json = response.Content.ReadAsStringAsync().Result;
                    var directions = JsonConvert.DeserializeObject<GoogleDirectionsResponse>(json);

                    var route = directions?.Routes?.FirstOrDefault();
                    if (route == null)
                        continue;

                    var polyline = route.OverviewPolyline?.Points;
                    var leg = route.Legs?.FirstOrDefault();

                    if (polyline == null || leg == null)
                        continue;

                    // Veritabanı güncelleme
                    var criteria = GroupOperator.Combine(
                        GroupOperatorType.And,
                        new BinaryOperator("FromAddress.Syscode", from.SysCode, BinaryOperatorType.Equal),
                        new BinaryOperator("ToAddress.SysCode", to.SysCode, BinaryOperatorType.Equal));

                    var admat = objectSpace.FindObject<AddressRouteMatrix>(criteria) ?? objectSpace.CreateObject<AddressRouteMatrix>();

                    admat.FromAddress = objectSpace.GetObjectByKey<Address>(from.SysCode);
                    admat.ToAddress = objectSpace.GetObjectByKey<Address>(to.SysCode);
                    admat.EncodedPolyline = polyline;
                    admat.Duration = leg.Duration?.Value ?? 0;
                    admat.DictanceMeters = leg.Distance?.Value ?? 0;

                    objectSpace.CommitChanges();

                    // Kota aşımını önlemek için kısa bekleme koyabilirsin
                  //  Thread.Sleep(150);
                }
            }
        }

        public static void CalculateRouteMatrixMissing(List<Address> addressList, IObjectSpace objectSpace)
        {
            var fromAddressRequestList = new List<Address>();
            var toAddressRequestList = new List<Address>();

            foreach (var from in addressList)
            {
                foreach (var to in addressList)
                {
                    if (from.SysCode == to.SysCode)
                        continue;

                    var criteria = GroupOperator.Combine(
                        GroupOperatorType.And,
                        new BinaryOperator("FromAddress.Syscode", from.SysCode),
                        new BinaryOperator("ToAddress.Syscode", to.SysCode)
                    );

                    var existing = objectSpace.FindObject<AddressRouteMatrix>(criteria);
                    if (existing == null)
                    {
                        fromAddressRequestList.Add(from);
                        toAddressRequestList.Add(to); 
                    }
                }
            }

            // Eksik kayıtlar varsa API çağrısı yap
            if (fromAddressRequestList.Count > 0)
            {
                CalculateDirectionsMatrix(fromAddressRequestList, toAddressRequestList, objectSpace);
            }
        }

    }
    public class DistanceMatrixResponse
    {
        [JsonProperty("destination_addresses")]
        public List<string> DestinationAddresses { get; set; }

        [JsonProperty("origin_addresses")]
        public List<string> OriginAddresses { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("error_message")]
        public string error_message { get; set; }

    }

    public class Row
    {
        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }
    }

    public class Element
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        // Polygon bilgisi (Google'dan encode edilmiş polyline string olarak)
        [JsonProperty("polygon_encoded")]
        public string PolygonEncoded { get; set; }
    }

    public class Distance
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; } // metre cinsinden
    }

    public class Duration
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; } // saniye cinsinden
    }


public class GoogleDirectionsResponse
    {
        [JsonProperty("routes")]
        public List<Route> Routes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Route
    {
        [JsonProperty("overview_polyline")]
        public OverviewPolyline OverviewPolyline { get; set; }

        [JsonProperty("legs")]
        public List<Leg> Legs { get; set; }
    }

    public class OverviewPolyline
    {
        [JsonProperty("points")]
        public string Points { get; set; }
    }

    public class Leg
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("end_address")]
        public string EndAddress { get; set; }

        [JsonProperty("steps")]
        public List<Step> Steps { get; set; }
    } 
    public class Step
    {
        [JsonProperty("distance")]
        public Distance Distance { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("html_instructions")]
        public string HtmlInstructions { get; set; }

        [JsonProperty("polyline")]
        public OverviewPolyline Polyline { get; set; }

        [JsonProperty("travel_mode")]
        public string TravelMode { get; set; }
    }

}