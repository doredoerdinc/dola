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
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace dola.Module
{


    [DefaultClassOptions]
    [Table("EntryLocation")]
    [XafDefaultProperty("Vehicle")]
    public class EntryLocation : BaseObjectWarehouseID, INotifyPropertyChanged
    {
        
        public EntryLocation()
        {
            Orders = new List<Order>();
            
        }
         
        State _State;
        public virtual State State
        {
            get { return _State; }
            set { _State = value; }
        } 

        Vehicle _Vehicle;
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Vehicle Vehicle
        {
            get { return _Vehicle; }
            set {
                _Vehicle = value;
                OnPropertyChanged();
            }
        }

        Vehicle _LinkVehicle;
        public virtual Vehicle LinkVehicle
        {
            get { return _LinkVehicle; }
            set { _LinkVehicle = value; }
        }

        Person _Driver;
        public virtual Person Driver
        {
            get { return _Driver; }
            set { _Driver = value; }
        } 

        DateTime? _EntryTime;
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? EntryTime
        {
            get { return _EntryTime; }
            set { _EntryTime = value; }
        }

        DateTime? _ExitTime;
        public DateTime? ExitTime
        {
            get { return _ExitTime; }
            set { _ExitTime = value; }
        } 
        
        [Aggregated]
        [InverseProperty("EntryLocations")] 
        public virtual IList<Order> Orders {
            get; set; 
        }

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //if (propertyName == "Vehicle"&&this.ObjectSpace!=null)
            //{
            //    if(Vehicle.LinkVehicle!=null)
            //    {
            //       LinkVehicle = ObjectSpace.GetObject<Vehicle>(Vehicle.LinkVehicle);
                  
            //    }
            //      if(Vehicle.DefaultDriver!=null)
            //        { 
            //            var getDriver = ObjectSpace.GetObject<Person>(Vehicle.DefaultDriver);
            //        if (getDriver != null)
            //        {
            //            Driver = getDriver;
            //        }
            //    } 
            //}

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public override void OnSaving()
        {
            base.OnSaving();
            if (Orders.Count!= Orders.Count && ObjectSpace!=null&&ExitTime==null)
            {
                foreach (var ord in Orders)
                { 
                    var order = ObjectSpace.GetObject<Order>(ord);  
                    ord.LastEntryLocation = this; 
                }
            }  
        }
          
    }
}
