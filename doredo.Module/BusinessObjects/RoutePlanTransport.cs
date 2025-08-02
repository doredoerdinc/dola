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


namespace dola.Module
{

    [Table("RoutePlanTransport")]
    [DefaultClassOptions]
    public class RoutePlanTransport : BaseObjectState
    {
        public RoutePlanTransport()
        {
            WorkingTimes = new List<WorkingTime>(); 
        }

        WorkingTime _WorkingTime;
        public virtual WorkingTime WorkingTime
        {
            get { return _WorkingTime; }
            set { _WorkingTime = value; }
        } 
        Owner _Owner;
        public virtual Owner Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        } 
        String _Description;
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        int? _RouteTotalStation;
        public int? RouteTotalStation
        {
            get { return _RouteTotalStation; }
            set { _RouteTotalStation = value; }
        } 
        double? _RouteTotalKm;
        public double? RouteTotalKm
        {
            get { return _RouteTotalKm; }
            set { _RouteTotalKm = value; }
        }

        double? _RouteTotalDuration;
        public double? RouteTotalDuration
        {
            get { return _RouteTotalDuration; }
            set { _RouteTotalDuration = value; }
        }

        public override void OnSaving()
        {
            base.OnSaving();   
        }

        VehicleType _VehicleType;
        public virtual VehicleType VehicleType
        {
            get { return _VehicleType; }
            set { _VehicleType = value; }
        } 

        public virtual IList<WorkingTime> WorkingTimes { get; set; }


    }
}
