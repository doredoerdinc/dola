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
    public class TripBase:BaseObjectC
    {
        Person _Driver;
        public virtual Person Driver
        {
            get { return _Driver; }
            set { _Driver = value; }
        }

        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }


    }


    [Table("TripCargo")]
    [DefaultClassOptions]
    public class TripCargo : TripBase//, IMapMultiplePoint
    {
        public TripCargo()
        {
            Tasks = new List<Task>();
            Orders = new List<Order>();
        }

        State _State;
        public virtual State State
        {
            get { return _State; }
            set { _State = value; }
        }


        DateTime? _PlanedStartDate;
        public DateTime? PlanedStartDate
        {
            get { return _PlanedStartDate; }
            set { _PlanedStartDate = value; }
        }

        DateTime? _PlanedFinishDate;
        public DateTime? PlanedFinishDate
        {
            get { return _PlanedFinishDate; }
            set { _PlanedFinishDate = value; }
        } 

        Address _StartAddress;
        public virtual Address StartAddress
        {
            get { return _StartAddress; }
            set { _StartAddress = value; }
        } 

        TaskTemplate _TaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        } 
        public virtual IList<Task> Tasks { get; set; } 
        public virtual IList<Order> Orders { get; set; }
       

        public override void OnSaving()
        {
            base.OnSaving();

        }


    }

    [Table("TripDocuments")]
    [DefaultClassOptions]
    public class TripDocument : BaseObjectC
    {
        String _DocumentCode;
        public String DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }
        }

        DateTime _DocumentTime;
        public DateTime DocumentTime
        {
            get { return _DocumentTime; }
            set { _DocumentTime = value; }
        }

        FileData _FileData;
        public virtual FileData FileData
        {
            get { return _FileData; }
            set { _FileData = value; }
        }


    } 


    [Table("Trip")]
    [DefaultClassOptions]
    public class Trip : OrderBase//, IMapMultiplePoint
    {
        public Trip()
        { 
            Tasks = new List<Task>();
            Containeries = new List<Container>();
        }

        TaskTemplate _TaskTemplate;
        public virtual TaskTemplate TaskTemplate
        {
            get { return _TaskTemplate; }
            set { _TaskTemplate = value; }
        }


        public virtual IList<Task> Tasks { get; set; }
         
        public virtual IList<Container> Containeries { get; set; } 

        Vehicle _Vehicle;
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set { _Vehicle = value; }
        }

        Person _Employee;
        public virtual Person Employee
        {
            get { return _Employee; }
            set { _Employee = value; }
        }

        public String Key { get { return SysCode;} }
        public string Title
        {
            get
            {
                if (this._Vehicle != null && Vehicle.LastVehiclePosition != null)
                {
                    return Vehicle.VehicleIdentifier;
                } else
                {
                    return null;
                }
            }
            
        }
        public double Latitude
        {
            get
            {
                if (this._Vehicle != null && Vehicle.LastVehiclePosition != null)
                {
                    return Vehicle.LastVehiclePosition.Latitude.Value;
                }
                else
                {
                    return 0;
                }
            } 
        }

        public double Longitude
        {
            get
            {
                if (this._Vehicle != null && Vehicle.LastVehiclePosition != null)
                {
                    return Vehicle.LastVehiclePosition.Longitude.Value;
                }
                else
                {
                    return 0;
                }
            }
        }

       // public IList<IMapPoint> Points => throw new NotImplementedException();

        //public IList<IMapPoint> Points {

        //}

        public override void OnSaving()
        {
            base.OnSaving();
            
        } 

    }

    

}