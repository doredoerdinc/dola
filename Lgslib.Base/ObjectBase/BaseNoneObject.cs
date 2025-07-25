using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;
using System.ComponentModel.DataAnnotations.Schema;
using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Drawing;
using DevExpress.ExpressApp.Editors;

namespace LgsLib.Base
{
    
    public interface IMapPoint 
    {
        string Key { get; }
        string Marker { get; }
        string Title { get; } 
        double Latitude { get; } 
        double Longitude { get; }

    }

    public interface IMapMultiplePoint : IMapPoint
    { 
        IList<IMapPoint> Points { get; }
    }

    public interface IRoute
    {

    }

    [DomainComponent]
    public class Map : NonePercentObject, IMapPoint
    {
        public Map() { }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        String _Key;
        public String Key
        {
            get { return _Key; }
            set { _Key = value; }
        }
        double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }
        double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        String _Marker;
        public String Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }


        List<MapPointLGS> _Points;
        public List<MapPointLGS> Points
        {
            get { return _Points; }
            set { _Points = value; }
        }


    }

    [DomainComponent]
    public class MapPointLGS : NonePercentObject, IMapPoint
    {
        public MapPointLGS() { }

        string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        String _Key;
        public String Key
        {
            get { return _Key; }
            set { _Key = value; }
        }


        double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }


        double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        String _Marker;
        public String Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }

        string _Object;
        public string Object
        {
            get { return _Object; }
            set { _Object = value; }
        }



    }

    [DomainComponent]
    public class MapPointOrderTransfer : NonePercentObject
    {
        public MapPointOrderTransfer() { }


        String _FromCode;
        public String FromCode
        {
            get { return _FromCode; }
            set { _FromCode = value; }
        }

        String _ToCode;
        public String ToCode
        {
            get { return _ToCode; }
            set { _ToCode = value; }
        } 
         
        String _EncodedPolyline;
        public String EncodedPolyline
        {
            get { return _EncodedPolyline; }
            set { _EncodedPolyline = value; }
        }

        String _Duration;
        public String Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        String _DictanceMeters;
        public String DictanceMeters
        {
            get { return _DictanceMeters; }
            set { _DictanceMeters = value; }
        } 

        int? _FromOrderQuantity;
        public int? FromOrderQuantity
        {
            get { return _FromOrderQuantity; }
            set { _FromOrderQuantity = value; }
        }

        int? _ToOrderQuantity;
        public int? ToOrderQuantity
        {
            get { return _FromOrderQuantity; }
            set { _FromOrderQuantity = value; }
        }

        String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        } 

        String _Key;
        public String Key
        {
            get { return _Key; }
            set { _Key = value; }
        }


        double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }


        double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        String _Marker;
        public String Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }

    }
  
}