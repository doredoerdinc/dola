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
using DevExpress.Persistent.Validation;
using Region = LgsLib.Base.Region;

namespace dola.Module
{ 
    public enum EnumVehicleOwntype
    {
        None = 0,
        Own = 1,
        Rent = 2,
    }

    [Table("VehicleBrand")]
    [DefaultClassOptions]
    public class VehicleBrand : BaseObjectC
    {
        public VehicleBrand() { }

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public virtual List<VehicleModel> Models { get; set; }
    }

    [Table("VehicleModel")]
    [DefaultClassOptions]
    public class VehicleModel : BaseObjectC
    {
        public VehicleModel() { }

        VehicleBrand _Brand;
        public virtual VehicleBrand Brand
        {
            get { return _Brand; }
            set { _Brand = value; }
        } 

        String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    } 

    [Table("VehicleType")]
    [DefaultClassOptions]
    public class VehicleType: BaseLookupC
    {
        public VehicleType() { }

        int? _Lenght;
        public int? Lenght
        {
            get { return _Lenght; }
            set { _Lenght = value; }
        }

        int? _Width;
        public int? Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        int? _Height;
        public int? Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        int? _CubicMeter;
        public int? CubicMeter
        {
            get { return _CubicMeter; }
            set { _CubicMeter = value; }
        }

        int? _NetWeightKG;
        public int? NetWeightKG
        {
            get { return _NetWeightKG; }
            set { _NetWeightKG = value; }
        }

        int? _MaximumLoadingWeiht;
        public int? MaximumLoadingWeiht
        {
            get { return _MaximumLoadingWeiht; }
            set { _MaximumLoadingWeiht = value; }
        }

    } 

    [Table("Vehicle")]
    [DefaultClassOptions]
    public class Vehicle : BaseObjectState
    {

        public Vehicle() 
        {
             Positions = new List<VehiclePosition>();
             Fuels = new List<VehicleFuel>();
             Documents = new List<DocumentTracking>(); 
        }

        EnumVehicleOwntype _OwnType;
        public EnumVehicleOwntype OwnType
        {
            get { return _OwnType; }
            set { _OwnType = value; }
        } 

        DateTime? _InspectionValidityDate;
        public DateTime? InspectionValidityDate
        {
            get { return _InspectionValidityDate; }
            set { _InspectionValidityDate = value; }
        } 
       
        DateTime? _FirstRegistirationDate;
         public DateTime? FirstRegistirationDate
        {
            get { return _FirstRegistirationDate; }
            set { _FirstRegistirationDate = value; }
        }

        DateTime? _RegistrationDate;
        public DateTime? RegistrationDate
        {
            get { return _RegistrationDate; }
            set { _RegistrationDate = value; }
        }

        String _ChassisCode;
        public String ChassisCode
        {
            get { return _ChassisCode; }
            set { _ChassisCode = value; }
        }

        String _MotorCode;
        public String MotorCode
        {
            get { return _MotorCode; }
            set { _MotorCode = value; }
        } 

        Owner _OwnerAssigned;
        public virtual Owner OwnerAssigned
        {
            get { return _OwnerAssigned; }
            set { _OwnerAssigned = value; }
        }

        Owner _OwnerMain;
        public virtual Owner OwnerMain
        {
            get { return _OwnerMain; }
            set { _OwnerMain = value; }
        } 

        VehicleModel _VehicleModel;
        public virtual VehicleModel VehicleModel
        {
            get { return _VehicleModel; }
            set { _VehicleModel = value; }
        }

        int? _ModelYear;
        public int? ModelYear
        {
            get { return _ModelYear; }
            set { _ModelYear = value; }
        } 

        String _VehicleIdentifier;
        [RuleRequiredField(DefaultContexts.Save)]
        [StringLength(200)]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public String VehicleIdentifier
        {
            get { return _VehicleIdentifier; }
            set { _VehicleIdentifier = value; }
        } 

        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        String _Trailer;
        public String Trailer
        {
            get { return _Trailer; }
            set { _Trailer = value; }
        }

        String _Driver;
        public String Driver
        {
            get { return _Driver; }
            set { _Driver = value; }
        }

        Vehicle _LinkVehicle;
        public Vehicle LinkVehicle
        {
            get { return _LinkVehicle; }
            set { _LinkVehicle = value; }
        }


        Person _DefaultDriver;
        public virtual Person DefaultDriver
        {
            get { return _DefaultDriver; }
            set { _DefaultDriver = value; }
        }   
        JobTitle _VehicleJobTitle;
        public JobTitle VehicleJobTitle
        {
            get { return _VehicleJobTitle; }
            set { _VehicleJobTitle = value; }
        } 

        string _TrackingDeviceCode;
        public string TrackingDeviceCode
        {
            get { return _TrackingDeviceCode; }
            set { _TrackingDeviceCode = value; }
        }

        string _DigitalTachograph;
        public string DigitalTachograph
        {
            get { return _DigitalTachograph; }
            set { _DigitalTachograph = value; }
        }

        String _IntegrationCode;
        public String IntegrationCode
        {
            get { return _IntegrationCode; }
            set { _IntegrationCode = value; }
        } 

        VehicleType _VehicleType;
        public virtual VehicleType VehicleType
        {
            get { return _VehicleType; }
            set { _VehicleType = value; }
        }

        Region _RegionVehicle;
        public virtual Region RegionVehicle
        {
            get { return _RegionVehicle; }
            set { _RegionVehicle = value; }
        }

        VehiclePosition _LastVehiclePosition;
        public virtual VehiclePosition LastVehiclePosition
        {
            get { return _LastVehiclePosition; }
            set { _LastVehiclePosition = value; }
        }
        public override void OnSaving()
        { 
            if (IsNewObject&& this.VehicleIdentifier != null)
            {

                SysCode = VehicleIdentifier.Replace(" ", "");
            }
            base.OnSaving();
        } 

        //public virtual List<Document> Documents { get; set; }
        public virtual List<VehiclePosition > Positions { get; set; }
        public virtual List<VehicleFuel> Fuels { get; set; }
        public virtual List<DocumentTracking> Documents { get; set; } 
        public string Key
        {
            get { return this.SysCode; }

        }  
        public string Title
        {
            get {
                if (LastVehiclePosition != null) { 
                var title=string.Format(
                        @"Plaka={0}
                          Km:{1}
                          Kontak:{2}
                          Hız:{3}
                          Yakıt Seviye:{4}
                          Pozisyon Zaman:{5}
                          Sürücü:{6}
                          Adress:{7}
                         ",VehicleIdentifier,LastVehiclePosition.Km,LastVehiclePosition.Ignation,LastVehiclePosition.Speed,LastVehiclePosition.FuelLevel,LastVehiclePosition.PositionTime,LastVehiclePosition.FirstDriver,LastVehiclePosition.Address);
                    return title;
                }
                return SysCode;
                
                }
            
        }

        public double Latitude
        {
            get { if (LastVehiclePosition != null) 
                    {
                        return LastVehiclePosition.Latitude.Value;
                    }
                return 0;
            }

        }

        public double Longitude
        {
            get
            {
                if (LastVehiclePosition != null)
                {
                    return LastVehiclePosition.Longitude.Value;
                }
                return 0;
            } 
        }
         
    }
} 