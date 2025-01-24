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
using System.Drawing;
 
namespace dola.Module
{

    public enum EnumGPS
    {
        [ImageName("State_Validation_Skipped")]
        OFF = 0,
        [ImageName("State_Validation_Valid")]
        ON = 1,
    }

    public enum EnumIgnation : int
    {
        
       [ImageName("State_Validation_Skipped")]
        OFF = 0,
       [ImageName("State_Validation_Valid")]
        ON = 1,
    }
 

    [Table("VehicleWaitingReason")]
    [DefaultClassOptions]
    public class VehicleWaitingReason : BaseObjectC
    {
        public VehicleWaitingReason() { }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

    }

     [Table("VehiclePosition")]
     [DefaultClassOptions]
     public class VehiclePosition :BaseObjectI
    {
        DateTime? _PositionTime;
        public DateTime? PositionTime
        {
            get { return _PositionTime; }
            set { _PositionTime = value; }
        }

        EnumGPS? _GPSActive;
        public EnumGPS? GPSActive
        {
            get { return _GPSActive; }
            set { _GPSActive = value; }
        }

        EnumIgnation? _Ignation;
        public EnumIgnation? Ignation
        {
            get { return _Ignation; }
            set { _Ignation = value; }
        }

        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        String _Address;
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        int? _SatCount;
        public int? SatCount
        {
            get { return _SatCount; }
            set { _SatCount = value; }
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

        String _Title;
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
         
        double? _Altitude;
        public double? Altitude
        {
            get { return _Altitude; }
            set { _Altitude = value; }
        }

        double? _Angle;
        public double? Angle
        {
            get { return _Angle; }
            set { _Angle = value; }
        }

        double? _Speed;
        public double? Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        double? _Km;
        public double? Km
        {
            get { return _Km; }
            set { _Km = value; }
        }

        double? _TotalKm;
        public double? TotalKm
        {
            get { return _TotalKm; }
            set { _TotalKm = value; }
        }

        double? _FuelLevel;
        public double? FuelLevel
        {
            get { return _FuelLevel; }
            set { _FuelLevel = value; }
        }

        double? _TotalLevel;
        public double? TotalLevel
        {
            get { return _TotalLevel; }
            set { _TotalLevel = value; }
        }
        String _DriverId;
        public String DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }

        String _DriverID2;
        public String DriverID2
        {
            get { return _DriverID2; }
            set { _DriverID2 = value; }
        }

        String _FirstDriver;
        public String FirstDriver
        {
            get { return _FirstDriver; }
            set { _FirstDriver = value; }
        }

        String _SecondDriver;
        public String SecondDriver
        {
            get { return _SecondDriver; }
            set { _SecondDriver = value; }
        }

        String _TrackingDeviceCode;
        public String TrackingDeviceCode
        {
            get { return _TrackingDeviceCode; }
            set { _TrackingDeviceCode = value; }
        }
           
    }

    [Table("PositionTemp")]
    [DefaultClassOptions]
    public class PositionTemp : BaseObjectI
    {
        PositionTemp() { }

        String _Owner;
        public String Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        String _DeviceId;
        public String DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }

        String _DateTime;
        public String DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }

        String _IsGpsActive;
        public String IsGpsActive
        {
            get { return _IsGpsActive; }
            set { _IsGpsActive = value; }
        }

        String _IsVehicleRunning;
        public String IsVehicleRunning
        {
            get { return _IsVehicleRunning; }
            set { _IsVehicleRunning = value; }
        } 

        String _Vehicle;
        public String Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        String _Address;
        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        String _SatCount;
        public String SatCount
        {
            get { return _SatCount; }
            set { _SatCount = value; }
        }

        String _Latitude;
        public String Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }

        String _Longitude;
        public String Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        String _Altitude;
        public String Altitude
        {
            get { return _Altitude; }
            set { _Altitude = value; }
        }

        String _Angle;
        public String Angle
        {
            get { return _Angle; }
            set { _Angle = value; }
        }

        String _Speed;
        public String Speed
        {
            get { return _Speed; }
            set { _Speed = value; }
        }

        String _Km;
        public String Km
        {
            get { return _Km; }
            set { _Km = value; }
        }

        String _TotalKm;
        public String TotalKm
        {
            get { return _TotalKm; }
            set { _TotalKm = value; }
        }

        String _FuelLevel;
        public String FuelLevel
        {
            get { return _FuelLevel; }
            set { _FuelLevel = value; }
        }

        String _TotalLevel;
        public String TotalLevel
        {
            get { return _TotalLevel; }
            set { _TotalLevel = value; }
        }

        String _DriverId;
        public String DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }

        String _DriverID2;
        public String DriverID2
        {
            get { return _DriverID2; }
            set { _DriverID2 = value; }
        }

        String _FirstDriver;
        public String FirstDriver
        {
            get { return _FirstDriver; }
            set { _FirstDriver = value; }
        }

        String _SecondDriver;
        public String SecondDriver
        {
            get { return _SecondDriver; }
            set { _SecondDriver = value; }
        }

        string _Error;
        [MaxLength]
        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }

    }
}
 
 
