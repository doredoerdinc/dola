using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using LgsLib.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{


    [Table("LocationGeoType")]
    [DefaultClassOptions]
    public class LocationGeoType : BaseObjectC
    {
        
    }

    public abstract class BaseGeoPoint : BaseObjectI
    {
        public BaseGeoPoint()
        {

        }

        int _Index;
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
        }


        double? _Latitude;
        public double? Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        double? _Longitude;
        public double? Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

    }


    [Table("GeoRoute")]
    [DefaultClassOptions]
    public class GeoRoute : BaseObjectC
    {
        public GeoRoute()
        {
            Points = new List<GeoRoutePoint>();
        }

        public virtual IList<GeoRoutePoint> Points { get; set; }

        Address _FromAddress;
        public virtual Address FromAddress
        {
            get { return _FromAddress; }
            set { _FromAddress = value; }
        }

        Address _ToAddress;
        public virtual Address ToAddress
        {
            get { return _ToAddress; }
            set { _ToAddress = value; }
        }

        double? _Meter;
        public double? Meter
        {
            get { return _Meter; }
            set { _Meter = value; }
        }

    }

    [Table("GeoRoutePoint")]
    [DefaultClassOptions]
    public class GeoRoutePoint:BaseGeoPoint
    {
        GeoRoute _GeoRoute;
        public virtual GeoRoute GeoRoute
        {
            get { return _GeoRoute; }
            set { _GeoRoute = value; }
        } 
    }

    [Table("LocationGeo")]
    [DefaultClassOptions]
    [XafDefaultProperty(nameof(Title))]
    public class LocationGeo : BaseObjectI, IMapPoint
    {
        public LocationGeo()
        {
            Points = new List<LocationGeoPoints>();
        }  
        public virtual IList<LocationGeoPoints> Points { get; set; }


        int _Radius;
        public int Radius
        {
            get { return _Radius; }
            set { _Radius = value; }
        }

        String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }  

        double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }  

        LocationGeoType _LocationGeoType;
        public virtual LocationGeoType LocationGeoType
        {
            get { return _LocationGeoType; }
            set { _LocationGeoType = value; }
        } 

        string _IntegrationCode;
        public string IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        } 
      
        public String Key
        {
            get { return this.ID.ToString(); }
             
        }

        String _Marker;
        public String Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }


    }
     
    [Table("LocationGeoPoints")]
    [DefaultClassOptions]
    public class LocationGeoPoints: BaseGeoPoint
    {

        LocationGeo _LocationGeo;
        public virtual LocationGeo LocationGeo
        {
            get { return _LocationGeo; }
            set { _LocationGeo = value; }
        }

    }
}

