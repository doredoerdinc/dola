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
using DevExpress.ExpressApp;

namespace dola.Module
{
    [Table("VW_DriverWorkingDay")]
    public class DriverWorkingDay : IXafEntityObject, IObjectSpaceLink
    {
        public DriverWorkingDay() { }

        String _IdentityCode;
        [Key]
        public String IdentityCode
        {
            get { return _IdentityCode; }
            set { _IdentityCode = value; }
        }

        String _Driver;
        public String Driver
        {
            get { return _Driver; }
            set { _Driver = value; }
        }

        int? _TripCount;
        public int? TripCount
        {
            get { return _TripCount; }
            set { _TripCount = value; }
        }


        int? _GYear;
        public int? GYear
        {
            get { return _GYear; }
            set { _GYear = value; }
        }

        int? _GMonth;
        public int? GMonth
        {
            get { return _GMonth; }
            set { _GMonth = value; }
        }

        int? _GDay;
        public int? GDay
        {
            get { return _GDay; }
            set { _GDay = value; }
        }

        DateTime? _WorkingDay;
        public DateTime? WorkingDay
        {
            get { return _WorkingDay; }
            set { _WorkingDay = value; }
        }


        DateTime? _WorkDateTimeStart;
        public DateTime? WorkDateTimeStart
        {
            get { return _WorkDateTimeStart; }
            set { _WorkDateTimeStart = value; }
        }

        DateTime? _WorkDateTimeFinish;
        public DateTime? WorkDateTimeFinish
        {
            get { return _WorkDateTimeFinish; }
            set { _WorkDateTimeFinish = value; }
        }

        int? _WorkMinute;
        public int? WorkMinute
        {
            get { return _WorkMinute; }
            set { _WorkMinute = value; }
        }


        IObjectSpace _ObjectSpace;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return _ObjectSpace; }
            set { _ObjectSpace = value; }
        }

        public virtual void OnCreated()
        { 
        }

        public virtual void OnSaving()
        {
            
        }

        public virtual void OnLoaded()
        {

        }

    }
    [Table("VW_VehicleWorkingDay")]
    public class VehicleWorkingDay : IXafEntityObject, IObjectSpaceLink
    {
        public VehicleWorkingDay() { }

        String _VehicleIdentifier;
        [Key]
        public String VehicleIdentifier
        {
            get { return _VehicleIdentifier; }
            set { _VehicleIdentifier = value; }
        }

        String _Vehicle;
        public String Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        int? _TripCount;
        public int? TripCount
        {
            get { return _TripCount; }
            set { _TripCount = value; }
        } 

        int? _GYear;
        public int? GYear
        {
            get { return _GYear; }
            set { _GYear = value; }
        }

        int? _GMonth;
        public int? GMonth
        {
            get { return _GMonth; }
            set { _GMonth = value; }
        }

        int? _GDay;
        public int? GDay
        {
            get { return _GDay; }
            set { _GDay = value; }
        }

        DateTime? _WorkingDay;
        public DateTime? WorkingDay
        {
            get { return _WorkingDay; }
            set { _WorkingDay = value; }
        }


        DateTime? _WorkDateTimeStart;
        public DateTime? WorkDateTimeStart
        {
            get { return _WorkDateTimeStart; }
            set { _WorkDateTimeStart = value; }
        }

        DateTime? _WorkDateTimeFinish;
        public DateTime? WorkDateTimeFinish
        {
            get { return _WorkDateTimeFinish; }
            set { _WorkDateTimeFinish = value; }
        }

        int? _WorkMinute;
        public int? WorkMinute
        {
            get { return _WorkMinute; }
            set { _WorkMinute = value; }
        }

        Region _RegionVehicle;
        public virtual Region RegionVehicle
        {
            get { return _RegionVehicle; }
            set { _RegionVehicle = value; }
        }



        IObjectSpace _ObjectSpace;
        [NotMapped]
        [Browsable(false)]
        public IObjectSpace ObjectSpace
        {
            get { return _ObjectSpace; }
            set { _ObjectSpace = value; }
        }

        public virtual void OnCreated()
        {
        }

        public virtual void OnSaving()
        {

        }

        public virtual void OnLoaded()
        {

        }

    }
}