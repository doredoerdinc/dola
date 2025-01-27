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

    public static class googleService
    {
        public static void GetDistanceMatrix()
        {
            using (var client = new WebClient())
            {
                var values = HttpUtility.ParseQueryString(string.Empty);

         

                values["origins"] = "test,{lat:12312313,lng:1203192380}";
                values["destinations"] = "test,{lat:12312313,lng:1203192380}";
                values["mode"] = "bicycling";
                values["language"] = "en";
                values["sensor"] = "false";
                values["key"] = "AIzaSyBqH8I4CO8L_ezjxoMxKXDVNKSonisvyhI";
                var uriBuilder = new UriBuilder("https://maps.googleapis.com/maps/api/distancematrix/json");
                uriBuilder.Query = values.ToString();
                var result = client.DownloadData(uriBuilder.ToString());
                var responString = Encoding.UTF8.GetString(result);
                //    JObject json = JObject.Parse(responString);
                var reponseDistance = JsonConvert.DeserializeObject<DistanceResponse>(responString);

                var a = 1;
                //var serializer = new JsonSerializer();
                //var distanceResponse = serializer.Deserialize<DistanceResponse>(json);
                //if (string.Equals("ok", distanceResponse.Status, StringComparison.OrdinalIgnoreCase))
                //{
                //    Console.WriteLine("origin addresses: {0}", string.Join(", ", distanceResponse.Origin_Addresses));
                //    Console.WriteLine("destination addresses: {0}", string.Join(", ", distanceResponse.Destination_Addresses));
                //    foreach (var row in distanceResponse.Rows)
                //    {
                //        foreach (var element in row.Elements)
                //        {
                //            if (string.Equals("ok", element.Status, StringComparison.OrdinalIgnoreCase))
                //            {
                //                Console.WriteLine("Distance: {0} {1}", element.Distance.Text, element.Distance.Value);
                //                Console.WriteLine("Duration: {0} {1}", element.Duration.Text, element.Duration.Value);
                //            }
                //        }
                //    }
                //}
            }
        }
         public static void GoogleComputeRoutes(List<LocationGeo> orginAddress, List<LocationGeo> destinationsAddress, IObjectSpace newIObjectSpace)
        {
            using (WebClient wc = new WebClient())
            { 
                byte[] resByte;
                string resString;
                byte[] reqString;

                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "*/*";
                wc.Headers["X-Goog-Api-Key"] = "AIzaSyAlQGCtvldPPETwFL7mO6nEX7adcCYoQgE";
                wc.Headers["X-Goog-FieldMask"] = "*";
                wc.Encoding = System.Text.Encoding.UTF8;
                var url = "https://routes.googleapis.com/directions/v2:computeRoutes"; 

                foreach (var org in orginAddress)
                {
                    Dictionary<string, object> dictData = new Dictionary<string, object>();
                    var fromLatLong = new GLatLng();
                    fromLatLong.latitude = org.Latitude;
                    fromLatLong.longitude = org.Longitude;
                    var fromGlocation = new GLocation();
                    fromGlocation.latLng = fromLatLong;
                    var orgin = new GOrigin();
                    orgin.location = fromGlocation;
                    dictData.Add("origin", orgin);

                    foreach (var des in destinationsAddress)
                    {
                        if (des.IntegrationCode != org.IntegrationCode)
                        { 
                            var toLatLong = new GLatLng();
                            toLatLong.latitude = des.Latitude;
                            toLatLong.longitude = des.Longitude;
                            var toGlocation = new GLocation();
                            toGlocation.latLng = toLatLong; 

                            var destination = new GOrigin();
                            destination.location = toGlocation;

                            var fromJson = JsonConvert.SerializeObject(orgin);
                            var tojson = JsonConvert.SerializeObject(destination);

                            dictData.Add("destination", destination);
                            var jsonData = JsonConvert.SerializeObject(dictData, Formatting.Indented);
                            reqString = Encoding.UTF8.GetBytes(jsonData);
                            JObject jobject = JObject.Parse(jsonData);

                            var filePath = String.Format(AppDomain.CurrentDomain.BaseDirectory + @"\{0}.json", "test1");
                            System.IO.File.WriteAllText(filePath, jsonData);
                            try
                            {
                                resByte = wc.UploadData(url, "POST", reqString);
                                resString = Encoding.UTF8.GetString(resByte);
                                var reponse=JsonConvert.DeserializeObject<GoogleRouteResponseRoot>(resString);
                                JObject json = JObject.Parse(resString);

                                AddressRouteMatrix admat = newIObjectSpace.CreateObject<AddressRouteMatrix>();

                                admat.FromAddress = newIObjectSpace.GetObjectByKey<Address>(org.IntegrationCode);
                                admat.ToAddressCode = newIObjectSpace.GetObjectByKey<Address>(des.IntegrationCode);
                                GoogleRoute route = reponse.routes.FirstOrDefault();
                                admat.EncodedPolyline = route.polyline.encodedPolyline;
                                admat.Duration =route.duration;
                                admat.StaticDuration = route.staticDuration;
                                admat.DictanceMeters = route.distanceMeters;
                                newIObjectSpace.CommitChanges();
                                dictData.Remove("destination");

                                var filePath2 = String.Format(AppDomain.CurrentDomain.BaseDirectory + @"\{0}.json", "response");
                                System.IO.File.WriteAllText(filePath2, json.ToString());
                            }
                            catch (Exception e)
                            {
                                var b = e;
                            }
                        }
                    }
                }

             

            }
        }
    }
    public class DistanceResult
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

        public class DistanceResponse
        {
            public string Status { get; set; }
            public string[] Origin_Addresses { get; set; }
            public string[] Destination_Addresses { get; set; }
            public Row[] Rows { get; set; }
        }

        public class Row
        {
            public Element[] Elements { get; set; }
        }

        public class Element
        {
            public string Status { get; set; }
            public Item Duration { get; set; }
            public Item Distance { get; set; }
        }

        public class Item
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }

    }