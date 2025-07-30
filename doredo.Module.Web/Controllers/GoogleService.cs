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

    public  class googleService 
    {
        public static void ComputeRouteMatrix(List<LocationGeo> fromAddresses, List<LocationGeo> toAddresses, IObjectSpace objectSpace)
        {
            using (HttpClient client = new HttpClient())
            {
                var apiKey = "AIzaSyBIMx0eWPwIW4LyicLuTUhpdWuuNq3rVz4"; // Config dosyasına taşı
                var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?key={apiKey}";

                var origins = string.Join("|", fromAddresses.Select(f =>
                $"{f.Latitude.ToString(CultureInfo.InvariantCulture)},{f.Longitude.ToString(CultureInfo.InvariantCulture)}"));

                var destinations = string.Join("|", toAddresses.Select(t =>
                $"{t.Latitude.ToString(CultureInfo.InvariantCulture)},{t.Longitude.ToString(CultureInfo.InvariantCulture)}"));

                url += $"&origins={origins}&destinations={destinations}&mode=driving";


                // HTTP çağrısı SENKRON yapılır
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;

                var matrix = JsonConvert.DeserializeObject<DistanceMatrixResponse>(responseString);
                if (matrix.status !="OK")
                {
                    var eror= string.Format("Google Service Error {0}",matrix.status);
                    throw new System.ArgumentException(eror); 
                } 

                for (int i = 0; i < fromAddresses.Count; i++)
                {
                    string uncalculateAddress = null;
                    var fromAddress = objectSpace.GetObjectByKey<Address>(fromAddresses[i].IntegrationCode); 
                    for (int j = 0; j < toAddresses.Count; j++)
                    {
                        var toAddress=objectSpace.GetObjectByKey<Address>(toAddresses[j].IntegrationCode);
                        if (fromAddresses[i].IntegrationCode != toAddresses[j].IntegrationCode)
                        {
                            var element = matrix.rows[i].elements[j];
                            var admatCriteria = GroupOperator.Combine(
                               GroupOperatorType.And
                               , new BinaryOperator("FromAddress.Syscode", fromAddresses[i].IntegrationCode, BinaryOperatorType.Equal)
                               , new BinaryOperator("ToAddress.SysCode", toAddresses[j].IntegrationCode, BinaryOperatorType.Equal));
                            if (matrix.rows[i].elements[j].distance != null)
                            {
                                AddressRouteMatrix admat = null;
                                admat = objectSpace.FindObject<AddressRouteMatrix>(admatCriteria);
                                if (admat == null)
                                {
                                    admat = objectSpace.CreateObject<AddressRouteMatrix>();
                                }
                                admat.FromAddress = fromAddress;
                                admat.ToAddress = toAddress;
                                admat.Duration = element.duration.value;
                                admat.StaticDuration = element.duration.value;
                                admat.DictanceMeters = element.distance.value;
                                objectSpace.CommitChanges();
                            }else
                            {
                                uncalculateAddress = uncalculateAddress + string.Format("#AdressName={0},AddressSysCode={1}", toAddress.Name, toAddress.SysCode);
                            }
                        }
                           
                         
                        }
                    int unCalculateRoute = matrix.rows[i].elements.Count(x => x.distance == null);
                    fromAddress.UncalculateAddress = uncalculateAddress;
                    fromAddress.UnCalculateRouteAddress = unCalculateRoute;
                    fromAddress.CalculateRouteAddress = fromAddress.AddressRouteMatrixies.Count();
                    fromAddress.CalculateRouteRequest = toAddresses.Count;
                    objectSpace.CommitChanges();
                }
            }
        }
    }

    public class DistanceMatrixResponse
    { 
        public string error_message { get; set; }
        public string status { get; set; }
        public List<string> destination_addresses { get; set; }
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Row
    {
        public List<Element> elements { get; set; }
    }

    public class Element
    {
        public TextValue distance { get; set; }
        public TextValue duration { get; set; }
        public string status { get; set; }
    }

    public class TextValue
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}